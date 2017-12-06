using System;
using System.Reflection;
using Autofac;
using ModernRonin.Terrarium.Logic.Transformations;
using Module = Autofac.Module;

namespace ModernRonin.Terrarium.Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Simulation>().As<ISimulation>().SingleInstance();
            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo<ISimulationStateTransformerWithDependencies>().As<ISimulationStateTransformerWithDependencies>()
                   .InstancePerDependency();
            builder.Register<Func<ISimulationState>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return () => ctx.Resolve<ISimulation>().CurrentState;
            });
        }
    }
}