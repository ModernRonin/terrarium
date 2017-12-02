using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;
using Caliburn.Micro;
using ModernRonin.Terrarium.Logic;
using ModernRonin.Terrarium.Rendering.Windows;

namespace ModernRonin.Terrarium.Client.Windows
{
    public class ApplicationSetup
    {
        readonly ContainerBuilder mBuilder;
        public ApplicationSetup(ContainerBuilder builder) => mBuilder = builder;
        public static Assembly[] Assemblies => GetAssemblies().Distinct().ToArray();
        IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> TypesFromAllAssemblies =>
            mBuilder.RegisterAssemblyTypes(Assemblies);
        static IEnumerable<Assembly> GetAssemblies()
        {
            yield return AssemblyOf<UwpClientModule>();
            yield return AssemblyOf<LogicModule>();
            yield return AssemblyOf<VisualizationModule>();
        }
        public void Configure()
        {
            mBuilder.RegisterType<DebugLogger>().As<ILog>().SingleInstance();

            TypesFromAllAssemblies.AssignableTo<ICommand>().SingleInstance();
            TypesFromAllAssemblies.EndingWith("View").AsSelf().SingleInstance();
            TypesFromAllAssemblies.EndingWith("ViewModel").AsSelf().SingleInstance();

            mBuilder.RegisterAssemblyModules(Assemblies);
        }
        static Assembly AssemblyOf<T>() => typeof(T).GetTypeInfo().Assembly;
    }
}