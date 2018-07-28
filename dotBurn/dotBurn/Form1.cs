using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using DamienG.Security.Cryptography;
using System.Threading;
using System.Text.RegularExpressions;

namespace dotBurn
{
    public partial class frmMain : Form
    {
        public const string TEXT_CONNECTING = "Connecting...";
        public const string TEXT_CONNECT = "Connect";
        public const string TEXT_READY = "Ready";
        public const string TEXT_UNKNOWN = "Unknown";
        public const string DEVICENAME = "MEEPROMMER";
        public const string TEXT_WRITING = "Writing...";
        public const string TEXT_READING = "Reading...";
        public const string TEXT_REREADING = "Re-reading...";
        private bool _bToolStripLocked = false;

        private CancellationTokenSource _ctsCancellationTokenSource = new CancellationTokenSource();

        public frmMain()
        {
            InitializeComponent();
            _getSerialPorts();
            spSerialPort.ReadTimeout = 10000;
            tslblStatus.Text = TEXT_READY;
            lblDeviceInfo.Font = new Font(lblDeviceInfo.Font, FontStyle.Italic);
            lblDeviceInfo.Text = TEXT_UNKNOWN;
            txtFileName.Focus();
            this.Tag = TEXT_READY;
        }

        #region EventHandlers
        private void btnConnectSerial_Click(object sender, EventArgs e)
        {
            _connectToDevice();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _disconnectFormSerial();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            _readFromDevice();
        }

        private void cbSerialPort_DropDown(object sender, EventArgs e)
        {
            _getSerialPorts();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            _writeToDevice();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            _selectFile();
        }

