using System;
using Autofac;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows.Drawing;
using ModernRonin.Terrarium.Rendering.Windows.Interaction;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class VisualizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => SetupVisualization(ctx.Resolve<IComponentContext>()));
            builder.RegisterType<Camera>().As<ICamera>().SingleInstance();
            builder.RegisterType<CameraController>().As<ICameraController>().SingleInstance();
        }
        static Action<Visualization> SetupVisualization(IComponentContext context)
        {
            return vis =>
            {
                var textureDirectory = context.Resolve<TextureDirectory>();
                var camera = context.Resolve<ICamera>();
                var renderer = context.Resolve<Renderer>();
                var simulationSnapshotter = context.Resolve<Func<ISimulationState>>();
                vis.OnLoading = textureDirectory.Load;
                vis.Window.ClientSizeChanged += (_, __) =>
                {
                    camera.ViewportWidth = vis.GraphicsDevice.Viewport.Width;
                    camera.ViewportHeight = vis.GraphicsDevice.Viewport.Height;
                };
                vis.OnUpdating = context.Resolve<ICameraController>().Update;
                vis.OnSettingTranslationMatrix = () => camera.TranslationMatrix;
                vis.OnRendering = () => { renderer.Render(simulationSnapshotter()); };
            };
        }
    }
}