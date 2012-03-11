namespace AppInForm.UI.Controls
{
    partial class SessionsControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SessionsControl));
            this.tvSessions = new System.Windows.Forms.TreeView();
            this.ilTreeView = new System.Windows.Forms.ImageList(this.components);
            this.tsSessionManager = new System.Windows.Forms.ToolStrip();
            this.tsbNewFolder = new System.Windows.Forms.ToolStripButton();
            this.tsbNewSSHSession = new System.Windows.Forms.ToolStripButton();
            this.tsSessionManager.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvSessions
            // 
            this.tvSessions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvSessions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.tvSessions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvSessions.ImageIndex = 0;
            this.tvSessions.ImageList = this.ilTreeView;
            this.tvSessions.Location = new System.Drawing.Point(0, 43);
            this.tvSessions.Name = "tvSessions";
            this.tvSessions.SelectedImageIndex = 0;
            this.tvSessions.ShowRootLines = false;
            this.tvSessions.Size = new System.Drawing.Size(300, 277);
            this.tvSessions.TabIndex = 1;
            // 
            // ilTreeView
            // 
            this.ilTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilTreeView.ImageStream")));
            this.ilTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.ilTreeView.Images.SetKeyName(0, "database.png");
            this.ilTreeView.Images.SetKeyName(1, "folder.png");
            // 
            // tsSessionManager
            // 
            this.tsSessionManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNewFolder,
            this.tsbNewSSHSession});
            this.tsSessionManager.Location = new System.Drawing.Point(0, 18);
            this.tsSessionManager.Name = "tsSessionManager";
            this.tsSessionManager.Size = new System.Drawing.Size(300, 25);
            this.tsSessionManager.TabIndex = 2;
            this.tsSessionManager.Text = "toolStrip1";
            // 
            // tsbNewFolder
            // 
            this.tsbNewFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewFolder.Image = global::AppInForm.Properties.Resources.folder_add;
            this.tsbNewFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewFolder.Name = "tsbNewFolder";
            this.tsbNewFolder.Size = new System.Drawing.Size(23, 22);
            this.tsbNewFolder.Text = "New Folder";
            this.tsbNewFolder.Click += new System.EventHandler(this.tsbNewFolder_Click);
            // 
            // tsbNewSSHSession
            // 
            this.tsbNewSSHSession.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNewSSHSession.Image = global::AppInForm.Properties.Resources.application_xp_terminal;
            this.tsbNewSSHSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNewSSHSession.Name = "tsbNewSSHSession";
            this.tsbNewSSHSession.Size = new System.Drawing.Size(23, 22);
            this.tsbNewSSHSession.Text = "New SSH Session";
            this.tsbNewSSHSession.Click += new System.EventHandler(this.tsbNewSSHSession_Click);
            // 
            // SessionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(300, 320);
            this.Controls.Add(this.tsSessionManager);
            this.Controls.Add(this.tvSessions);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SessionsControl";
            this.Padding = new System.Windows.Forms.Padding(0, 18, 0, 0);
            this.tsSessionManager.ResumeLayout(false);
            this.tsSessionManager.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tvSessions;
        private System.Windows.Forms.ToolStrip tsSessionManager;
        private System.Windows.Forms.ToolStripButton tsbNewFolder;
        private System.Windows.Forms.ImageList ilTreeView;
        private System.Windows.Forms.ToolStripButton tsbNewSSHSession;

    }
}