        private void btnGenerateMD5_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txtFileName.Text))
                {
                    _displayChecksumMD5(File.ReadAllBytes(txtFileName.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating checksum: " + ex.Message + "", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateCRC32_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(txtFileName.Text))
                {
                    _displayChecksumCRC32(File.ReadAllBytes(txtFileName.Text));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while generating checksum: " + ex.Message + "", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tslblCancel_Click(object sender, EventArgs e)
        {
            _ctsCancellationTokenSource.Cancel();
        }

        private void cbAddHeader_CheckedChanged(object sender, EventArgs e)
        {
            mtxtHeader.Enabled = cbAddHeader.Checked;
            mtxtHeader.TabStop = cbAddHeader.Checked;
            if (cbAddHeader.Checked)
            {
                mtxtHeader.Focus();
            }
        }
        #endregion

        private void _getSerialPorts()
        {
            int iTMPSelectedItem = cbSerialPort.SelectedIndex;
            cbSerialPort.Items.Clear();

            ManagementObjectSearcher moSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SerialPort");
            foreach (ManagementObject mObject in moSearcher.Get())
            {
                cbSerialPort.Items.Add(new ComboboxItem(mObject["Name"].ToString(), mObject["DeviceID"].ToString()));
            }
            cbSerialPort.SelectedIndex = iTMPSelectedItem;
            if (-1 == cbSerialPort.SelectedIndex && cbSerialPort.Items.Count > 0)
            {
                cbSerialPort.SelectedIndex = 0;
            }
        }

        private void _connectToDevice()
        {
            try
            {
                if (!spSerialPort.IsOpen)
                {
                    if (-1 == cbSerialPort.SelectedIndex)
                    {
                        throw new Exception("Select a serial port first!");
                    }

                    spSerialPort.PortName = ((ComboboxItem)cbSerialPort.SelectedItem).Value;
                    // ui updates
                    lblSerialConnectionStatus.Text = TEXT_CONNECTING;
                    lblSerialConnectionStatus.ForeColor = Color.Black;
                    _disableControls();
                    _uiSerialConnecting();
                    // connection task setup
                    _ctsCancellationTokenSource = new CancellationTokenSource();
                    _ctsCancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(11)); // set timeout
                    CancellationToken ctCancellationToken = _ctsCancellationTokenSource.Token;
                    Task tskConnect = Task.Factory.StartNew(() =>
                    {
                        Task<string> Read = null;
                        try
                        {
                            // already cancelled?!
                            ctCancellationToken.ThrowIfCancellationRequested();

                            _setStatus(TEXT_CONNECTING, true, -1, true); // set the status with a marquee
                            // try connecting
                            Thread t = Thread.CurrentThread; // HACK - there is no other way to handle cancellation
                            using (ctCancellationToken.Register(t.Abort))
                            {
                                spSerialPort.Open();
                                //Thread.Sleep(15000); // test
                            }

                            if (spSerialPort.IsOpen)
                            { // ask the device for it's version
                                _writeLineToSerial("").Wait();
                                _writeLineToSerial("V").Wait();
                                Task.Delay(500).Wait(_ctsCancellationTokenSource.Token);
                                Read = _readStringFromSerial();
                                Read.Wait(10000, _ctsCancellationTokenSource.Token); // read, timeout after 10sec, cancel enabled
                                string sDeviceInfo = string.Empty;
                                if (Read.IsCompleted)
                                {
                                    sDeviceInfo = Read.Result;
                                }
                                if (sDeviceInfo.StartsWith(DEVICENAME))
                                { // this is the device we were looking for
                                    _uiSerialConnected(sDeviceInfo);
                                }
                                else
                                { // this is something else
                                    throw new Exception("Unknown device (\"" + sDeviceInfo + "\")");
                                }
                            }
                            else
                            {
                                _uiSerialNotConnected();
                            }
                        }
                        catch (ThreadAbortException tex)
                        {
                            // do nothing
                            _disconnectFormSerial();
                            _uiSerialNotConnected();
                        }
                        catch (OperationCanceledException cex)
                        {
                            _disconnectFormSerial();
                            _uiSerialNotConnected();
                            //MessageBox.Show("Operation cancelled!", "Cancelled!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        catch (Exception ex)
                        {
                            _disconnectFormSerial();
                            _uiSerialNotConnected();
                            MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }, _ctsCancellationTokenSource.Token).ContinueWith((t) =>
                    {
                        // reset
                        _setStatus(TEXT_READY, false, -1, false); // default status
                        _enableControls();
                    });
                }
                else
                {
                    _disconnectFormSerial();
                    _uiSerialNotConnected();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _writeToDevice()
        {
            try
            {
                string sFileName = txtFileName.Text;
                // check for the nescessary things
                if (!spSerialPort.IsOpen)
                {
                    throw new Exception("Connect to the device first!");
                }
                if (string.IsNullOrWhiteSpace(sFileName))
                {
                    throw new Exception("Filename cannot be empty!");
                }
                if (!File.Exists(sFileName))
                {
                    throw new FileNotFoundException("File \"" + sFileName + "\" does not exist!");
                }
                // get the contents of the file
                byte[] baFileContents = File.ReadAllBytes(sFileName);
                if (0 == baFileContents.Length)
                {
                    throw new Exception("File cannot be empty!");
                }
                // try parsing the size
                int iRomSize = _getROMSize();

                // set up the ui
                _disableControls();
                _setStatus(TEXT_WRITING, true, 0, true);
                // write task setup
                _ctsCancellationTokenSource = new CancellationTokenSource();
                CancellationToken ctCancellationToken = _ctsCancellationTokenSource.Token;
                // set up the task
                Task<int> itskWriteTask = Task<int>.Factory.StartNew(() =>
                {
                    int iPos = 0;
                    try
                    {
                        // already cancelled?!
                        ctCancellationToken.ThrowIfCancellationRequested();

                        do
                        {
                            // update the progress bar
                            int iProgressBarPos = (int)Math.Ceiling(((float)iPos / (float)iRomSize/*baFileContents.Length*/) * 100);
                            _setStatus(TEXT_WRITING, true, iProgressBarPos, true);
                            // cancelled?
                            ctCancellationToken.ThrowIfCancellationRequested();
                            // set up the buffer
                            byte[] baBuffer = new byte[1024];
                            Buffer.BlockCopy(baFileContents, iPos, baBuffer, 0, baBuffer.Length);
                            // send the write command (w,00000000,00000000)
                            _writeLineToSerial("w," + _intToAddress(iPos) + "," + _intToAddress(baBuffer.Length) + ",00").Wait(10000, _ctsCancellationTokenSource.Token);
                            // write to the serial port
                            _writeToSerial(baBuffer).Wait(10000, _ctsCancellationTokenSource.Token); // timeout and cancel
                            //_writeLineToSerial(_hashToString(baBuffer)).Wait(10000, _ctsCancellationTokenSource.Token); // TEST
                            iPos += baBuffer.Length;
                        } while (iPos < iRomSize /*baFileContents.Length*/);

                        // check after write
                        if (cbAddHeader.Checked)
                        {
                            // calculate the original checksum
                            string sOriginalChecksum = _generateChecksumCRC32(baFileContents);
                            // re-read the data
                            // send the read command (r,00000000,00000000)
                            _writeLineToSerial("r," + _intToAddress(0) + "," + _intToAddress(iRomSize) + ",00").Wait(10000, _ctsCancellationTokenSource.Token);

                            List<byte> lbBytes = new List<byte>();
                            bool bExit = false;
                            do
                            {
                                // update the progress bar
                                int iProgressBarPos = (int)Math.Ceiling(((float)lbBytes.Count / (float)iRomSize/*baFileContents.Length*/) * 100);
                                _setStatus(TEXT_REREADING, true, iProgressBarPos, true);
                                // do the read
                                Task<byte[]> tbaRead = _readFromSerial();
                                tbaRead.Wait(10000, _ctsCancellationTokenSource.Token);
                                lbBytes.AddRange(tbaRead.Result);
                                //int iCurrentByte = spSerialPort.ReadByte();
                                //lbBytes.Add((byte)iCurrentByte);
                                if (lbBytes.Count >= iRomSize)
                                {
                                    bExit = true;
                                }
                            } while (!bExit);
                            // trim it to the right size
                            byte[] baTMP = new byte[iRomSize];
                            Buffer.BlockCopy(lbBytes.ToArray(), 0, baTMP, 0, baTMP.Length);

                            // calculate the checksum for the re-read data
                            string sNewChecksum = _generateChecksumCRC32(baTMP);
                            if (0 != string.Compare(sOriginalChecksum, sNewChecksum))
                            {
                                throw new Exception("Write ok, but schecksums did not match!");
                            }
                        }
                    }
                    catch (OperationCanceledException cex)
                    {
                        //MessageBox.Show("Operation cancelled!", "Cancelled!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //throw cex;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return iPos;
                }, _ctsCancellationTokenSource.Token).ContinueWith((t) =>
                {
                    if (_ctsCancellationTokenSource.IsCancellationRequested)
                    {
                        MessageBox.Show("" + t.Result + "byte(s) written", "Cancelled!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (t.IsCompleted)
                    {
                        // done
                        MessageBox.Show("" + t.Result + "byte(s) written", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    _setStatus(TEXT_READY, false, 0, false);
                    _enableControls();

                    return 0;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _setStatus(TEXT_READY, false, 0, false);
                _enableControls();
            }
        }

        private void _readFromDevice()
        {
            try
            {
                // check for the nescessary things
                if (!spSerialPort.IsOpen)
                {
                    throw new Exception("Connect to the device first!");
                }
                if (string.IsNullOrWhiteSpace(txtFileName.Text))
                {
                    throw new Exception("Filename cannot be empty!");
                }
                // try parsing the size
                int iRomSize = _getROMSize();
                // is overwriting ok?
                if (File.Exists(txtFileName.Text) && DialogResult.Cancel == MessageBox.Show("File already exists! Overwrite?", "Overwrite file", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    return;
                }
                // check the header
                if (cbAddHeader.Checked)
                {
                    if (!Regex.IsMatch(mtxtHeader.Text.Replace(" ", ""), "^[0-9a-fA-F]+$"))
                    {
                        throw new ArgumentException("Invalid header format!");
                    }
                }

                // set up the ui
                _disableControls();
                _setStatus(TEXT_READING, true, 0, true);
                // write task setup
                _ctsCancellationTokenSource = new CancellationTokenSource();
                _ctsCancellationTokenSource.CancelAfter(-1);
                CancellationToken ctCancellationToken = _ctsCancellationTokenSource.Token;
                // set up the task
                Task<int> itskReadTask = Task<int>.Factory.StartNew(() =>
                {
                    int iBytesRead = 0;
                    try
                    {
                        // already cancelled?!
                        ctCancellationToken.ThrowIfCancellationRequested();

                        // send the read command (r,00000000,00000000)
                        _writeLineToSerial("r," + _intToAddress(0) + "," + _intToAddress(iRomSize) + ",00").Wait(10000, _ctsCancellationTokenSource.Token);

                        List<byte> lbBytes = new List<byte>();
                        bool bExit = false;
                        do
                        {
                            // update the progress bar
                            int iProgressBarPos = (int)Math.Ceiling(((float)lbBytes.Count / (float)iRomSize/*baFileContents.Length*/) * 100);
                            _setStatus(TEXT_READING, true, iProgressBarPos, true);
                            // do the read
                            Task<byte[]> tbaRead = _readFromSerial();
                            tbaRead.Wait(10000, _ctsCancellationTokenSource.Token);
                            lbBytes.AddRange(tbaRead.Result);
                            //int iCurrentByte = spSerialPort.ReadByte();
                            //lbBytes.Add((byte)iCurrentByte);
                            if (lbBytes.Count >= iRomSize)
                            {
                                bExit = true;
                            }
                        } while (!bExit);

                        // add the header
                        int iHeaderSize = ((cbAddHeader.Checked) ? 16 : 0);
                        byte[] baRet = new byte[iRomSize + iHeaderSize];
                        if (cbAddHeader.Checked)
                        {
                            string sHeader = mtxtHeader.Text.Replace(" ", "");
                            for (int i = 0; i < sHeader.Length; i += 2)
                            {
                                baRet[i / 2] = Convert.ToByte(sHeader.Substring(i, 2), 16);
                            }
                        }
                        // copty the read contents into the out buffer
                        Buffer.BlockCopy(lbBytes.ToArray(), 0, baRet, iHeaderSize, iRomSize);
                        // write the bytes into the file
                        File.WriteAllBytes(txtFileName.Text, baRet);

                        // 
                        byte[] baTMP = new byte[iRomSize];
                        Buffer.BlockCopy(lbBytes.ToArray(), 0, baTMP, 0, baTMP.Length);
                        // generate checksums if needed
                        if (cbGenerateChecksum.Checked)
                        {
                            _generateChecksumCRC32(baTMP);
                            _generateChecksumMD5(baTMP);
                        }
                        iBytesRead = baTMP.Length;
                    }
                    catch (OperationCanceledException cex)
                    {
                        //MessageBox.Show("Operation cancelled!", "Cancelled!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //throw cex;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return iBytesRead;
                }, _ctsCancellationTokenSource.Token).ContinueWith((t) =>
                {
                    if (_ctsCancellationTokenSource.IsCancellationRequested)
                    {
                        MessageBox.Show("" + t.Result + "byte(s) read", "Cancelled!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else if (t.IsCompleted)
                    {
                        // done
                        MessageBox.Show("" + t.Result + "byte(s) read", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    _setStatus(TEXT_READY, false, 0, false);
                    _enableControls();

                    return 0;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _setStatus(TEXT_READY, false, 0, false);
                _enableControls();
            }
        }

        private string _intToAddress(int input)
        {
            string sRet = string.Empty;

            int iAddressSize = 4;
            if (cb32bitAddress.Checked)
            {
                iAddressSize = 8;
            }
            sRet = input.ToString("X" + iAddressSize);

            return sRet;
        }

        private int _getROMSize()
        {
            int iRomSize = 0;
            if (!int.TryParse((string)cbRomSize.SelectedItem, out iRomSize))
            {
                throw new ArgumentException("Invalid size: \"" + cbRomSize.SelectedItem + "\"");
            }
            if (0 == iRomSize)
            {
                throw new ArgumentException("Size must be grater than zero!");
            }

            if (!cb32bitAddress.Checked && 64 < iRomSize)
            {
                throw new ArgumentException("32bit addressing must be enabled to work with ROM sizes larger than 64k!");
            }

            return (iRomSize * 1024)/* - 1*/;
        }

        private void _selectFile()
        {
            if (DialogResult.OK == ofdSelectFileDialog.ShowDialog())
            {
                txtFileName.Text = ofdSelectFileDialog.FileName;
                long lFileSize = 0;
                int iFileSize = 0;
                if (File.Exists(txtFileName.Text))
                {
                    lFileSize = new FileInfo(txtFileName.Text).Length;
                    iFileSize = (int)(lFileSize / 1024);
                    cbRomSize.SelectedItem = null;
                    cbRomSize.SelectedItem = iFileSize.ToString();
                }
                lblFileSize.Text = (lFileSize / 1024).ToString("0.0") + " KByte";
                lblFileSize.Tag = lFileSize + " byte";
                _displayChecksums(string.Empty, string.Empty, true);
            }
        }

        #region serial_helpers
        private async Task<string> _readStringFromSerial()
        {
            string sRet = string.Empty;
            byte[] baBytes = await _readFromSerial();

            if (0 < baBytes.Length)
            {
                sRet = Encoding.ASCII.GetString(baBytes);
            }

            return sRet;
        }

        private async Task<byte[]> _readFromSerial()
        {
            byte[] baRet = new byte[0];
            if (null != spSerialPort && spSerialPort.IsOpen)
            {
                byte[] baBuffer = new byte[4096];
                Task<int> tiReadTask = spSerialPort.BaseStream.ReadAsync(baBuffer, 0, baBuffer.Length);
                int iBytesRead = await tiReadTask;

                //string sData = Encoding.ASCII.GetString(baBuffer, 0, iBytesRead);
                baRet = new byte[iBytesRead];
                Buffer.BlockCopy(baBuffer, 0, baRet, 0, iBytesRead);
            }

            return baRet;
        }

        private async Task _writeStringToSerial(string data)
        {
            await _writeToSerial(Encoding.ASCII.GetBytes(data));
        }

        private async Task _writeLineToSerial(string data)
        {
            await _writeStringToSerial(data + "\r\n");
        }

        private async Task _writeToSerial(byte[] buffer)
        {
            if (null != spSerialPort && spSerialPort.IsOpen)
            {
                Task tWriteTask = spSerialPort.BaseStream.WriteAsync(buffer, 0, buffer.Length);
                await tWriteTask;
            }
        }

        private void _disconnectFormSerial()
        {
            if (null != spSerialPort && spSerialPort.IsOpen)
            {
                spSerialPort.Close();
            }
        }
        #endregion

        #region ui_modification
        private void _uiSerialConnecting()
        {
            lblSerialConnectionStatus.Text = TEXT_CONNECTING;
            lblSerialConnectionStatus.ForeColor = Color.Black;
            btnConnectSerial.Text = TEXT_CONNECT;
            cbSerialPort.Enabled = false;
            lblDeviceInfo.Text = "Unknown";
        }

        private void _uiSerialConnected(string sDeviceInfo)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { _uiSerialConnectedBase(sDeviceInfo); }));
            }
            else
            {
                _uiSerialConnectedBase(sDeviceInfo);
            }
        }

        private void _uiSerialConnectedBase(string sDeviceInfo)
        {
            lblSerialConnectionStatus.Text = "Connected";
            lblSerialConnectionStatus.ForeColor = Color.Green;
            btnConnectSerial.Text = "Disconnect";
            btnConnectSerial.Tag = "Disconnect from the device";
            cbSerialPort.Enabled = false;
            lblDeviceInfo.Font = new Font(lblDeviceInfo.Font, FontStyle.Regular);
            lblDeviceInfo.Text = sDeviceInfo;
        }

        private void _uiSerialNotConnected()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { _uiSerialNotConnectedBase(); }));
            }
            else
            {
                _uiSerialNotConnectedBase();
            }
        }

        private void _uiSerialNotConnectedBase()
        {
            lblSerialConnectionStatus.Text = "Not connected";
            lblSerialConnectionStatus.ForeColor = Color.Red;
            btnConnectSerial.Text = TEXT_CONNECT;
            btnConnectSerial.Tag = "Connect to the device";
            lblDeviceInfo.Font = new Font(lblDeviceInfo.Font, FontStyle.Italic);
            lblDeviceInfo.Text = TEXT_UNKNOWN;
            cbSerialPort.Enabled = true;
        }

        private void _disableControls()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { _disableControlsBase(); }));
            }
            else
            {
                _disableControlsBase();
            }
        }

        private void _disableControlsBase()
        {
            txtFileName.Enabled = false;
            btnBrowse.Enabled = false;
            //cbSerialPort.Enabled = false;
            btnConnectSerial.Enabled = false;
            cbRomSize.Enabled = false;
            cbAddHeader.Enabled = false;
            mtxtHeader.Enabled = false;
            cbGenerateChecksum.Enabled = false;
            cbCheckAfterWrite.Enabled = false;
            btnGenerateCRC32.Enabled = false;
            btnGenerateMD5.Enabled = false;
            btnRead.Enabled = false;
            btnWrite.Enabled = false;
            cb32bitAddress.Enabled = false;
        }

        private void _enableControls()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { _enableControlsBase(); }));
            }
            else
            {
                _enableControlsBase();
            }
        }

        private void _enableControlsBase()
        {
            txtFileName.Enabled = true;
            btnBrowse.Enabled = true;
            //cbSerialPort.Enabled = true;
            btnConnectSerial.Enabled = true;
            cbRomSize.Enabled = true;
            cbAddHeader.Enabled = true;
            if (cbAddHeader.Checked)
            {
                mtxtHeader.Enabled = true;
            }
            cbGenerateChecksum.Enabled = true;
            cbCheckAfterWrite.Enabled = true;
            btnGenerateCRC32.Enabled = true;
            btnGenerateMD5.Enabled = true;
            btnRead.Enabled = true;
            btnWrite.Enabled = true;
            cb32bitAddress.Enabled = true;
        }

        private void _setStatus(string message)
        {
            if (_bToolStripLocked)
            { // the progress bas is shown, so it won't be a good idea to overwrite it with a status message
                return;
            }
            _setStatus(message, false, 0, false);
        }

        private void _setStatus(string message, bool showProgressBar, int progressPosition, bool isCancellable)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { _setStatusBase(message, showProgressBar, progressPosition, isCancellable); }));
            }
            else
            {
                _setStatusBase(message, showProgressBar, progressPosition, isCancellable);
            }
        }

        private void _setStatusBase(string message, bool showProgressBar, int progressPosition, bool isCancellable)
        {
            _bToolStripLocked = showProgressBar;
            tslblStatus.Text = message;
            tspbProgressBar.Visible = showProgressBar;
            if (-1 == progressPosition)
            {
                tspbProgressBar.Style = ProgressBarStyle.Marquee;
            }
            if (progressPosition >= 0)
            {
                tspbProgressBar.Style = ProgressBarStyle.Continuous;
                tspbProgressBar.Value = progressPosition;
            }
            tslblCancel.Visible = isCancellable;
        }


        private void _showTagInStatusBar(object sender, EventArgs e)
        {
            if (null == sender)
            {
                return;
            }
            Control cSender = (Control)sender;
            string sTag = cSender.Tag as string;
            if (!string.IsNullOrWhiteSpace(sTag) && cSender.Enabled)
            {
                _setStatus(sTag);
            }
            else
            {
                _setStatus(TEXT_READY);
            }
        }

        private void _displayChecksums(string CRC32, string MD5, bool force)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => { _displayChecksumsBase(CRC32, MD5, force); }));
            }
            else
            {
                _displayChecksumsBase(CRC32, MD5, force);
            }
        }

        private void _displayChecksumsBase(string CRC32, string MD5, bool force)
        {
            if (force)
            {
                txtChecksumCRC32.Text = CRC32;
                txtChecksumMD5.Text = MD5;
            }
            else
            {
                if (!string.IsNullOrEmpty(CRC32))
                {
                    txtChecksumCRC32.Text = CRC32;
                }
                if (!string.IsNullOrEmpty(MD5))
                {
                    txtChecksumMD5.Text = MD5;
                }
            }
        }

        private void _displayChecksumMD5(byte[] buffer)
        {
            _displayChecksums(string.Empty, _generateChecksumMD5(buffer), false);
        }

        private void _displayChecksumCRC32(byte[] buffer)
        {
            _displayChecksums(_generateChecksumCRC32(buffer), string.Empty, false);
        }

        #endregion

        #region hash_functions
        private string _generateChecksumMD5(byte[] buffer)
        {
            using (MD5 md5hash = MD5.Create())
            {
                byte[] baHashData = md5hash.ComputeHash(buffer);
                return _hashToString(baHashData);
            }
        }

        private string _generateChecksumCRC32(byte[] buffer)
        {
            using (Crc32 crc32 = new Crc32())
            {
                byte[] baHashData = crc32.ComputeHash(buffer);
                return _hashToString(baHashData);
            }
        }

        private string _hashToString(byte[] data)
        {
            string sRet = string.Empty;
            foreach (byte b in data)
            {
                sRet += b.ToString("x2").ToLower();
            }

            return sRet;
        }
        #endregion

    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public ComboboxItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
