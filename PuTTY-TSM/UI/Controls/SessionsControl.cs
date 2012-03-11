using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AppInForm.UI.Controls
{
    public partial class SessionsControl : DockContent
    {
        TreeNode rootNode;


        private SessionManager sessionManager;
        public SessionsControl(SessionManager sessionManager)
        {
            InitializeComponent();

            this.sessionManager = sessionManager;

            //Remove visual debugging colours
            BackColor = Color.White;
            tvSessions.BackColor = Color.White;

            TabText = "PuTTY Sessions";
            LoadSessionData();
        }

        private void LoadSessionData()
        {

            //Root node
            rootNode = new TreeNode("Database");
            rootNode.ImageIndex = 0;
            tvSessions.Nodes.Add(rootNode);

            //Add folders to root
            Dictionary<Int32, String> folders = sessionManager.ListFolders();
            foreach (KeyValuePair<Int32, String> folder in folders)
            {
                AddFolder(folder.Key, folder.Value);
            }

            //Finally, expand all
            rootNode.ExpandAll();
        }

        private void AddFolder(Int32 id, String name)
        {
            TreeNode folderNode = new TreeNode(name,1,1);         
            LoadSessions(id, folderNode);
            rootNode.Nodes.Add(folderNode);
        }

        private void LoadSessions(int folderID, TreeNode folderNode)
        {
            sessionManager.ListSessions(folderID);
        }

        private void tsbNewFolder_Click(object sender, EventArgs e)
        {
            String folderName = "Folder " + (new Random()).Next(0, 100).ToString();
            int id = sessionManager.CreateFolder(folderName);
            AddFolder(id, folderName);
        }

        private void tsbNewSSHSession_Click(object sender, EventArgs e)
        {
            String sessionName = "Session " + (new Random()).Next(0, 100).ToString();
            String sessionHost = "rails.pdr.im";
            //TODO: Capture Username
            //TODO: Capture Port
            int id = sessionManager.CreateSSHSession(1, sessionName, sessionHost, "", 22);
        }

    }
}
