using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace AppInForm
{
    public delegate void PuttyClosedCallback(bool error);

    public partial class PuttyPanel : Panel
    {

        #region "DLL Calls"
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndParent);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        [DllImport("user32")]
        static extern bool AnimateWindow(IntPtr hwnd, int time, AnimateWindowFlags flags);
        #endregion

        #region "Constants etc"
        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WM_CLOSE = 0x10;
        private const int WS_CHILD = 0x40000000;
        private const int WS_BORDER = 0x00800000;
        public const uint WS_THICKFRAME = 0x00040000;
        const int FILE_NOT_FOUND = 2;
        const int ACCESS_DENIED = 5;

        public enum AnimateWindowFlags
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

        private enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized
            /// or maximized, the system restores it to its original size and
            /// position. An application should specify this flag when displaying
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position.
            /// This value is similar to "ShowNormal", except the window is not
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is
            /// minimized or maximized, the system restores it to its original size
            /// and position. An application should specify this flag when restoring
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread
            /// that owns the window is hung. This flag should only be used when
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }
        #endregion

        #region "Instance Variables"
        private Process m_Process = null;
        private IntPtr m_AppWin;
        //internal PuttyClosedCallback m_CloseCallback;
        #endregion

        public PuttyPanel()
        {
        }

        public void CreateApplication()
        {
            try
            {
                m_Process = new Process();
                m_Process.EnableRaisingEvents = true;
                m_Process.StartInfo.FileName = "putty.exe";
                m_Process.StartInfo.Arguments = "root@php.pdr.im -pw 3th3rn3t";

                m_Process.Exited += delegate(object sender, EventArgs ev)
                {
                    //m_CloseCallback(true);
                };

                m_Process.Start();

                // Wait for application to start and become idle
                m_Process.WaitForInputIdle();
                m_AppWin = m_Process.MainWindowHandle;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(this, ex.Message, "Invalid Operation Error");
                throw;
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode == ACCESS_DENIED)
                {
                    throw;
                }
                else if (ex.NativeErrorCode == FILE_NOT_FOUND)
                {
                    throw;
                }
            }

            SetParent(m_AppWin, this.Handle);

            ShowWindow(m_AppWin, WindowShowStyle.Maximize);

            long lStyle = GetWindowLong(m_AppWin, GWL_STYLE);
            lStyle &= ~(WS_BORDER | WS_THICKFRAME);
            SetWindowLong(m_AppWin, GWL_STYLE, (int)lStyle);

            MoveWindow(m_AppWin, 0, 0, this.Width, this.Height, true);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnHandleDestroyed");
            if (m_AppWin != IntPtr.Zero)
            {
                PostMessage(m_AppWin, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

                System.Threading.Thread.Sleep(1000);

                m_AppWin = IntPtr.Zero;
            }

            base.OnHandleDestroyed(e);
        }

        protected override void OnResize(EventArgs e)
        {
            ResizePutty();
            base.OnResize(e);
        }

        internal void ResizePutty()
        {
            if (this.m_AppWin != IntPtr.Zero)
            {
                System.Diagnostics.Debug.WriteLine("Resize PP + PTR");
                MoveWindow(m_AppWin, -3, 16, this.Width + 5, this.Height - 12, true);
            }

        }


        protected override void OnSizeChanged(EventArgs e)
        {
//            System.Diagnostics.Debug.WriteLine("OnSizeChanged");
            this.Invalidate();
            base.OnSizeChanged(e);
        }

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public bool RefocusPuTTY()
        {
            System.Diagnostics.Debug.WriteLine("ReFocusPuTTY");

            return (this.m_AppWin != null
                && GetForegroundWindow() != this.m_AppWin
                && !SetForegroundWindow(this.m_AppWin));
        }

    }
}
