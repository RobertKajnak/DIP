namespace DIP
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.progressBarOutgoing = new System.Windows.Forms.ProgressBar();
            this.textBoxIPHost = new System.Windows.Forms.TextBox();
            this.textBoxIPTarget = new System.Windows.Forms.TextBox();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonTestConnection = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.progressBarIncoming = new System.Windows.Forms.ProgressBar();
            this.labelOutgoing = new System.Windows.Forms.Label();
            this.labelIncoming = new System.Windows.Forms.Label();
            this.labelConnectionResult = new System.Windows.Forms.Label();
            this.pictureBoxConnectionResult = new System.Windows.Forms.PictureBox();
            this.pictureBoxHost = new System.Windows.Forms.PictureBox();
            this.pictureBoxTarget = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectionResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHost)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTarget)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBarOutgoing
            // 
            resources.ApplyResources(this.progressBarOutgoing, "progressBarOutgoing");
            this.progressBarOutgoing.Name = "progressBarOutgoing";
            // 
            // textBoxIPHost
            // 
            resources.ApplyResources(this.textBoxIPHost, "textBoxIPHost");
            this.textBoxIPHost.Name = "textBoxIPHost";
            this.textBoxIPHost.ReadOnly = true;
            // 
            // textBoxIPTarget
            // 
            resources.ApplyResources(this.textBoxIPTarget, "textBoxIPTarget");
            this.textBoxIPTarget.Name = "textBoxIPTarget";
            this.textBoxIPTarget.TextChanged += new System.EventHandler(this.textBoxIPTarget_TextChanged);
            // 
            // textBoxFile
            // 
            this.textBoxFile.BackColor = System.Drawing.SystemColors.ControlLight;
            this.textBoxFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxFile, "textBoxFile");
            this.textBoxFile.Name = "textBoxFile";
            // 
            // buttonTestConnection
            // 
            resources.ApplyResources(this.buttonTestConnection, "buttonTestConnection");
            this.buttonTestConnection.Name = "buttonTestConnection";
            this.buttonTestConnection.UseVisualStyleBackColor = true;
            this.buttonTestConnection.Click += new System.EventHandler(this.buttonTestConnection_Click);
            // 
            // buttonSend
            // 
            resources.ApplyResources(this.buttonSend, "buttonSend");
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // progressBarIncoming
            // 
            resources.ApplyResources(this.progressBarIncoming, "progressBarIncoming");
            this.progressBarIncoming.Name = "progressBarIncoming";
            // 
            // labelOutgoing
            // 
            resources.ApplyResources(this.labelOutgoing, "labelOutgoing");
            this.labelOutgoing.Name = "labelOutgoing";
            // 
            // labelIncoming
            // 
            resources.ApplyResources(this.labelIncoming, "labelIncoming");
            this.labelIncoming.Name = "labelIncoming";
            // 
            // labelConnectionResult
            // 
            resources.ApplyResources(this.labelConnectionResult, "labelConnectionResult");
            this.labelConnectionResult.Name = "labelConnectionResult";
            // 
            // pictureBoxConnectionResult
            // 
            resources.ApplyResources(this.pictureBoxConnectionResult, "pictureBoxConnectionResult");
            this.pictureBoxConnectionResult.Image = global::DIP.Properties.Resources.dots;
            this.pictureBoxConnectionResult.InitialImage = global::DIP.Properties.Resources.dots;
            this.pictureBoxConnectionResult.Name = "pictureBoxConnectionResult";
            this.pictureBoxConnectionResult.TabStop = false;
            // 
            // pictureBoxHost
            // 
            resources.ApplyResources(this.pictureBoxHost, "pictureBoxHost");
            this.pictureBoxHost.Name = "pictureBoxHost";
            this.pictureBoxHost.TabStop = false;
            this.pictureBoxHost.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBoxTarget
            // 
            this.pictureBoxTarget.Image = global::DIP.Properties.Resources.computer;
            resources.ApplyResources(this.pictureBoxTarget, "pictureBoxTarget");
            this.pictureBoxTarget.InitialImage = global::DIP.Properties.Resources.computer;
            this.pictureBoxTarget.Name = "pictureBoxTarget";
            this.pictureBoxTarget.TabStop = false;
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.labelConnectionResult);
            this.Controls.Add(this.pictureBoxConnectionResult);
            this.Controls.Add(this.labelIncoming);
            this.Controls.Add(this.labelOutgoing);
            this.Controls.Add(this.progressBarIncoming);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonTestConnection);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.pictureBoxHost);
            this.Controls.Add(this.textBoxIPTarget);
            this.Controls.Add(this.textBoxIPHost);
            this.Controls.Add(this.pictureBoxTarget);
            this.Controls.Add(this.progressBarOutgoing);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormMain";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.FormMain_DragOver);
            this.DragLeave += new System.EventHandler(this.FormMain_DragLeave);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxConnectionResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHost)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTarget)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarOutgoing;
        private System.Windows.Forms.PictureBox pictureBoxTarget;
        private System.Windows.Forms.TextBox textBoxIPHost;
        private System.Windows.Forms.TextBox textBoxIPTarget;
        private System.Windows.Forms.PictureBox pictureBoxHost;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonTestConnection;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ProgressBar progressBarIncoming;
        private System.Windows.Forms.Label labelOutgoing;
        private System.Windows.Forms.Label labelIncoming;
        private System.Windows.Forms.PictureBox pictureBoxConnectionResult;
        private System.Windows.Forms.Label labelConnectionResult;
    }
}

