using System;
using Caliburn.Micro;

namespace ModernRonin.Terrarium.Client.Windows
{
    public class NullLogger : ILog
    {
        public void Info(string format, params object[] args) { }
        public void Warn(string format, params object[] args) { }
        public void Error(Exception exception) { }
    }
}