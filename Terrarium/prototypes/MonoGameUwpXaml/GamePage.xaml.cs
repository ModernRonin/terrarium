using System;
using Windows.UI.Xaml;
using ModernRonin.Terrarium.Logic;
using MonoGame.Framework;

namespace MonoGameUwpXaml
{
    public sealed partial class GamePage
    {
        readonly VisualSimulation mVisualization;
        readonly Simulation mSimulation = new Simulation(SimulationState.Default);
        readonly DispatcherTimer mTimer = new DispatcherTimer();
        public GamePage()
        {
            InitializeComponent();
            var launchArguments = string.Empty;
            mVisualization = XamlGame<VisualSimulation>.Create(launchArguments, Window.Current.CoreWindow, SwapChainPanel);

            mTimer.Interval = TimeSpan.FromMilliseconds(33);
            mTimer.Tick += OnTimer;
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            SetDisplayFromSimulation();
        }
        void OnTimer(object sender, object e) => SetDisplayFromSimulation();
        void SetDisplayFromSimulation()
        {
            mVisualization.SimulationState = mSimulation.CurrentState;
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