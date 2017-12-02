using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Autofac;
using ModernRonin.Terrarium.Rendering.Windows;
using MonoGame.Framework;

namespace ModernRonin.Terrarium.Client.Windows
{
    public class UwpClientModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => SetupVisualization()).SingleInstance();
        }
        Action<SwapChainPanel> SetupVisualization()
        {
            return panel => XamlGame<Visualization>.Create(string.Empty, Window.Current.CoreWindow, panel);
        }
    }
}