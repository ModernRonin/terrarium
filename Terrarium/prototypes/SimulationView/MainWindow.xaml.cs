using System.Windows;
using SimulationView.Model;

namespace SimulationView
{
    public partial class MainWindow
    {
        readonly Simulation mSimulation = new Simulation(SimulationState.Default);
        public MainWindow()
        {
            InitializeComponent();
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