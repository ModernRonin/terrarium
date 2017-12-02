using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Autofac;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows;
using MonoGame.Framework;

namespace ModernRonin.Terrarium.Client.Windows
{
    public class UwpClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => SetupVisualization(ctx.Resolve<IComponentContext>())).SingleInstance();
        }
        Action<SwapChainPanel> SetupVisualization(IComponentContext context)
        {
            var simulation = context.Resolve<ISimulation>();
            var visualizationFinisher = context.Resolve<Action<Visualization>>();
            return panel =>
            {
                var visualization = XamlGame<Visualization>.Create(string.Empty, Window.Current.CoreWindow, panel);
                visualization.OnUpdate = () => simulation.CurrentState;
                visualizationFinisher(visualization);
            };
        }
    }
}