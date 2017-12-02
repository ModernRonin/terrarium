using System;
using Autofac;

namespace ModernRonin.Terrarium.Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Simulation>().As<ISimulation>().SingleInstance();
            builder.Register<Func<ISimulationState>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return () => ctx.Resolve<ISimulation>().CurrentState;
            });
        }
    }
}