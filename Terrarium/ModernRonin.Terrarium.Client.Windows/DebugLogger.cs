using System;
using System.Diagnostics;
using Caliburn.Micro;

namespace ModernRonin.Terrarium.Client.Windows
{
    public class DebugLogger : ILog
    {
        public void Error(Exception exception)
        {
            Debug.WriteLine(CreateLogMessage(exception.ToString()), "ERROR");
        }
        public void Info(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "INFO");
        }
        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine(CreateLogMessage(format, args), "WARN");
        }
        static string CreateLogMessage(string format, params object[] args) =>
            $"[{DateTime.Now:o}] {string.Format(format, args)}";
    }
}