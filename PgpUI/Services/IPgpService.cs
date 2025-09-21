namespace PgpUI.Services
{
    public interface IPgpService : IDisposable
    {
        void EncryptFile(string inputFile, string outputFile, string publicKeyFile, bool armor, bool withIntegrityCheck);
        void EncryptAndSign(string inputFile, string outputFile, string publicKeyFile, string privateKeyFile, string passPhrase, bool armor);
        void Decrypt(string inputFile, string privateKeyFile, string passPhrase, string outputFile);
    }
}
