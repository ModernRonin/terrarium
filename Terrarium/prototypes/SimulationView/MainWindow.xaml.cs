using System;
using System.Windows;
using System.Windows.Threading;
using SimulationView.Model;

namespace SimulationView
{
    public partial class MainWindow
    {
        readonly Simulation mSimulation = new Simulation(SimulationState.Default);
        readonly DispatcherTimer mTimer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            mTimer.Interval = TimeSpan.FromMilliseconds(33);
            mTimer.Tick += OnTimer;
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            SetDisplayFromSimulation();
        }
        void OnTimer(object sender, EventArgs e) => SetDisplayFromSimulation();
        void SetDisplayFromSimulation()
        {
            Display.SimulationState = mSimulation.CurrentState;
        }
        void OnStart(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            mSimulation.Start();
            mTimer.Start();
            StopButton.IsEnabled = true;
        }
        async void OnStop(object sender, RoutedEventArgs e)
        {
            StopButton.IsEnabled = false;
            await mSimulation.Stop();
            mTimer.Stop();
            StartButton.IsEnabled = true;
        }
    }
}