using Windows.UI.Xaml;
using ModernRonin.Terrarium.Logic;

namespace MonoGameUwpXaml
{
    public sealed partial class GamePage
    {
        readonly Simulation mSimulation = new Simulation(SimulationState.Default);
        public GamePage()
        {
            InitializeComponent();
            SimulationView.SimulationStateSource = () => mSimulation.CurrentState;

            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
        }
        void OnStart(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            mSimulation.Start();
            StopButton.IsEnabled = true;
        }
        async void OnStop(object sender, RoutedEventArgs e)
        {
            StopButton.IsEnabled = false;
            await mSimulation.Stop();
            StartButton.IsEnabled = true;
        }
    }
}