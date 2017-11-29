using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ModernRonin.Terrarium.Logic
{
    public class Simulation : ISimulation
    {
        readonly Stopwatch mWatch = new Stopwatch();
        SimulationState mCurrentState;
        bool mIsRunning;
        bool mIsStopRequested;
        Task mTask;
        public Simulation(SimulationState initialState) => mCurrentState = initialState;
        public Simulation() : this(SimulationState.Default) { }
        public SimulationState CurrentState => mCurrentState;
        public int MaximumFramesPerSecond { get; set; } = 30;
        public void Tick()
        {
            mWatch.Restart();
            var next = new SimulationTicker(CurrentState).Tick();
            Interlocked.Exchange(ref mCurrentState, next);
            mWatch.Stop();
            var minimumTimePerFrame = TimeSpan.FromMilliseconds(1000d / MaximumFramesPerSecond);
            var timeLeftToWait = minimumTimePerFrame.Subtract(mWatch.Elapsed);
            if (timeLeftToWait.TotalMilliseconds > 0) Thread.Sleep(timeLeftToWait);
        }
        public void Start()
        {
            if (mIsRunning) throw new InvalidOperationException("Already running");
            mIsStopRequested = false;
            mIsRunning = true;
            mTask = Task.Run(() => Run());
        }
        public async Task Stop()
        {
            if (!mIsRunning) throw new InvalidOperationException("Not running");
            mIsStopRequested = true;
            await mTask;
            mTask = null;
            mIsRunning = false;
        }
        void Run()
        {
            while (!mIsStopRequested) Tick();
        }
    }
}