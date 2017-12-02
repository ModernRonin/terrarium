using Autofac;
using ModernRonin.Terrarium.Rendering.Windows.Interaction;

namespace ModernRonin.Terrarium.Rendering.Windows
{
    public class VisualizationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Camera>().As<ICamera>().SingleInstance();
            builder.RegisterType<CameraController>().As<ICameraController>().SingleInstance();
        }
    }
}