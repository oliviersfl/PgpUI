using PgpUI.Services;

namespace PgpUI
{
    public partial class MainForm : Form
    {
        private readonly IPgpService _pgpService;
        private string _publicKeyPath;
        private string _privateKeyPath;
        private string _passPhrase;

        public MainForm()
        {
            InitializeComponent();
            _pgpService = new PgpService(); // Assuming you have an implementation
            SetupDragDrop();
        }

        private void SetupDragDrop()
        {
            // Enable drag and drop for the main form and panel
            pnlDropArea.AllowDrop = true;

            pnlDropArea.DragEnter += PnlDropArea_DragEnter;
            pnlDropArea.DragDrop += PnlDropArea_DragDrop;
        }

        private void PnlDropArea_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
                pnlDropArea.BackColor = SystemColors.ControlDark;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void PnlDropArea_DragDrop(object sender, DragEventArgs e)
        {
            pnlDropArea.BackColor = SystemColors.ControlLight;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files)
            {
                if (File.Exists(file) && !lstFiles.Items.Contains(file))
                {
                    lstFiles.Items.Add(file);
                }
            }

            UpdateFileCount();
        }

        private void UpdateFileCount()
        {
            lblFileCount.Text = $"{lstFiles.Items.Count} file(s) selected";
        }

        private void btnSelectPublicKey_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Public Key Files (*.asc;*.pub)|*.asc;*.pub|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Public Key File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _publicKeyPath = openFileDialog.FileName;
                    lblPublicKey.Text = Path.GetFileName(_publicKeyPath);
                    lblPublicKey.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void btnSelectPrivateKey_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Private Key Files (*.asc;*.key)|*.asc;*.key|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Private Key File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _privateKeyPath = openFileDialog.FileName;
                    lblPrivateKey.Text = Path.GetFileName(_privateKeyPath);
                    lblPrivateKey.ForeColor = SystemColors.ControlText;
                }
            }
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_publicKeyPath))
            {
                MessageBox.Show("Please select a public key first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("Please drag and drop files to encrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProcessFiles(ProcessEncryption, "Encryption");
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_privateKeyPath))
            {
                MessageBox.Show("Please select a private key first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtPassPhrase.Text))
            {
                MessageBox.Show("Please enter the passphrase.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _passPhrase = txtPassPhrase.Text;

            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("Please drag and drop files to decrypt.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ProcessFiles(ProcessDecryption, "Decryption");
        }

        private void ProcessFiles(Action<string> processAction, string operation)
        {
            try
            {
                ToggleControls(false);
                progressBar.Value = 0;
                progressBar.Maximum = lstFiles.Items.Count;
                lblStatus.Text = $"{operation} in progress...";

                foreach (string filePath in lstFiles.Items)
                {
                    try
                    {
                        processAction(filePath);
                        progressBar.Value++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error processing {Path.GetFileName(filePath)}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                lblStatus.Text = $"{operation} completed successfully!";
                MessageBox.Show($"{operation} completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"{operation} failed!";
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleControls(true);
            }
        }

        private void ToggleControls(bool enable)
        {
            btnEncrypt.Enabled = enable;
            btnDecrypt.Enabled = enable;
            btnSelectPublicKey.Enabled = enable;
            btnSelectPrivateKey.Enabled = enable;
            btnClearList.Enabled = enable;
            txtPassPhrase.Enabled = enable;
            chkArmor.Enabled = enable;
            chkIntegrityCheck.Enabled = enable;
            chkSign.Enabled = enable;
        }

        private void ProcessEncryption(string inputFile)
        {
            string outputFile = GetOutputFilePath(inputFile, ".pgp");

            if (chkSign.Checked && !string.IsNullOrEmpty(_privateKeyPath) && !string.IsNullOrEmpty(txtPassPhrase.Text))
            {
                _pgpService.EncryptAndSign(
                    inputFile,
                    outputFile,
                    _publicKeyPath,
                    _privateKeyPath,
                    txtPassPhrase.Text,
                    chkArmor.Checked
                );
            }
            else
            {
                _pgpService.EncryptFile(
                    inputFile,
                    outputFile,
                    _publicKeyPath,
                    chkArmor.Checked,
                    chkIntegrityCheck.Checked
                );
            }
        }

        private void ProcessDecryption(string inputFile)
        {
            string outputFile = GetOutputFilePath(inputFile, ".decrypted");

            _pgpService.Decrypt(
                inputFile,
                _privateKeyPath,
                _passPhrase,
                outputFile
            );
        }

        private string GetOutputFilePath(string inputFile, string extension)
        {
            string directory = Path.GetDirectoryName(inputFile);
            string fileName = Path.GetFileNameWithoutExtension(inputFile);
            return Path.Combine(directory, fileName + extension);
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            lstFiles.Items.Clear();
            progressBar.Value = 0;
            UpdateFileCount();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _pgpService?.Dispose();
        }

        private void lnkClearPublicKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _publicKeyPath = null;
            lblPublicKey.Text = "No public key selected";
            lblPublicKey.ForeColor = SystemColors.GrayText;
        }

        private void lnkClearPrivateKey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _privateKeyPath = null;
            lblPrivateKey.Text = "No private key selected";
            lblPrivateKey.ForeColor = SystemColors.GrayText;
        }
    }
}
