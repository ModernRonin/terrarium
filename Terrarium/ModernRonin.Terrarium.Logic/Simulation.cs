using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModernRonin.Terrarium.Logic
{
    public class Simulation : ISimulation
    {
        readonly Stopwatch mWatch = new Stopwatch();
        readonly IEnumerable<ISimulationStateTransformer> mTransformers;
        ISimulationState mCurrentState;
        bool mIsStopRequested;
        Task mTask;
        public Simulation(ISimulationState initialState, IEnumerable<ISimulationStateTransformer> transformers)
        {
            mCurrentState = initialState;
            mTransformers = transformers;
        }
        // ReSharper disable once UnusedMember.Global - used by IOC
        public Simulation(IEnumerable<ISimulationStateTransformer> transformers) : this(Defaults.SimulationState, transformers) { }
        public ISimulationState CurrentState => mCurrentState;
        public int MaximumFramesPerSecond { get; set; } = 30;
        public bool IsRunning { get; set; }
        public void Tick()
        {
            mWatch.Restart();
            var next= mTransformers.OrderBy(t => t.Priority).Aggregate(mCurrentState, (s, t) => t.Transform(s));
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