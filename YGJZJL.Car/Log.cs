using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using INI;

namespace YGJZJL.Car
{
    enum LogStatus
    {
        Info = 0,Debug = 1,Exception = 2,Error = 3
    }

    class Log
    {
        private static int _logLevel = 0;

        static Log()
        {
            string Current = System.IO.Directory.GetCurrentDirectory();//获取当前根目录
            Ini ini = new Ini(Current + "/mcms.ini");
            int.TryParse(ini.ReadValue("Log", "level"), out _logLevel);
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="error"></param>
        public static void Error(string error)
        {
            if ((int)LogStatus.Error >= _logLevel)
                WriteLog(LogStatus.Error, error);
        }

        public static void Info(string info)
        {
            if ((int)LogStatus.Info >= _logLevel)
                WriteLog(LogStatus.Info, info);
        }

        public static void Debug(string debug)
        {
            if ((int)LogStatus.Debug >= _logLevel)
                WriteLog(LogStatus.Debug, debug);
        }

        private static void WriteLog(LogStatus logStatus, string message)
        {
            string m_szRunPath = System.Environment.CurrentDirectory.ToString();
            //string m_szRunPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (!(System.IO.Directory.Exists(m_szRunPath + "\\log")))
            {
                System.IO.Directory.CreateDirectory(m_szRunPath + "\\log");
            }

            string strDate = System.DateTime.Now.ToString("yyyyMMdd");

            System.IO.TextWriter tw = new System.IO.StreamWriter(m_szRunPath + "\\log\\Mcms_" + strDate + ".log", true);

            tw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + (((int)logStatus == 0 ? "info" : (int)logStatus == 1 ? "debug" : (int)logStatus == 2 ? "exception" : (int)logStatus == 3 ? "error" : "") + ": " + message));
            //tw.WriteLine(((int)logStatus == 0 ? "info" : (int)logStatus == 1 ? "debug" : (int)logStatus == 2 ? "exception" : (int)logStatus == 3 ? "error" : "")  + ": " + message);
            //tw.WriteLine("\r\n");
            tw.Close();
        }
    }
}
