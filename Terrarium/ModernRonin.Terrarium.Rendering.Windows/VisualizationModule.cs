using System;
using System.Reflection;
using Autofac;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows.Drawing;
using ModernRonin.Terrarium.Rendering.Windows.Interaction;
using Module = Autofac.Module;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class VisualizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ctx => SetupVisualization(ctx.Resolve<IComponentContext>()));
            builder.RegisterType<Camera>().As<ICamera>().SingleInstance();
            builder.RegisterType<CameraController>().As<ICameraController>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<ARenderer>().AsSelf()
                   .InstancePerDependency();
            builder.RegisterType<Renderer>().AsSelf().InstancePerDependency();
            builder.RegisterType<TextureDirectory>().AsSelf().SingleInstance();
            builder.RegisterType<EntitySpriteFactory>().As<IEntitySpriteFactory>().SingleInstance();
            builder.Register(ctx => GraphicsDeviceProvider(ctx.Resolve<IComponentContext>()));
            builder.Register(ctx => SpriteBatchProvider(ctx.Resolve<IComponentContext>()));
        }
        static Func<GraphicsDevice> GraphicsDeviceProvider(IComponentContext context)
        {
            return () => context.Resolve<Visualization>().GraphicsDevice;
        }
        static Func<SpriteBatch> SpriteBatchProvider(IComponentContext context)
        {
            return () => context.Resolve<Visualization>().Batch;
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