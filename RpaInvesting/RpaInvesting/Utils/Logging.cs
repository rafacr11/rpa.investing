using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaInvesting.Utils
{
    public enum TypeLog
    {
        Info,
        Error,
        Warning
    }

    public class Logging
    {
        public string LogPath { get; set; }
        public string FullFilePath { get; set; }

        private Logging()
        {
        }

        public Logging(string logPath, string logFileName = null)
        {
            LogPath = logPath + (!logPath.EndsWith(@"\") ? @"\" : string.Empty);
            FullFilePath = LogPath + (string.IsNullOrEmpty(logFileName) ? "log_" + DateTime.Now.ToString("yyyMMdd_HHmmss") + ".txt" : logFileName);
        }

        public void Log(TypeLog typeLog, String message)
        {
            if (!Directory.Exists(LogPath)) { Directory.CreateDirectory(LogPath); }

            StringBuilder sb = new StringBuilder();
            switch (typeLog)
            {
                case TypeLog.Info:
                    sb.AppendLine(DateTime.Now + " - [ INFO ] - " + message);
                    Console.Write(DateTime.Now + " - [ INFO ] - " + message);
                    break;

                case TypeLog.Error:
                    sb.AppendLine(DateTime.Now + " - [ ERROR ] - " + message);
                    Console.Write(DateTime.Now + " - [ ERROR ] - " + message);
                    break;

                case TypeLog.Warning:
                    sb.AppendLine(DateTime.Now + " - [ WARNING ] - " + message);
                    Console.Write(DateTime.Now + " - [ WARNING ] - " + message);
                    break;

                default:
                    break;
            }

            File.AppendAllText(FullFilePath, sb.ToString());
            Task.Run(() => Console.Out.WriteLineAsync(sb.ToString()));
            sb.Clear();
        }

        public void Info(String message)
        {
            Log(TypeLog.Info, message);
        }

        public void Error(String message)
        {
            Log(TypeLog.Error, message);
        }

        public void Warning(String message)
        {
            Log(TypeLog.Warning, message);
        }
    }
}