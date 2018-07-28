namespace dotBurn
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.spSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tslblCancel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ofdSelectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.cbSerialPort = new System.Windows.Forms.ComboBox();
            this.btnConnectSerial = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDeviceInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSerialConnectionStatus = new System.Windows.Forms.Label();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb32bitAddress = new System.Windows.Forms.CheckBox();
            this.cbAddHeader = new System.Windows.Forms.CheckBox();
            this.cbCheckAfterWrite = new System.Windows.Forms.CheckBox();
            this.mtxtHeader = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbRomSize = new System.Windows.Forms.ComboBox();
            this.cbGenerateChecksum = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtChecksumMD5 = new System.Windows.Forms.TextBox();
            this.txtChecksumCRC32 = new System.Windows.Forms.TextBox();
            this.btnGenerateMD5 = new System.Windows.Forms.Button();
            this.btnGenerateCRC32 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // spSerialPort
            // 
            this.spSerialPort.BaudRate = 115200;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblStatus,
            this.tspbProgressBar,
            this.tslblCancel,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(398, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblStatus
            // 
            this.tslblStatus.Name = "tslblStatus";
            this.tslblStatus.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.tslblStatus.Size = new System.Drawing.Size(44, 17);
            this.tslblStatus.Text = "Ready";
            // 
            // tspbProgressBar
            // 
            this.tspbProgressBar.Name = "tspbProgressBar";
            this.tspbProgressBar.Size = new System.Drawing.Size(100, 16);
            this.tspbProgressBar.Visible = false;
            // 
            // tslblCancel
            // 
            this.tslblCancel.IsLink = true;
            this.tslblCancel.Name = "tslblCancel";
            this.tslblCancel.Size = new System.Drawing.Size(43, 17);
            this.tslblCancel.Text = "Cancel";
            this.tslblCancel.ToolTipText = "Cancel the current operation";
            this.tslblCancel.Visible = false;
            this.tslblCancel.VisitedLinkColor = System.Drawing.Color.Black;
            this.tslblCancel.Click += new System.EventHandler(this.tslblCancel_Click);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Enabled = false;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(339, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "noti, 2015";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ofdSelectFileDialog
            // 
            this.ofdSelectFileDialog.CheckFileExists = false;
            this.ofdSelectFileDialog.DefaultExt = "*.bin";
            this.ofdSelectFileDialog.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";
            this.ofdSelectFileDialog.RestoreDirectory = true;
            this.ofdSelectFileDialog.Title = "Select rom file";
            // 
            // cbSerialPort
            // 
            this.cbSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSerialPort.FormattingEnabled = true;
            this.cbSerialPort.Location = new System.Drawing.Point(6, 19);
            this.cbSerialPort.Name = "cbSerialPort";
            this.cbSerialPort.Size = new System.Drawing.Size(193, 21);
            this.cbSerialPort.TabIndex = 5;
            this.cbSerialPort.Tag = "Select the serial port the device is connected to";
            this.cbSerialPort.DropDown += new System.EventHandler(this.cbSerialPort_DropDown);
            this.cbSerialPort.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // btnConnectSerial
            // 
            this.btnConnectSerial.Location = new System.Drawing.Point(205, 17);
            this.btnConnectSerial.Name = "btnConnectSerial";
            this.btnConnectSerial.Size = new System.Drawing.Size(75, 23);
            this.btnConnectSerial.TabIndex = 6;
            this.btnConnectSerial.Tag = "Connect to the device";
            this.btnConnectSerial.Text = "Connect";
            this.btnConnectSerial.UseVisualStyleBackColor = true;
            this.btnConnectSerial.Click += new System.EventHandler(this.btnConnectSerial_Click);
            this.btnConnectSerial.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDeviceInfo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblSerialConnectionStatus);
            this.groupBox1.Controls.Add(this.btnConnectSerial);
            this.groupBox1.Controls.Add(this.cbSerialPort);
            this.groupBox1.Location = new System.Drawing.Point(12, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 68);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Connection";
            this.groupBox1.Enter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // lblDeviceInfo
            // 
            this.lblDeviceInfo.AutoEllipsis = true;
            this.lblDeviceInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDeviceInfo.Location = new System.Drawing.Point(70, 47);
            this.lblDeviceInfo.Name = "lblDeviceInfo";
            this.lblDeviceInfo.Size = new System.Drawing.Size(293, 14);
            this.lblDeviceInfo.TabIndex = 9;
            this.lblDeviceInfo.Tag = "Device information";
            this.lblDeviceInfo.Text = "Unknown";
            this.lblDeviceInfo.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Device info:";
            // 
            // lblSerialConnectionStatus
            // 
            this.lblSerialConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblSerialConnectionStatus.Location = new System.Drawing.Point(286, 22);
            this.lblSerialConnectionStatus.Name = "lblSerialConnectionStatus";
            this.lblSerialConnectionStatus.Size = new System.Drawing.Size(78, 13);
            this.lblSerialConnectionStatus.TabIndex = 7;
            this.lblSerialConnectionStatus.Tag = "Connection status";
            this.lblSerialConnectionStatus.Text = "Not connected";
            this.lblSerialConnectionStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSerialConnectionStatus.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(6, 19);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(277, 20);
            this.txtFileName.TabIndex = 2;
            this.txtFileName.Tag = "Enter a filename to use";
            this.txtFileName.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(289, 17);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Tag = "Select a file to use";
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            this.btnBrowse.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(229, 386);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 25;
            this.btnRead.Tag = "Read the specified amount of bytes from the device";
            this.btnRead.Text = "Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            this.btnRead.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(310, 386);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 26;
            this.btnWrite.Tag = "Write the specified amount of bytes to the device";
            this.btnWrite.Text = "Write";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            this.btnWrite.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtFileName);
            this.groupBox2.Controls.Add(this.btnBrowse);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 51);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ROM file";
            this.groupBox2.Enter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblFileSize);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cb32bitAddress);
            this.groupBox3.Controls.Add(this.cbAddHeader);
            this.groupBox3.Controls.Add(this.cbCheckAfterWrite);
            this.groupBox3.Controls.Add(this.mtxtHeader);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.cbRomSize);
            this.groupBox3.Controls.Add(this.cbGenerateChecksum);
            this.groupBox3.Location = new System.Drawing.Point(12, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(373, 139);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Options";
            this.groupBox3.Enter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // lblFileSize
            // 
            this.lblFileSize.Location = new System.Drawing.Point(278, 22);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(86, 13);
            this.lblFileSize.TabIndex = 20;
            this.lblFileSize.Tag = "0 bytes";
            this.lblFileSize.Text = "0,0 KByte";
            this.lblFileSize.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(234, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "File size:";
            // 
            // cb32bitAddress
            // 
            this.cb32bitAddress.AutoSize = true;
            this.cb32bitAddress.Checked = true;
            this.cb32bitAddress.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb32bitAddress.Location = new System.Drawing.Point(6, 118);
            this.cb32bitAddress.Name = "cb32bitAddress";
            this.cb32bitAddress.Size = new System.Drawing.Size(111, 17);
            this.cb32bitAddress.TabIndex = 18;
            this.cb32bitAddress.Tag = "Use 16 or 32 bit address";
            this.cb32bitAddress.Text = "Use 32bit address";
            this.cb32bitAddress.UseVisualStyleBackColor = true;
            this.cb32bitAddress.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // cbAddHeader
            // 
            this.cbAddHeader.AutoSize = true;
            this.cbAddHeader.Location = new System.Drawing.Point(6, 48);
            this.cbAddHeader.Name = "cbAddHeader";
            this.cbAddHeader.Size = new System.Drawing.Size(84, 17);
            this.cbAddHeader.TabIndex = 14;
            this.cbAddHeader.Tag = "Insert a header into the file";
            this.cbAddHeader.Text = "Add header:";
            this.cbAddHeader.UseVisualStyleBackColor = true;
            this.cbAddHeader.CheckedChanged += new System.EventHandler(this.cbAddHeader_CheckedChanged);
            this.cbAddHeader.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // cbCheckAfterWrite
            // 
            this.cbCheckAfterWrite.AutoSize = true;
            this.cbCheckAfterWrite.Location = new System.Drawing.Point(6, 95);
            this.cbCheckAfterWrite.Name = "cbCheckAfterWrite";
            this.cbCheckAfterWrite.Size = new System.Drawing.Size(106, 17);
            this.cbCheckAfterWrite.TabIndex = 17;
            this.cbCheckAfterWrite.Tag = "Re-reads data from the device after writing it";
            this.cbCheckAfterWrite.Text = "Check after write";
            this.cbCheckAfterWrite.UseVisualStyleBackColor = true;
            this.cbCheckAfterWrite.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // mtxtHeader
            // 
            this.mtxtHeader.AsciiOnly = true;
            this.mtxtHeader.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mtxtHeader.Enabled = false;
            this.mtxtHeader.Location = new System.Drawing.Point(93, 46);
            this.mtxtHeader.Mask = "AA AA AA AA AA AA AA AA AA AA AA AA AA AA AA AA";
            this.mtxtHeader.Name = "mtxtHeader";
            this.mtxtHeader.PromptChar = ' ';
            this.mtxtHeader.Size = new System.Drawing.Size(271, 20);
            this.mtxtHeader.TabIndex = 15;
            this.mtxtHeader.TabStop = false;
            this.mtxtHeader.Tag = "Specify the header in hex";
            this.mtxtHeader.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(182, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "KByte";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Read/Write size:";
            // 
            // cbRomSize
            // 
            this.cbRomSize.FormattingEnabled = true;
            this.cbRomSize.Items.AddRange(new object[] {
            "4",
            "10",
            "16",
            "24",
            "32",
            "40",
            "48",
            "64",
            "80",
            "96",
            "128",
            "136",
            "160",
            "192",
            "256",
            "320",
            "384",
            "512",
            "640",
            "768",
            "1024",
            "2048"});
            this.cbRomSize.Location = new System.Drawing.Point(99, 19);
            this.cbRomSize.Name = "cbRomSize";
            this.cbRomSize.Size = new System.Drawing.Size(77, 21);
            this.cbRomSize.TabIndex = 12;
            this.cbRomSize.Tag = "The size of the data to read from/write to the device";
            this.cbRomSize.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // cbGenerateChecksum
            // 
            this.cbGenerateChecksum.AutoSize = true;
            this.cbGenerateChecksum.Location = new System.Drawing.Point(6, 72);
            this.cbGenerateChecksum.Name = "cbGenerateChecksum";
            this.cbGenerateChecksum.Size = new System.Drawing.Size(122, 17);
            this.cbGenerateChecksum.TabIndex = 16;
            this.cbGenerateChecksum.Tag = "Generates the checksums after a read operation";
            this.cbGenerateChecksum.Text = "Generate checksum";
            this.cbGenerateChecksum.UseVisualStyleBackColor = true;
            this.cbGenerateChecksum.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtChecksumMD5);
            this.groupBox4.Controls.Add(this.txtChecksumCRC32);
            this.groupBox4.Controls.Add(this.btnGenerateMD5);
            this.groupBox4.Controls.Add(this.btnGenerateCRC32);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Location = new System.Drawing.Point(12, 288);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(373, 79);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Checksum";
            this.groupBox4.Enter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // txtChecksumMD5
            // 
            this.txtChecksumMD5.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtChecksumMD5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChecksumMD5.Location = new System.Drawing.Point(53, 48);
            this.txtChecksumMD5.Name = "txtChecksumMD5";
            this.txtChecksumMD5.ReadOnly = true;
            this.txtChecksumMD5.Size = new System.Drawing.Size(230, 20);
            this.txtChecksumMD5.TabIndex = 23;
            this.txtChecksumMD5.TabStop = false;
            this.txtChecksumMD5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtChecksumCRC32
            // 
            this.txtChecksumCRC32.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtChecksumCRC32.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtChecksumCRC32.Location = new System.Drawing.Point(53, 21);
            this.txtChecksumCRC32.Name = "txtChecksumCRC32";
            this.txtChecksumCRC32.ReadOnly = true;
            this.txtChecksumCRC32.Size = new System.Drawing.Size(230, 20);
            this.txtChecksumCRC32.TabIndex = 20;
            this.txtChecksumCRC32.TabStop = false;
            this.txtChecksumCRC32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGenerateMD5
            // 
            this.btnGenerateMD5.Location = new System.Drawing.Point(289, 47);
            this.btnGenerateMD5.Name = "btnGenerateMD5";
            this.btnGenerateMD5.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateMD5.TabIndex = 24;
            this.btnGenerateMD5.Tag = "Generate MD5 checksum for the selected file";
            this.btnGenerateMD5.Text = "Generate";
            this.btnGenerateMD5.UseVisualStyleBackColor = true;
            this.btnGenerateMD5.Click += new System.EventHandler(this.btnGenerateMD5_Click);
            this.btnGenerateMD5.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // btnGenerateCRC32
            // 
            this.btnGenerateCRC32.Location = new System.Drawing.Point(289, 20);
            this.btnGenerateCRC32.Name = "btnGenerateCRC32";
            this.btnGenerateCRC32.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateCRC32.TabIndex = 21;
            this.btnGenerateCRC32.Tag = "Generate CRC32 checksum for the selected file";
            this.btnGenerateCRC32.Text = "Generate";
            this.btnGenerateCRC32.UseVisualStyleBackColor = true;
            this.btnGenerateCRC32.Click += new System.EventHandler(this.btnGenerateCRC32_Click);
            this.btnGenerateCRC32.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "MD5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "CRC32";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 441);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnWrite);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnRead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "dotBurn";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.MouseEnter += new System.EventHandler(this._showTagInStatusBar);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort spSerialPort;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.OpenFileDialog ofdSelectFileDialog;
        private System.Windows.Forms.ComboBox cbSerialPort;
        private System.Windows.Forms.Button btnConnectSerial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label lblSerialConnectionStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MaskedTextBox mtxtHeader;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbRomSize;
        private System.Windows.Forms.CheckBox cbGenerateChecksum;
        private System.Windows.Forms.ToolStripStatusLabel tslblStatus;
        private System.Windows.Forms.ToolStripProgressBar tspbProgressBar;
        private System.Windows.Forms.CheckBox cbAddHeader;
        private System.Windows.Forms.CheckBox cbCheckAfterWrite;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnGenerateMD5;
        private System.Windows.Forms.Button btnGenerateCRC32;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblDeviceInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel tslblCancel;
        private System.Windows.Forms.TextBox txtChecksumMD5;
        private System.Windows.Forms.TextBox txtChecksumCRC32;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.CheckBox cb32bitAddress;
        private System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Label label4;
    }
}

