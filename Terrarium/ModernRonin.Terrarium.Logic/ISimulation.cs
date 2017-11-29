using System.Threading.Tasks;

namespace ModernRonin.Terrarium.Logic
{
    public interface ISimulation
    {
        ISimulationState CurrentState { get; }
        int MaximumFramesPerSecond { get; set; }
        bool IsRunning { get; }
        void Tick();
        void Start();
        Task Stop();
    }
}