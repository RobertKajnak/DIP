using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    public partial class FormMain : Form
    {
        private static FileLogic fileLogic;
        private static NetLogic netLogic;
        private static bool isFile;

        public FormMain()
        {
            this.MaximizeBox = false;
            InitializeComponent();

            fileLogic = new FileLogic(this);
            netLogic = new NetLogic(this, fileLogic);
            try {
                Logger.Start("Program");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.InnerException.Message);
            }

            AddHostIP("Fetching IP adress...");
            Thread getIPThread = new Thread(() =>
            {
                string ip = netLogic.GetHostIPAddress();
                Delegate addIPDel = (MethodInvoker)(delegate () { this.AddHostIP(ip); });
                this.Invoke(addIPDel);
            });
            getIPThread.IsBackground = true;
            getIPThread.Start();
        }

        public void AddFile(string fileName)
        {
            textBoxFile.Text = fileName;
            this.BackColor = System.Drawing.SystemColors.ControlLight;

            Icon icon = Icon.ExtractAssociatedIcon(fileName);
            pictureBoxHost.Image = icon.ToBitmap();

            EnableFile();
        }

        public void AddHostIP(string ip)
        {
            textBoxIPHost.Text = ip;
        }

        public void AddTargetIP(string ip)
        {
            textBoxIPTarget.Text = ip;
        }

        public void PostMessage(string message, string title)
        {
            MessageBox.Show(message, title);
        }

        /** Private helper functions */
        protected string GetFileFromDialogBox()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }

        protected delegate void PostValidationResult_ReturnCall(string result);
        protected void PostValidatationResult(string result)
        { 
            labelConnectionResult.Visible = true;
            pictureBoxConnectionResult.Visible = true;
            buttonTestConnection.Enabled = true;

            if (!result.Equals(""))
            {
                pictureBoxConnectionResult.Image = global::DIP.Properties.Resources.tick;
                labelConnectionResult.Text = result;
                MessageBox.Show(result, "Connected");
            }
            else
            {
                labelConnectionResult.Text = "No Connection";
                pictureBoxConnectionResult.Image = global::DIP.Properties.Resources.cross;
                MessageBox.Show("The ping to the specified IP timed out", "Ping failed");
            }
        }

        protected void PostValidationInProgress()
        {
            labelConnectionResult.Visible = true;
            pictureBoxConnectionResult.Visible = true;
            buttonTestConnection.Enabled = false;
            labelConnectionResult.Text = "Connecting";
            this.pictureBoxConnectionResult.Image = global::DIP.Properties.Resources.dots;
        }

        /** Keystroke and mouse events **/
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /** Graphical events **/
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            fileLogic.AddFile(GetFileFromDialogBox());
        }

        private void buttonTestConnection_Click(object sender, EventArgs e)
        {
            netLogic.AddTargetIPAddress(textBoxIPTarget.Text);

            new Thread(() =>
            {
                this.Invoke(new MethodInvoker(PostValidationInProgress));
                PostValidationResult_ReturnCall pvrc = new PostValidationResult_ReturnCall(PostValidatationResult);
                string result = null;
                try
                {
                    result = netLogic.Ping();
                }
                catch
                {
                    MessageBox.Show("An error occured while trying to connect to the target\nPlease check if the entered IP address is correct", "Error");
                }

                this.Invoke(pvrc, result);


            }).Start();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            
            new Thread(()=> {
                this.Invoke(new MethodInvoker(() =>{ textBoxIPTarget.Enabled = false; }) );
                try
                {
                    netLogic.SendFile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "A problem occured while transferring the file");
                    PostValidationResult_ReturnCall pvrc = new PostValidationResult_ReturnCall(PostValidatationResult);
                    this.Invoke(pvrc, "");
                }
                this.Invoke(new MethodInvoker(() => { textBoxIPTarget.Enabled = true; }));
            }).Start();
            
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ((string[])e.Data.GetData(DataFormats.FileDrop)).Length==1)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FormMain_DragLeave(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.SystemColors.ControlLight;
        }

        private void FormMain_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ((string[])e.Data.GetData(DataFormats.FileDrop)).Length == 1)
            {
                this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            }
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileName = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && fileName.Length == 1)
            {
                fileLogic.AddFile(fileName[0]);
            }
        }

        private void textBoxIPTarget_TextChanged(object sender, EventArgs e)
        {
            if (netLogic.isValidIP(textBoxIPTarget.Text))
            {
                netLogic.AddTargetIPAddress(textBoxIPTarget.Text);
                EnableConnection();
            }
            else
            {
                DisableConnection();
            }
           
        }

        private void EnableConnection()
        {
            buttonTestConnection.Enabled = true;
            if (isFile)
            {
                buttonSend.Enabled = true;
            }
        }

        private void DisableConnection()
        {
            buttonTestConnection.Enabled = false;
            buttonSend.Enabled = false;

        }
        
        private void EnableFile()
        {
            isFile = true;
            if (buttonTestConnection.Enabled)
            {
                buttonSend.Enabled = true;
            }
        }


    }
}
