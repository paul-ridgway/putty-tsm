using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace AppInForm
{


    class PuttyControl : DockContent
    {
        PuttyPanel puttyPanel;

        public PuttyControl()
        {
            CreatePanel();
        }


        private void CreatePanel()
        {

            BackColor = Color.Black;
            Padding = new System.Windows.Forms.Padding(3,3,0,0);
            puttyPanel = new PuttyPanel();
            SuspendLayout();
            puttyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            puttyPanel.Location = new System.Drawing.Point(0, 0);
            puttyPanel.Name = "puttyPanel";
            puttyPanel.TabIndex = 0;
            //this.puttyPanel.m_CloseCallback = this.m_ApplicationExit;
            Controls.Add(this.puttyPanel);
            puttyPanel.CreateApplication();
            ResumeLayout();

            TabText = "PuTTY";
        }


        protected override void OnDockStateChanged(EventArgs e)
        {
            puttyPanel.Show();
            base.OnDockStateChanged(e);
        }

        protected override void OnResize(EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("Resize");
            base.OnResize(e);
        }

        protected override void OnResizeEnd(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("ResizeEND");
            base.OnResize(e);
        }

        protected override void OnClick(EventArgs e) 
        {
        }

        internal void SetFocusToChildApplication()
        {
            this.puttyPanel.RefocusPuTTY();
        }

        internal void ResizePutty()
        {
            this.puttyPanel.ResizePutty();
        }
    }

    
}
