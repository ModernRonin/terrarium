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
            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo<ISimulationStateTransformer>().As<ISimulationStateTransformer>()
                   .InstancePerDependency();
            builder.Register<Func<ISimulationState>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return () => ctx.Resolve<ISimulation>().CurrentState;
            });
        }
    }
}