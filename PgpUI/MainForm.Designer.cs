namespace PgpUI
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            pnlDropArea = new Panel();
            lblDropFiles = new Label();
            pictureBox1 = new PictureBox();
            lstFiles = new ListBox();
            lblFileCount = new Label();
            btnClearList = new Button();
            gbEncryption = new GroupBox();
            chkSign = new CheckBox();
            chkIntegrityCheck = new CheckBox();
            chkArmor = new CheckBox();
            btnEncrypt = new Button();
            gbDecryption = new GroupBox();
            txtPassPhrase = new TextBox();
            label1 = new Label();
            btnDecrypt = new Button();
            gbKeys = new GroupBox();
            lnkClearPrivateKey = new LinkLabel();
            lnkClearPublicKey = new LinkLabel();
            lblPrivateKey = new Label();
            btnSelectPrivateKey = new Button();
            lblPublicKey = new Label();
            btnSelectPublicKey = new Button();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            toolTip = new ToolTip(components);
            pnlDropArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            gbEncryption.SuspendLayout();
            gbDecryption.SuspendLayout();
            gbKeys.SuspendLayout();
            SuspendLayout();
            // 
            // pnlDropArea
            // 
            pnlDropArea.AllowDrop = true;
            pnlDropArea.BackColor = Color.FromArgb(240, 245, 249);
            pnlDropArea.BorderStyle = BorderStyle.FixedSingle;
            pnlDropArea.Controls.Add(lblDropFiles);
            pnlDropArea.Controls.Add(pictureBox1);
            pnlDropArea.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pnlDropArea.Location = new Point(25, 25);
            pnlDropArea.Margin = new Padding(5);
            pnlDropArea.Name = "pnlDropArea";
            pnlDropArea.Size = new Size(1045, 180);
            pnlDropArea.TabIndex = 0;
            // 
            // lblDropFiles
            // 
            lblDropFiles.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDropFiles.ForeColor = Color.FromArgb(66, 133, 244);
            lblDropFiles.Location = new Point(200, 61);
            lblDropFiles.Margin = new Padding(4, 0, 4, 0);
            lblDropFiles.Name = "lblDropFiles";
            lblDropFiles.Size = new Size(802, 60);
            lblDropFiles.TabIndex = 2;
            lblDropFiles.Text = "Drag and drop files here to encrypt or decrypt";
            lblDropFiles.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(135, 50);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(60, 60);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // lstFiles
            // 
            lstFiles.BackColor = Color.White;
            lstFiles.BorderStyle = BorderStyle.FixedSingle;
            lstFiles.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lstFiles.FormattingEnabled = true;
            lstFiles.ItemHeight = 36;
            lstFiles.Location = new Point(25, 230);
            lstFiles.Margin = new Padding(5);
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(1045, 182);
            lstFiles.TabIndex = 1;
            // 
            // lblFileCount
            // 
            lblFileCount.AutoSize = true;
            lblFileCount.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblFileCount.ForeColor = Color.FromArgb(96, 96, 96);
            lblFileCount.Location = new Point(30, 450);
            lblFileCount.Margin = new Padding(5, 0, 5, 0);
            lblFileCount.Name = "lblFileCount";
            lblFileCount.Size = new Size(202, 37);
            lblFileCount.TabIndex = 2;
            lblFileCount.Text = "0 file(s) selected";
            // 
            // btnClearList
            // 
            btnClearList.BackColor = Color.FromArgb(245, 245, 245);
            btnClearList.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnClearList.FlatStyle = FlatStyle.Flat;
            btnClearList.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnClearList.ForeColor = Color.FromArgb(96, 96, 96);
            btnClearList.Location = new Point(920, 440);
            btnClearList.Margin = new Padding(5);
            btnClearList.Name = "btnClearList";
            btnClearList.Size = new Size(150, 50);
            btnClearList.TabIndex = 3;
            btnClearList.Text = "Clear List";
            btnClearList.UseVisualStyleBackColor = false;
            btnClearList.Click += btnClearList_Click;
            // 
            // gbEncryption
            // 
            gbEncryption.BackColor = Color.White;
            gbEncryption.Controls.Add(chkSign);
            gbEncryption.Controls.Add(chkIntegrityCheck);
            gbEncryption.Controls.Add(chkArmor);
            gbEncryption.Controls.Add(btnEncrypt);
            gbEncryption.Font = new Font("Segoe UI Semibold", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbEncryption.ForeColor = Color.FromArgb(66, 66, 66);
            gbEncryption.Location = new Point(25, 500);
            gbEncryption.Margin = new Padding(5);
            gbEncryption.Name = "gbEncryption";
            gbEncryption.Padding = new Padding(5);
            gbEncryption.Size = new Size(510, 255);
            gbEncryption.TabIndex = 4;
            gbEncryption.TabStop = false;
            gbEncryption.Text = "Encryption Options";
            // 
            // chkSign
            // 
            chkSign.AutoSize = true;
            chkSign.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkSign.ForeColor = Color.FromArgb(66, 66, 66);
            chkSign.Location = new Point(230, 45);
            chkSign.Margin = new Padding(5);
            chkSign.Name = "chkSign";
            chkSign.Size = new Size(99, 41);
            chkSign.TabIndex = 3;
            chkSign.Text = "Sign";
            chkSign.UseVisualStyleBackColor = true;
            // 
            // chkIntegrityCheck
            // 
            chkIntegrityCheck.AutoSize = true;
            chkIntegrityCheck.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkIntegrityCheck.ForeColor = Color.FromArgb(66, 66, 66);
            chkIntegrityCheck.Location = new Point(230, 95);
            chkIntegrityCheck.Margin = new Padding(5);
            chkIntegrityCheck.Name = "chkIntegrityCheck";
            chkIntegrityCheck.Size = new Size(231, 41);
            chkIntegrityCheck.TabIndex = 2;
            chkIntegrityCheck.Text = "Integrity Check";
            chkIntegrityCheck.UseVisualStyleBackColor = true;
            // 
            // chkArmor
            // 
            chkArmor.AutoSize = true;
            chkArmor.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkArmor.ForeColor = Color.FromArgb(66, 66, 66);
            chkArmor.Location = new Point(30, 45);
            chkArmor.Margin = new Padding(5);
            chkArmor.Name = "chkArmor";
            chkArmor.Size = new Size(123, 41);
            chkArmor.TabIndex = 1;
            chkArmor.Text = "Armor";
            chkArmor.UseVisualStyleBackColor = true;
            // 
            // btnEncrypt
            // 
            btnEncrypt.BackColor = Color.FromArgb(66, 133, 244);
            btnEncrypt.FlatAppearance.BorderSize = 0;
            btnEncrypt.FlatStyle = FlatStyle.Flat;
            btnEncrypt.Font = new Font("Segoe UI Semibold", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEncrypt.ForeColor = Color.White;
            btnEncrypt.Location = new Point(30, 150);
            btnEncrypt.Margin = new Padding(5);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(380, 63);
            btnEncrypt.TabIndex = 0;
            btnEncrypt.Text = "Encrypt Files";
            btnEncrypt.UseVisualStyleBackColor = false;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // gbDecryption
            // 
            gbDecryption.BackColor = Color.White;
            gbDecryption.Controls.Add(txtPassPhrase);
            gbDecryption.Controls.Add(label1);
            gbDecryption.Controls.Add(btnDecrypt);
            gbDecryption.Font = new Font("Segoe UI Semibold", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbDecryption.ForeColor = Color.FromArgb(66, 66, 66);
            gbDecryption.Location = new Point(569, 500);
            gbDecryption.Margin = new Padding(5);
            gbDecryption.Name = "gbDecryption";
            gbDecryption.Padding = new Padding(5);
            gbDecryption.Size = new Size(501, 255);
            gbDecryption.TabIndex = 5;
            gbDecryption.TabStop = false;
            gbDecryption.Text = "Decryption";
            // 
            // txtPassPhrase
            // 
            txtPassPhrase.BorderStyle = BorderStyle.FixedSingle;
            txtPassPhrase.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPassPhrase.Location = new Point(241, 43);
            txtPassPhrase.Margin = new Padding(5);
            txtPassPhrase.Name = "txtPassPhrase";
            txtPassPhrase.PasswordChar = '•';
            txtPassPhrase.Size = new Size(250, 44);
            txtPassPhrase.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(66, 66, 66);
            label1.Location = new Point(25, 50);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(151, 37);
            label1.TabIndex = 1;
            label1.Text = "Passphrase:";
            // 
            // btnDecrypt
            // 
            btnDecrypt.BackColor = Color.FromArgb(66, 133, 244);
            btnDecrypt.FlatAppearance.BorderSize = 0;
            btnDecrypt.FlatStyle = FlatStyle.Flat;
            btnDecrypt.Font = new Font("Segoe UI Semibold", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDecrypt.ForeColor = Color.White;
            btnDecrypt.Location = new Point(30, 150);
            btnDecrypt.Margin = new Padding(5);
            btnDecrypt.Name = "btnDecrypt";
            btnDecrypt.Size = new Size(380, 63);
            btnDecrypt.TabIndex = 0;
            btnDecrypt.Text = "Decrypt Files";
            btnDecrypt.UseVisualStyleBackColor = false;
            btnDecrypt.Click += btnDecrypt_Click;
            // 
            // gbKeys
            // 
            gbKeys.BackColor = Color.White;
            gbKeys.Controls.Add(lnkClearPrivateKey);
            gbKeys.Controls.Add(lnkClearPublicKey);
            gbKeys.Controls.Add(lblPrivateKey);
            gbKeys.Controls.Add(btnSelectPrivateKey);
            gbKeys.Controls.Add(lblPublicKey);
            gbKeys.Controls.Add(btnSelectPublicKey);
            gbKeys.Font = new Font("Segoe UI Semibold", 10.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            gbKeys.ForeColor = Color.FromArgb(66, 66, 66);
            gbKeys.Location = new Point(25, 767);
            gbKeys.Margin = new Padding(5);
            gbKeys.Name = "gbKeys";
            gbKeys.Padding = new Padding(5);
            gbKeys.Size = new Size(1045, 210);
            gbKeys.TabIndex = 6;
            gbKeys.TabStop = false;
            gbKeys.Text = "Key Files";
            // 
            // lnkClearPrivateKey
            // 
            lnkClearPrivateKey.ActiveLinkColor = Color.FromArgb(200, 66, 133, 244);
            lnkClearPrivateKey.AutoSize = true;
            lnkClearPrivateKey.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkClearPrivateKey.LinkColor = Color.FromArgb(66, 133, 244);
            lnkClearPrivateKey.Location = new Point(895, 134);
            lnkClearPrivateKey.Margin = new Padding(5, 0, 5, 0);
            lnkClearPrivateKey.Name = "lnkClearPrivateKey";
            lnkClearPrivateKey.Size = new Size(79, 37);
            lnkClearPrivateKey.TabIndex = 5;
            lnkClearPrivateKey.TabStop = true;
            lnkClearPrivateKey.Text = "Clear";
            lnkClearPrivateKey.LinkClicked += lnkClearPrivateKey_LinkClicked;
            // 
            // lnkClearPublicKey
            // 
            lnkClearPublicKey.ActiveLinkColor = Color.FromArgb(200, 66, 133, 244);
            lnkClearPublicKey.AutoSize = true;
            lnkClearPublicKey.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lnkClearPublicKey.LinkColor = Color.FromArgb(66, 133, 244);
            lnkClearPublicKey.Location = new Point(895, 45);
            lnkClearPublicKey.Margin = new Padding(5, 0, 5, 0);
            lnkClearPublicKey.Name = "lnkClearPublicKey";
            lnkClearPublicKey.Size = new Size(79, 37);
            lnkClearPublicKey.TabIndex = 4;
            lnkClearPublicKey.TabStop = true;
            lnkClearPublicKey.Text = "Clear";
            lnkClearPublicKey.LinkClicked += lnkClearPublicKey_LinkClicked;
            // 
            // lblPrivateKey
            // 
            lblPrivateKey.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPrivateKey.ForeColor = Color.FromArgb(150, 150, 150);
            lblPrivateKey.Location = new Point(250, 139);
            lblPrivateKey.Margin = new Padding(5, 0, 5, 0);
            lblPrivateKey.Name = "lblPrivateKey";
            lblPrivateKey.Size = new Size(530, 61);
            lblPrivateKey.TabIndex = 3;
            lblPrivateKey.Text = "No private key selected";
            lblPrivateKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSelectPrivateKey
            // 
            btnSelectPrivateKey.BackColor = Color.FromArgb(245, 245, 245);
            btnSelectPrivateKey.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnSelectPrivateKey.FlatStyle = FlatStyle.Flat;
            btnSelectPrivateKey.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSelectPrivateKey.ForeColor = Color.FromArgb(66, 66, 66);
            btnSelectPrivateKey.Location = new Point(30, 134);
            btnSelectPrivateKey.Margin = new Padding(5);
            btnSelectPrivateKey.Name = "btnSelectPrivateKey";
            btnSelectPrivateKey.Size = new Size(200, 66);
            btnSelectPrivateKey.TabIndex = 2;
            btnSelectPrivateKey.Text = "Private Key";
            btnSelectPrivateKey.UseVisualStyleBackColor = false;
            btnSelectPrivateKey.Click += btnSelectPrivateKey_Click;
            // 
            // lblPublicKey
            // 
            lblPublicKey.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblPublicKey.ForeColor = Color.FromArgb(150, 150, 150);
            lblPublicKey.Location = new Point(250, 50);
            lblPublicKey.Margin = new Padding(5, 0, 5, 0);
            lblPublicKey.Name = "lblPublicKey";
            lblPublicKey.Size = new Size(530, 57);
            lblPublicKey.TabIndex = 1;
            lblPublicKey.Text = "No public key selected";
            lblPublicKey.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSelectPublicKey
            // 
            btnSelectPublicKey.BackColor = Color.FromArgb(245, 245, 245);
            btnSelectPublicKey.FlatAppearance.BorderColor = Color.FromArgb(224, 224, 224);
            btnSelectPublicKey.FlatStyle = FlatStyle.Flat;
            btnSelectPublicKey.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSelectPublicKey.ForeColor = Color.FromArgb(66, 66, 66);
            btnSelectPublicKey.Location = new Point(30, 45);
            btnSelectPublicKey.Margin = new Padding(5);
            btnSelectPublicKey.Name = "btnSelectPublicKey";
            btnSelectPublicKey.Size = new Size(200, 67);
            btnSelectPublicKey.TabIndex = 0;
            btnSelectPublicKey.Text = "Public Key";
            btnSelectPublicKey.UseVisualStyleBackColor = false;
            btnSelectPublicKey.Click += btnSelectPublicKey_Click;
            // 
            // progressBar
            // 
            progressBar.BackColor = Color.White;
            progressBar.ForeColor = Color.FromArgb(66, 133, 244);
            progressBar.Location = new Point(25, 987);
            progressBar.Margin = new Padding(5);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(1045, 35);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 7;
            // 
            // lblStatus
            // 
            lblStatus.Font = new Font("Segoe UI", 10.125F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStatus.ForeColor = Color.FromArgb(96, 96, 96);
            lblStatus.Location = new Point(25, 1032);
            lblStatus.Margin = new Padding(5, 0, 5, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(1045, 40);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            BackColor = Color.FromArgb(250, 250, 250);
            ClientSize = new Size(1101, 1100);
            Controls.Add(lblStatus);
            Controls.Add(progressBar);
            Controls.Add(gbKeys);
            Controls.Add(gbDecryption);
            Controls.Add(gbEncryption);
            Controls.Add(btnClearList);
            Controls.Add(lblFileCount);
            Controls.Add(lstFiles);
            Controls.Add(pnlDropArea);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(5);
            MaximizeBox = false;
            Name = "MainForm";
            Padding = new Padding(25);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "PGP Encryption Tool";
            FormClosing += MainForm_FormClosing;
            pnlDropArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            gbEncryption.ResumeLayout(false);
            gbEncryption.PerformLayout();
            gbDecryption.ResumeLayout(false);
            gbDecryption.PerformLayout();
            gbKeys.ResumeLayout(false);
            gbKeys.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        private System.Windows.Forms.Panel pnlDropArea;
        private System.Windows.Forms.Label lblDropFiles;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox lstFiles;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Button btnClearList;
        private System.Windows.Forms.GroupBox gbEncryption;
        private System.Windows.Forms.CheckBox chkSign;
        private System.Windows.Forms.CheckBox chkIntegrityCheck;
        private System.Windows.Forms.CheckBox chkArmor;
        private System.Windows.Forms.Button btnEncrypt;
        private System.Windows.Forms.GroupBox gbDecryption;
        private System.Windows.Forms.TextBox txtPassPhrase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.GroupBox gbKeys;
        private System.Windows.Forms.LinkLabel lnkClearPrivateKey;
        private System.Windows.Forms.LinkLabel lnkClearPublicKey;
        private System.Windows.Forms.Label lblPrivateKey;
        private System.Windows.Forms.Button btnSelectPrivateKey;
        private System.Windows.Forms.Label lblPublicKey;
        private System.Windows.Forms.Button btnSelectPublicKey;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolTip toolTip;
    }
}