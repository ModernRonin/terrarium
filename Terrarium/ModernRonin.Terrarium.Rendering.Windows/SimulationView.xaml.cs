using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ModernRonin.Terrarium.Logic;
using MonoGame.Framework;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public sealed partial class SimulationView : UserControl, IDisposable
    {
        public static readonly DependencyProperty SimulationStateSourceProperty =
            DependencyProperty.Register(nameof(SimulationStateSource),
                typeof(Func<ISimulationState>),
                typeof(SimulationView),
                new PropertyMetadata((Func<ISimulationState>) (() => new NullSimulationState())));
        readonly VisualSimulation mVisualization;
        public SimulationView()
        {
            InitializeComponent();
            mVisualization = XamlGame<VisualSimulation>.Create(string.Empty, Window.Current.CoreWindow, SwapChainPanel);
            mVisualization.OnUpdate = () => SimulationStateSource();
        }
        public Func<ISimulationState> SimulationStateSource
        {
            get => (Func<ISimulationState>) GetValue(SimulationStateSourceProperty);
            set => SetValue(SimulationStateSourceProperty, value);
        }
        public void Dispose()
        {
            mVisualization.Dispose();
        }
    }
}