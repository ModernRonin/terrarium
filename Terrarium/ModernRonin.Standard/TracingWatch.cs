using System;
using System.Diagnostics;

namespace ModernRonin.Standard
{
    public class TracingWatch : IDisposable
    {
        readonly string mActivity;
        readonly Stopwatch mWatch = new Stopwatch();
        public TracingWatch(string activity)
        {
            mActivity = activity;
            mWatch.Start();
        }
        public void Dispose()
        {
            mWatch.Stop();
            Trace.WriteLine($"{mActivity} took {mWatch.ElapsedMilliseconds}ms");
        }
    }
}