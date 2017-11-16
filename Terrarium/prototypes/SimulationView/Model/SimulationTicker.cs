namespace SimulationView.Model
{
    public class SimulationTicker
    {
        readonly SimulationState mCurrent;
        readonly SimulationState mNext = new SimulationState();
        public SimulationTicker(SimulationState current) => mCurrent = current;
        public SimulationState Tick() => mNext;
    }
}