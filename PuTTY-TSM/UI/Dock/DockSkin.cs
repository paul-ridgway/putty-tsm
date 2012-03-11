using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing;

namespace AppInForm
{
    class DockSkin : DockPanelSkin
    {

        public DockSkin()
        {
            DockPaneStripSkin.DocumentGradient.ActiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = Color.FromArgb(235, 235, 235);
            DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = Color.FromArgb(200, 200, 200);
        }
    }
}
