using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.IO;

namespace PgpUI.Services
{
    public class PgpService : IPgpService
    {
        // USAGE:
        //PGPEncryptDecrypt.EncryptFile(inputFileName,
        //                      outputFileName,
        //                      recipientKeyFileName,
        //                      shouldArmor,
        //                      shouldCheckIntegrity);
        //PGPEncryptDecrypt.Decrypt(inputFileName,
        //                  privateKeyFileName,
        //                  passPhrase,
        //                  outputFileName);
        private const int BufferSize = 0x10000; // should always be power of 2
        private bool _disposed;

        #region Encrypt

        /*
         * Encrypt the file.
         */

        public void EncryptFile(string inputFile, string outputFile, string publicKeyFile, bool armor, bool withIntegrityCheck)
        {
            try
            {
                using (Stream publicKeyStream = File.OpenRead(publicKeyFile))
                {
                    PgpPublicKey encKey = ReadPublicKey(publicKeyStream);

                    using (Stream outputStream = File.Create(outputFile))
                    {
                        Stream output = outputStream;

                        // Add armor if requested
                        if (armor)
                        {
                            output = new ArmoredOutputStream(output);
                        }

                        // Create encrypted data generator
                        PgpEncryptedDataGenerator encryptedDataGenerator = new PgpEncryptedDataGenerator(
                            SymmetricKeyAlgorithmTag.Cast5, withIntegrityCheck, new SecureRandom());
                        encryptedDataGenerator.AddMethod(encKey);

                        // Create the encrypted output stream
                        using (Stream encryptedOut = encryptedDataGenerator.Open(output, new byte[BufferSize]))
                        {
                            // Create compressed data generator
                            PgpCompressedDataGenerator compressedDataGenerator = new PgpCompressedDataGenerator(
                                CompressionAlgorithmTag.Zip);

                            using (Stream compressedOut = compressedDataGenerator.Open(encryptedOut))
                            {
                                // Create literal data generator
                                PgpLiteralDataGenerator literalDataGenerator = new PgpLiteralDataGenerator();
                                FileInfo fileInfo = new FileInfo(inputFile);

                                using (Stream literalOut = literalDataGenerator.Open(
                                    compressedOut, PgpLiteralData.Binary,
                                    fileInfo.Name, fileInfo.Length, fileInfo.LastWriteTime))
                                {
                                    // Write the file content
                                    using (FileStream inputFileStream = File.OpenRead(inputFile))
                                    {
                                        byte[] buffer = new byte[BufferSize];
                                        int bytesRead;

                                        while ((bytesRead = inputFileStream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            literalOut.Write(buffer, 0, bytesRead);
                                        }
                                    }
                                }
                            }
                        }

                        // Close armor stream if used
                        if (armor)
                        {
                            output.Close();
                        }
                    }
                }
            }
            catch (PgpException e)
            {
                throw;
            }
        }

        #endregion Encrypt

        #region Encrypt and Sign

        /*
         * Encrypt and sign the file pointed to by unencryptedFileInfo and
         */

        public void EncryptAndSign(string inputFile, string outputFile, string publicKeyFile, string privateKeyFile, string passPhrase, bool armor)
        {
            PgpEncryptionKeys encryptionKeys = new PgpEncryptionKeys(publicKeyFile, privateKeyFile, passPhrase);

            if (!File.Exists(inputFile))
                throw new FileNotFoundException(String.Format("Input file [{0}] does not exist.", inputFile));

            if (!File.Exists(publicKeyFile))
                throw new FileNotFoundException(String.Format("Public Key file [{0}] does not exist.", publicKeyFile));

            if (!File.Exists(privateKeyFile))
                throw new FileNotFoundException(String.Format("Private Key file [{0}] does not exist.", privateKeyFile));

            if (String.IsNullOrEmpty(passPhrase))
                throw new ArgumentNullException("Invalid Pass Phrase.");

            if (encryptionKeys == null)
                throw new ArgumentNullException("Encryption Key not found.");

            using (Stream outputStream = File.Create(outputFile))
            {
                if (armor)
                    using (ArmoredOutputStream armoredOutputStream = new ArmoredOutputStream(outputStream))
                    {
                        OutputEncrypted(inputFile, armoredOutputStream, encryptionKeys);
                    }
                else
                    OutputEncrypted(inputFile, outputStream, encryptionKeys);
            }
        }

        private void OutputEncrypted(string inputFile, Stream outputStream, PgpEncryptionKeys encryptionKeys)
        {
            using (Stream encryptedOut = ChainEncryptedOut(outputStream, encryptionKeys))
            {
                FileInfo unencryptedFileInfo = new FileInfo(inputFile);
                using (Stream compressedOut = ChainCompressedOut(encryptedOut))
                {
                    PgpSignatureGenerator signatureGenerator = InitSignatureGenerator(compressedOut, encryptionKeys);
                    using (Stream literalOut = ChainLiteralOut(compressedOut, unencryptedFileInfo))
                    {
                        using (FileStream inputFileStream = unencryptedFileInfo.OpenRead())
                        {
                            WriteOutputAndSign(compressedOut, literalOut, inputFileStream, signatureGenerator);
                            inputFileStream.Close();
                        }
                    }
                }
            }
        }

        private void WriteOutputAndSign(Stream compressedOut, Stream literalOut, FileStream inputFile, PgpSignatureGenerator signatureGenerator)
        {
            int length = 0;
            byte[] buf = new byte[BufferSize];
            while ((length = inputFile.Read(buf, 0, buf.Length)) > 0)
            {
                literalOut.Write(buf, 0, length);
                signatureGenerator.Update(buf, 0, length);
            }
            signatureGenerator.Generate().Encode(compressedOut);
        }

        private Stream ChainEncryptedOut(Stream outputStream, PgpEncryptionKeys m_encryptionKeys)
        {
            PgpEncryptedDataGenerator encryptedDataGenerator;
            encryptedDataGenerator = new PgpEncryptedDataGenerator(SymmetricKeyAlgorithmTag.TripleDes, new SecureRandom());
            encryptedDataGenerator.AddMethod(m_encryptionKeys.PublicKey);
            return encryptedDataGenerator.Open(outputStream, new byte[BufferSize]);
        }

        private Stream ChainCompressedOut(Stream encryptedOut)
        {
            PgpCompressedDataGenerator compressedDataGenerator = new PgpCompressedDataGenerator(CompressionAlgorithmTag.Zip);
            return compressedDataGenerator.Open(encryptedOut);
        }

        private Stream ChainLiteralOut(Stream compressedOut, FileInfo file)
        {
            PgpLiteralDataGenerator pgpLiteralDataGenerator = new PgpLiteralDataGenerator();
            return pgpLiteralDataGenerator.Open(compressedOut, PgpLiteralData.Binary, file);
        }

        private PgpSignatureGenerator InitSignatureGenerator(Stream compressedOut, PgpEncryptionKeys m_encryptionKeys)
        {
            const bool IsCritical = false;
            const bool IsNested = false;
            PublicKeyAlgorithmTag tag = m_encryptionKeys.SecretKey.PublicKey.Algorithm;
            PgpSignatureGenerator pgpSignatureGenerator = new PgpSignatureGenerator(tag, HashAlgorithmTag.Sha1);
            pgpSignatureGenerator.InitSign(PgpSignature.BinaryDocument, m_encryptionKeys.PrivateKey);
            foreach (string userId in m_encryptionKeys.SecretKey.PublicKey.GetUserIds())
            {
                PgpSignatureSubpacketGenerator subPacketGenerator = new PgpSignatureSubpacketGenerator();
                subPacketGenerator.SetSignerUserId(IsCritical, userId);
                pgpSignatureGenerator.SetHashedSubpackets(subPacketGenerator.Generate());
                // Just the first one!
                break;
            }
            pgpSignatureGenerator.GenerateOnePassVersion(IsNested).Encode(compressedOut);
            return pgpSignatureGenerator;
        }

        #endregion Encrypt and Sign

        #region Decrypt

        /*
       * decrypt a given stream.
       */

        public void Decrypt(string inputfile, string privateKeyFile, string passPhrase, string outputFile)
        {
            if (!File.Exists(inputfile))
                throw new FileNotFoundException(String.Format("Encrypted File [{0}] not found.", inputfile));

            if (!File.Exists(privateKeyFile))
                throw new FileNotFoundException(String.Format("Private Key File [{0}] not found.", privateKeyFile));

            if (String.IsNullOrEmpty(outputFile))
                throw new ArgumentNullException("Invalid Output file path.");

            using (Stream inputStream = File.OpenRead(inputfile))
            {
                using (Stream keyIn = File.OpenRead(privateKeyFile))
                {
                    Decrypt(inputStream, keyIn, passPhrase, outputFile);
                }
            }
        }

        /*
        * decrypt a given stream.
        */

        public void Decrypt(Stream inputStream, Stream privateKeyStream, string passPhrase, string outputFile)
        {
            try
            {
                inputStream = PgpUtilities.GetDecoderStream(inputStream);
                PgpObjectFactory pgpF = new PgpObjectFactory(inputStream);
                PgpObject o = pgpF.NextPgpObject();

                // Handle PGP encrypted data list
                PgpEncryptedDataList enc;
                if (o is PgpEncryptedDataList)
                    enc = (PgpEncryptedDataList)o;
                else
                    enc = (PgpEncryptedDataList)pgpF.NextPgpObject();

                // Find secret key
                PgpSecretKeyRingBundle pgpSec = new PgpSecretKeyRingBundle(
                    PgpUtilities.GetDecoderStream(privateKeyStream));

                PgpPrivateKey sKey = null;
                PgpPublicKeyEncryptedData pbe = null;

                foreach (PgpPublicKeyEncryptedData pked in enc.GetEncryptedDataObjects())
                {
                    sKey = FindSecretKey(pgpSec, pked.KeyId, passPhrase.ToCharArray());
                    if (sKey != null)
                    {
                        pbe = pked;
                        break;
                    }
                }

                if (sKey == null)
                    throw new ArgumentException("Secret key for message not found.");

                // Get decrypted stream
                using (Stream clear = pbe.GetDataStream(sKey))
                {
                    PgpObjectFactory plainFact = new PgpObjectFactory(clear);
                    PgpObject message = plainFact.NextPgpObject();

                    // Handle compressed data
                    if (message is PgpCompressedData cData)
                    {
                        using (Stream compDataIn = cData.GetDataStream())
                        {
                            PgpObjectFactory of = new PgpObjectFactory(compDataIn);
                            message = of.NextPgpObject();

                            // Skip signature lists if present
                            if (message is PgpOnePassSignatureList)
                            {
                                message = of.NextPgpObject();
                            }

                            ProcessLiteralData(message, outputFile);
                        }
                    }
                    // Handle literal data directly
                    else if (message is PgpLiteralData)
                    {
                        ProcessLiteralData(message, outputFile);
                    }
                    else
                    {
                        throw new PgpException("Message type not supported: " + message.GetType().Name);
                    }
                }
            }
            catch (PgpException ex)
            {
                throw;
            }
        }

        private void ProcessLiteralData(PgpObject message, string outputFile)
        {
            if (message is PgpLiteralData ld)
            {
                using (Stream output = File.Create(outputFile))
                using (Stream unc = ld.GetInputStream())
                {
                    Streams.PipeAll(unc, output);
                }
            }
            else
            {
                throw new PgpException("Expected literal data but found: " + message.GetType().Name);
            }
        }

        #endregion Decrypt

        #region Private helpers

        /*
        * A simple routine that opens a key ring file and loads the first available key suitable for encryption.
        */

        private PgpPublicKey ReadPublicKey(Stream inputStream)
        {
            inputStream = PgpUtilities.GetDecoderStream(inputStream);

            PgpPublicKeyRingBundle pgpPub = new PgpPublicKeyRingBundle(inputStream);

            // we just loop through the collection till we find a key suitable for encryption, in the real
            // world you would probably want to be a bit smarter about this.
            // iterate through the key rings.
            foreach (PgpPublicKeyRing kRing in pgpPub.GetKeyRings())
            {
                foreach (PgpPublicKey k in kRing.GetPublicKeys())
                {
                    if (k.IsEncryptionKey)
                        return k;
                }
            }

            throw new ArgumentException("Can't find encryption key in key ring.");
        }

        /*
        * Search a secret key ring collection for a secret key corresponding to keyId if it exists.
        */

        private PgpPrivateKey FindSecretKey(PgpSecretKeyRingBundle pgpSec, long keyId, char[] pass)
        {
            PgpSecretKey pgpSecKey = pgpSec.GetSecretKey(keyId);

            if (pgpSecKey == null)
                return null;

            return pgpSecKey.ExtractPrivateKey(pass);
        }

        #endregion Private helpers

        #region IDisposable Implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;

            // No unmanaged resources to dispose in this case
        }

        #endregion
    }
}
