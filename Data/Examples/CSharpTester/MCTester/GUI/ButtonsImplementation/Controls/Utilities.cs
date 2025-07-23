using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing;

namespace MapCore.Common
{
	/// <summary>
	/// Summary description for Utilities.
	/// </summary>
	public class Utilities
	{
		static public void ShowErrorMessage(string title, string testerMessage, Exception currException)
		{
			//retrieving the Class name
			StackTrace st = new StackTrace();
			System.Reflection.MethodBase currMethod = st.GetFrame(1).GetMethod();
			string m_className = currMethod.ReflectedType.FullName;
			
			//retrieving the Method name
			char[] delimeters = new char[1];
			delimeters[0] = '\n';
			string currentMethodLine = currException.StackTrace.Split(delimeters)[1];
			//MessageBox.Show(currentMethodLine);
			int inIndex = currentMethodLine.IndexOf(" in");
			int atIndex = currentMethodLine.IndexOf("at ");
			string currentMethodName;
			
			if (inIndex != -1)
				currentMethodName = currentMethodLine.Substring(atIndex+3 ,inIndex-(atIndex+3)).Trim();
			else
				currentMethodName = currentMethodLine.Substring(atIndex+3).Trim();
			
			string caption = "MCError - " + title;
			string message = "Class: " + m_className + "\r\n";
			message += "Function: " + currentMethodName + "\r\n";
			message += "Message: " + testerMessage + "\r\n";
			message += "Error: " + currException.Message;

            Logger.WriteMessage("Error: " + currException.Message, Logger._FATAL);
			MessageBox.Show(message,caption);
            
		}

        public static Color ToColor(DNSMcBColor mcColor)
        {
            return Color.FromArgb(mcColor.a, mcColor.r, mcColor.g, mcColor.b);
        }

        public static Color ToColor(DNSMcFColor mcColor)
        {
            return Color.FromArgb((int)mcColor.a, (int)mcColor.r, (int)mcColor.g, (int)mcColor.b);
        }

        

        static public void ShowErrorMessage(string title,Exception currException,string moreErrorMsg = "")
        {
            //retrieving the Class name
            StackTrace st = new StackTrace();
            System.Reflection.MethodBase currMethod = st.GetFrame(1).GetMethod();
            string m_className = currMethod.ReflectedType.FullName;

            //retrieving the Method name
            char[] delimeters = new char[1];
            delimeters[0] = '\n';
            string currentMethodLine = "";
            string[] stringMessages = currException.StackTrace.Split(delimeters);
            if (stringMessages != null)
            {
                if (stringMessages.Length > 1)
                    currentMethodLine = stringMessages[1];
                else
                    currentMethodLine = stringMessages[0];
            }
            
            int inIndex = currentMethodLine.IndexOf(" in");
            int atIndex = currentMethodLine.IndexOf("at ");
            string currentMethodName;

            if (inIndex != -1)
                currentMethodName = currentMethodLine.Substring(atIndex + 3, inIndex - (atIndex + 3)).Trim();
            else
                currentMethodName = currentMethodLine.Substring(atIndex + 3).Trim();

            string caption = "MCError - " + title;
            string message = "Class: " + m_className + "\r\n";
            message += "Function: " + currentMethodName + "\r\n";
            message += "Error: " + currException.Message;
            if (moreErrorMsg != "")
            {
                message += ", " + moreErrorMsg;
            }
            Logger.WriteMessage("Error: " + currException.Message, Logger._FATAL);

            if (currException.InnerException!=null)
            {
                message += "\r\nInner Exception:" + currException.InnerException.Message;
            }
            MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
	}

    public class Logger
    {
        public static int _NOLOG = -1;
        public static int _LOW_DEBUG = 0;
        public static int _DEBUG = 1;
        public static int _WARNING = 2;
        public static int _FATAL = 3;
        public static int _BUG = 4;
        public static int _MAPCORE_API_CALL = 5;
        private static int LOG_LVL;
        private static StreamWriter TextW;
        

        public static void SetSeverity(int Severity)
        {
            
            LOG_LVL = Severity;

            if (Severity > -1)
            {
                Win32.FreeConsole();
                Win32.AllocConsole();
                string FileDate = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "___" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString();

                if (TextW != null)
                {
                    TextW.Close();
                    TextW.Dispose();
                }
                
                TextW = new StreamWriter(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\MCTester" + FileDate + ".Log", true);
                WriteMessage("LOG APPLICATION STARTED!", _BUG);
                WriteMessage("LOG LEVEL = " + Severity.ToString(), _BUG);

            }
            else
            {
                Win32.FreeConsole();
            }
        }

        public static void WriteMessage(string sMSG, int Severity)
        {
            if (Severity <= LOG_LVL && LOG_LVL != -1)
            {
                string output = DateTime.Now.ToString() + " - "+  ShowSeverity(Severity) + ">>" + sMSG;
                Console.WriteLine(output);

                if (TextW != null)
                    ((StreamWriter)TextW).WriteLine(output);
            }

            //System.Diagnostics.Debug.Print(sMSG);
        }

        private static string ShowSeverity(int LVL)
        {
            switch (LVL)
            {
                case 0: Console.ForegroundColor = ConsoleColor.Blue; return "LOW_DEBUG";
                case 1: Console.ForegroundColor = ConsoleColor.DarkGray; return "DEBUG";
                case 2: Console.ForegroundColor = ConsoleColor.Yellow; return "WARNING";
                case 3: Console.ForegroundColor = ConsoleColor.Green; return "FATAL";
                case 4: Console.ForegroundColor = ConsoleColor.Red; return "BUG";
                case 5: Console.ForegroundColor = ConsoleColor.Cyan; return "APICALL";
                default: Console.ForegroundColor = ConsoleColor.White; return "UNKNOWN";
            }
        }
    }


    public class Win32
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern Boolean FreeConsole();

    }
}
