namespace DirectorySyncingTesting
{
    partial class DirectorySync
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
            this.RTBSyncDirectorys = new System.Windows.Forms.RichTextBox();
            this.BtnSync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RTBSyncDirectorys
            // 
            this.RTBSyncDirectorys.Dock = System.Windows.Forms.DockStyle.Top;
            this.RTBSyncDirectorys.Location = new System.Drawing.Point(0, 0);
            this.RTBSyncDirectorys.Name = "RTBSyncDirectorys";
            this.RTBSyncDirectorys.Size = new System.Drawing.Size(277, 93);
            this.RTBSyncDirectorys.TabIndex = 0;
            this.RTBSyncDirectorys.Text = "";
            // 
            // BtnSync
            // 
            this.BtnSync.Location = new System.Drawing.Point(92, 99);
            this.BtnSync.Name = "BtnSync";
            this.BtnSync.Size = new System.Drawing.Size(75, 23);
            this.BtnSync.TabIndex = 1;
            this.BtnSync.Text = "Sync";
            this.BtnSync.UseVisualStyleBackColor = true;
            this.BtnSync.Click += new System.EventHandler(this.BtnSync_Click);
            // 
            // DirectorySync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 145);
            this.Controls.Add(this.BtnSync);
            this.Controls.Add(this.RTBSyncDirectorys);
            this.Name = "DirectorySync";
            this.Text = "Sync";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox RTBSyncDirectorys;
        private System.Windows.Forms.Button BtnSync;
    }
}

