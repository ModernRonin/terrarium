using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulationView.Model
{
    public class Simulation
    {
        SimulationState mCurrentState;
        bool mIsRunning;
        bool mIsStopRequested;
        Task mTask;
        public SimulationState CurrentState => mCurrentState;
        public void Tick()
        {
            var next = new SimulationTicker(CurrentState).Tick();
            Interlocked.Exchange(ref mCurrentState, next);
        }
        void Run()
        {
            while (!mIsStopRequested) Tick();
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
    }
}