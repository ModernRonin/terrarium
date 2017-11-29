using System.Threading.Tasks;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulation
    {
        SimulationState CurrentState { get; }
        int MaximumFramesPerSecond { get; set; }
        void Tick();
        void Start();
        Task Stop();
    }
}