using System;
using Autofac;
using ModernRonin.Terrarium.Logic.Config;
using ModernRonin.Terrarium.Logic.Transformations;
using ModernRonin.Terrarium.Logic.Transformations.Execution;
using ModernRonin.Terrarium.Logic.Transformations.Framework;

namespace ModernRonin.Terrarium.Logic
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Simulation>().As<ISimulation>().SingleInstance();
            builder.RegisterType<SimulationStateTransformer>().As<ISimulationStateTransformer>().SingleInstance();
            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo<ISimulationStateTransformerWithDependencies>()
                   .As<ISimulationStateTransformerWithDependencies>().InstancePerDependency();
            builder.RegisterAssemblyTypes(ThisAssembly).AssignableTo<IInstructionExecutor>().As<IInstructionExecutor>()
                   .InstancePerDependency();
            builder.Register<Func<ISimulationState>>(c =>
            {
                var ctx = c.Resolve<IComponentContext>();
                return () => ctx.Resolve<ISimulation>().CurrentState;
            });
            builder.RegisterType<Configuration>().AsImplementedInterfaces().SingleInstance();
        }
    }
}