using System;
using System.Diagnostics;

namespace SimulationView
{
    public class TracingWatch : IDisposable
    {
        readonly Stopwatch mWatch = new Stopwatch();
        public TracingWatch()
        {
            mWatch.Start();
        }
        public void Dispose()
        {
            mWatch.Stop();
            Trace.WriteLine($"Render took {mWatch.ElapsedMilliseconds}ms");
        }
    }
}