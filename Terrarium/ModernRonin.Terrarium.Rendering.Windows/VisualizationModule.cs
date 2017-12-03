using System;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Microsoft.Xna.Framework.Graphics;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows.Drawing;
using ModernRonin.Terrarium.Rendering.Windows.Interaction;
using Module = Autofac.Module;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class VisualizationModule : Module
    {
        /* CAVEAT: because of how XamlGame<>.Create() works, there is no way to hook between construction of a MonoGame Game and it running.
         * so in order to make all the parts of this system IOC constructable, we gotta work around Visualization/Game not being IOC constructable.
         * As it's not constructed by the container, the container wouldn't know to resolve it, either, when it needs to access properties
         * like GraphicsDevice or (Sprite)Batch. Thus we save the instance in SetupVisualization/OnLoading.
         * BUT BEWARE: this means we cannot have multiple instances of Visualization/Game running!
         * 
        */
        Visualization mVisualization;
        static ResolvedParameter CreateParameter<T>(Func<T> getter)
        {
            return new ResolvedParameter((paramInfo, _) => paramInfo.ParameterType == typeof(T), (_, __) => getter());
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<KeyboardDelta>().AsSelf().SingleInstance();
            builder.RegisterType<MouseDelta>().AsSelf().SingleInstance();
            builder.RegisterBuildCallback(SetupVisualization);
            builder.RegisterType<Camera>().As<ICamera>().SingleInstance();
            builder.RegisterType<CameraController>().As<ICameraController>().SingleInstance();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<ARenderer>().AsSelf()
                   .InstancePerDependency().WithParameters(new Parameter[]
                   {
                       CreateParameter(() => mVisualization.GraphicsDevice),
                       CreateParameter(() => mVisualization.Batch)
                   });

            builder.RegisterType<Renderer>().AsSelf().InstancePerDependency();
            builder.RegisterType<TextureDirectory>().AsSelf().SingleInstance();
            builder.RegisterType<EntitySpriteFactory>().As<IEntitySpriteFactory>().SingleInstance();
            builder.Register<Func<GraphicsDevice>>(_ => () => mVisualization.GraphicsDevice);
        }
        void SetupVisualization(IComponentContext context)
        {

            Visualization.OnLoading = instance =>
            {
                context.Resolve<TextureDirectory>().Load(instance.Content);
                var camera = context.Resolve<ICamera>();
                instance.Window.ClientSizeChanged += (_, __) =>
                {
                    camera.ViewportWidth = instance.GraphicsDevice.Viewport.Width;
                    camera.ViewportHeight = instance.GraphicsDevice.Viewport.Height;
                };
                mVisualization = instance;
            };
            Visualization.OnUpdating = instance =>
            {
                context.Resolve<ICameraController>().Update();
                instance.TransformationMatrix = context.Resolve<ICamera>().TransformationMatrix;
            };
            Visualization.OnRendering = instance =>
            {
                var cc = context.Resolve<IComponentContext>();
                var simulationSnapshotter = cc.Resolve<Func<ISimulationState>>();
                var renderer = context.Resolve<Renderer>();
                renderer.Render(simulationSnapshotter());
            };
        }
    }
}