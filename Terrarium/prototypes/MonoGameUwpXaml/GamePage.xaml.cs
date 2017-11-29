using System;
using Windows.UI.Xaml;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows;
using MonoGame.Framework;

namespace MonoGameUwpXaml
{
    public sealed partial class GamePage
    {
        readonly VisualSimulation mVisualization;
        readonly Simulation mSimulation = new Simulation(SimulationState.Default);
        public GamePage()
        {
            InitializeComponent();
            var launchArguments = string.Empty;
            mVisualization = XamlGame<VisualSimulation>.Create(launchArguments, Window.Current.CoreWindow, SwapChainPanel);
            mVisualization.OnUpdate = () => mSimulation.CurrentState;

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