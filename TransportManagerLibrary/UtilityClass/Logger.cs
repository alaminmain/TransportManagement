using System;
using System.Diagnostics;
using System.IO;
using System.Web;

namespace TransportManagerLibrary.UtilityClass
{
    public class Logger
    {
        private static readonly object lockObject = new object();
        private static FileStream fileStream = (FileStream)null;
        private static StreamWriter streamWriter = (StreamWriter)null;
        private static string filePath;
        private static string fileName;
        private static string fileExtension;
        private static string fileDate;
        private static TransportManagerLibrary.UtilityClass.LogLevel logLevel;
        private static Logger instance;

        static Logger()
        {
        }

        private Logger(string logPath, uint logLevel)
        {
            Logger.filePath = Path.GetDirectoryName(logPath);
            if (!Directory.Exists(Logger.filePath))
                Directory.CreateDirectory(Logger.filePath);

            Logger.fileName = Path.GetFileNameWithoutExtension(logPath);
            Logger.fileExtension = Path.GetExtension(logPath);
            DateTime now = DateTime.Now;
            int num = now.Month;
            string str1 = num.ToString().PadLeft(2, '0');
            num = now.Day;
            string str2 = num.ToString().PadLeft(2, '0');
            Logger.fileDate = str1 + str2;
            if (!Enum.IsDefined(typeof(TransportManagerLibrary.UtilityClass.LogLevel), (object)logLevel))
                logLevel = 5U;
            Logger.logLevel = (TransportManagerLibrary.UtilityClass.LogLevel)logLevel;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="logLevel"></param>
        /// 
        public static void CreateInstance(string logPath, uint logLevel)
        {
            if (Logger.instance != null)
                return;
            Logger.instance = new Logger(logPath, logLevel);
        }

        public static void LogError(string message, params object[] param)
        {
            Logger.Log(TransportManagerLibrary.UtilityClass.LogLevel.Error, message, param);
        }

        public static void LogDebug(string message, params object[] param)
        {
            Logger.Log(TransportManagerLibrary.UtilityClass.LogLevel.Debug, message, param);
        }

        public static void LogInfo(string message, params object[] param)
        {
            Logger.Log(TransportManagerLibrary.UtilityClass.LogLevel.Info, message, param);
        }

        public static void LogVerbose(string message, params object[] param)
        {
            Logger.Log(TransportManagerLibrary.UtilityClass.LogLevel.Verbose, message, param);
        }


        public static void LogWarning(string message, params object[] param)
        {
            Logger.Log(TransportManagerLibrary.UtilityClass.LogLevel.Warning, message, param);
        }

        /// <summary>
        /// This method is used for writing log.
        /// </summary>
        /// <param name="logLevel"> </param>
        /// <param name="message"></param>
        /// <param name="param"></param>
        private static void Log(TransportManagerLibrary.UtilityClass.LogLevel logLevel, string message, params object[] param)
        {
            if (Logger.instance == null)
                return;
            if (logLevel <= Logger.logLevel && Logger.logLevel != TransportManagerLibrary.UtilityClass.LogLevel.Off && logLevel != TransportManagerLibrary.UtilityClass.LogLevel.Off)
            {
                try
                {
                    lock (Logger.lockObject)
                    {
                        if (Logger.IsDateChangeRequired())
                        {
                            if (Logger.streamWriter != null)
                            {
                                Logger.streamWriter.Close();
                                Logger.streamWriter = (StreamWriter)null;
                            }
                            if (Logger.fileStream != null)
                            {
                                Logger.fileStream.Close();
                                Logger.fileStream = (FileStream)null;
                            }
                        }
                        if (Logger.fileStream == null)
                        {
                            Logger.fileStream = new FileStream(Path.Combine(Logger.filePath, Logger.fileName + Logger.fileDate + Logger.fileExtension), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                            Logger.streamWriter = new StreamWriter((Stream)Logger.fileStream);
                            Logger.streamWriter.AutoFlush = true;
                        }
                        StackTrace local_0 = new StackTrace(false);
                        string local_1 = "";
                        if (local_0.FrameCount > 2)
                        {
                            StackFrame local_2 = local_0.GetFrame(2);
                            local_1 = local_2.GetMethod().DeclaringType.Name + "." + local_2.GetMethod().Name;
                        }
                        string local_1_1 = local_1.PadRight(50, ' ');
                        if (local_1_1.Length > 50)
                            local_1_1 = local_1_1.Remove(50);
                        string local_3 = string.Empty;
                        if (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["SessionUserName"] != null)
                            local_3 = HttpContext.Current.Session["SessionUserName"].ToString();
                        string local_3_1 = string.IsNullOrEmpty(local_3) ? new string(' ', 25) : local_3.PadRight(25, ' ');
                        Logger.streamWriter.WriteLine("{0}|{1}|{2}|{3}|{4}", (object)DateTime.Now.ToString("yy/MM/dd HH:mm:ss.fff"), (object)((object)logLevel).ToString().PadRight(8, ' '), (object)local_3_1, (object)local_1_1, (object)string.Format(message, param));
                        ((TextWriter)Logger.streamWriter).Flush();
                    }
                }
                catch
                {
                }
            }
        }

        private static bool IsDateChangeRequired()
        {
            DateTime now = DateTime.Now;
            int num = now.Month;
            string str1 = num.ToString().PadLeft(2, '0');
            num = now.Day;
            string str2 = num.ToString().PadLeft(2, '0');
            string str3 = str1 + str2;
            if (str3.Equals(Logger.fileDate))
                return false;
            Logger.fileDate = str3;
            return true;
        }
    }
}
