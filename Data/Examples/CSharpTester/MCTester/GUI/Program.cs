using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MCTester;

namespace MCTester
{
    static class Program
    {
        public static MainForm AppMainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length > 0)
                AppMainForm = new MainForm(args);
            else
                AppMainForm = new MainForm();

            Application.Run(AppMainForm);
            
            MCTester.Managers.MapWorld.MCTMapFormManager.DisposeAllWrapperObjects();  
        }
    }
}