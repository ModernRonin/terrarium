using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public sealed partial class SimulationView
    {
        public static readonly DependencyProperty SwapChainPanelConsumerProperty =
            DependencyProperty.Register(nameof(SwapChainPanelConsumer),
                typeof(Action<SwapChainPanel>),
                typeof(SimulationView),
                new PropertyMetadata((Action<SwapChainPanel>) (_ => { })));
        public SimulationView()
        {
            InitializeComponent();
        }
        public Action<SwapChainPanel> SwapChainPanelConsumer
        {
            get => (Action<SwapChainPanel>) GetValue(SwapChainPanelConsumerProperty);
            set
            {
                SetValue(SwapChainPanelConsumerProperty, value);
                value?.Invoke(SwapChainPanel);
            }
        }
    }
}