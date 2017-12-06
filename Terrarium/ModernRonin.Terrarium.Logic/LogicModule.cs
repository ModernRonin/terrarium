using System;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace ModernRonin.Terrarium.Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Simulation>().As<ISimulation>().SingleInstance();
            builder.RegisterType<SimulationTicker>().As<ISimulationTicker>().InstancePerDependency();
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AssignableTo<ISimulationStateTransformer>()
                   .InstancePerDependency();
            builder.Register<Func<ISimulationState>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return () => ctx.Resolve<ISimulation>().CurrentState;
            });
        }
    }
}