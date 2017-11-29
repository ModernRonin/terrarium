using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ModernRonin.Terrarium.Logic
{
    public class Simulation : ISimulation
    {
        readonly Stopwatch mWatch = new Stopwatch();
        ISimulationState mCurrentState;
        bool mIsStopRequested;
        Task mTask;
        public Simulation(SimulationState initialState) => mCurrentState = initialState;
        // ReSharper disable once UnusedMember.Global - used by IOC
        public Simulation() : this(SimulationState.Default) { }
        public ISimulationState CurrentState => mCurrentState;
        public int MaximumFramesPerSecond { get; set; } = 30;
        public bool IsRunning { get; set; }
        public void Tick()
        {
            mWatch.Restart();
            var next = new SimulationTicker(CurrentState).Tick();
            Interlocked.Exchange(ref mCurrentState, next);
            mCurrentState = next;
            mWatch.Stop();
            var minimumTimePerFrame = TimeSpan.FromMilliseconds(1000d / MaximumFramesPerSecond);
            var timeLeftToWait = minimumTimePerFrame.Subtract(mWatch.Elapsed);
            if (timeLeftToWait.TotalMilliseconds > 0) Thread.Sleep(timeLeftToWait);
        }
        public void Start()
        {
            if (IsRunning) throw new InvalidOperationException("Already running");
            mIsStopRequested = false;
            IsRunning = true;
            mTask = Task.Run(() => Run());
        }
        public async Task Stop()
        {
            if (!IsRunning) throw new InvalidOperationException("Not running");
            mIsStopRequested = true;
            await mTask;
            mTask = null;
            IsRunning = false;
        }
        void Run()
        {
            while (!mIsStopRequested) Tick();
        }
    }
}