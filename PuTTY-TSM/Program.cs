using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace AppInForm
{
    static class Program
    {
        private static SessionManager sessionManager;
        public static SessionManager SessionManager { get { return sessionManager; } }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Set up global constants
            sessionManager = new SessionManager(Directory.GetCurrentDirectory() + "\\sessions.s3db");
            sessionManager.Open();

            //Run main application
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
            
            //Tear down and tidy up
            sessionManager.Close();
            sessionManager = null;
        }
    }
}
