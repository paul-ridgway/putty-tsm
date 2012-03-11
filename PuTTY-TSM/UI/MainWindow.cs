using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using AppInForm.UI.Controls;

namespace AppInForm
{
    public partial class MainWindow : Form
    {
        private DockPanel dockPanel = new DockPanel();
        private List<PuttyControl> puttyControls = new List<PuttyControl>();

        public MainWindow()
        {
            InitializeComponent();

            dockPanel.DocumentStyle = DocumentStyle.SystemMdi;
            dockPanel.ActiveAutoHideContent = null;
            dockPanel.DefaultFloatWindowSize = new System.Drawing.Size(800, 600);
            dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            dockPanel.DockBackColor = System.Drawing.SystemColors.Control;
            dockPanel.DockBottomPortion = 200;
            dockPanel.DockRightPortion = 200;
            dockPanel.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            dockPanel.Location = new System.Drawing.Point(0, 0);
            dockPanel.Name = "dockPanel1";
            dockPanel.Skin = new DockSkin();


            dockPanel.ActiveDocumentChanged += dockPanel1_ActiveDocumentChanged;

            Controls.Add(dockPanel);
            dockPanel.BringToFront();

            //TODO: Move to a Control Factory?   
            SessionsControl sessionControl = new SessionsControl(Program.SessionManager );
            sessionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            sessionControl.DockAreas = DockAreas.DockTop | DockAreas.DockRight | DockAreas.DockLeft | DockAreas.DockBottom;
            sessionControl.Show(dockPanel, DockState.DockRight);


        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (dockPanel.ActiveDocument is PuttyControl)
            {
                PuttyControl p = (PuttyControl)dockPanel.ActiveDocument;
                p.SetFocusToChildApplication();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //TODO: Move to a Control Factory?   
            {
                PuttyControl puttyControl = new PuttyControl();
                puttyControl.Dock = System.Windows.Forms.DockStyle.Fill;
                puttyControl.DockAreas = DockAreas.Document;
                puttyControl.Show(dockPanel, DockState.Document);
                puttyControls.Add(puttyControl);
            }

        }

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Resize End Main Window");
            foreach (PuttyControl puttyControl in puttyControls)
            {
                //TODO: Remove?
                //puttyControl.ResizePutty();
            }
        }

    }
}
