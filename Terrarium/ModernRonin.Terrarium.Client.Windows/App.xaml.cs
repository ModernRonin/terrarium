using System;
using System.Collections.Generic;
using System.Reflection;
using Windows.ApplicationModel.Activation;
using Windows.Foundation.Metadata;
using Windows.System.Profile;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Autofac;
using Caliburn.Micro;
using ModernRonin.Terrarium.Client.Windows.Views;

namespace ModernRonin.Terrarium.Client.Windows
{
    sealed partial class App
    {
        readonly ContainerBuilder mContainerBuilder = new ContainerBuilder();
        IContainer mContainer;
        FrameAdapter mRootFrame;
        public App()
        {
            SetPointerMode();
        }
        void ConfigureCaliburnMicro(IEnumerable<Assembly> assemblies)
        {
            LogManager.GetLog = _ => CreateCaliburnLogger();
            var cfg = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "ModernRonin.Terrarium.Client.Windows.ViewModels",
                DefaultSubNamespaceForViews = "ModernRonin.Terrarium.Client.Windows.Views",
                IncludeViewSuffixInViewModelNames = false
            };
            ViewLocator.ConfigureTypeMappings(cfg);
            ViewModelLocator.ConfigureTypeMappings(cfg);
            AssemblySource.Instance.Clear();
            AssemblySource.Instance.AddRange(assemblies);

            mContainerBuilder.Register(x => mRootFrame).As<INavigationService>().SingleInstance();
        }
        ILog CreateCaliburnLogger() => new NullLogger();
        protected override void Configure()
        {
            var applicationSetup = new ApplicationSetup(mContainerBuilder);
            applicationSetup.Configure();
            ConfigureCaliburnMicro(applicationSetup.Assemblies);

            mContainerBuilder.Register(x => mRootFrame).As<INavigationService>().SingleInstance();
            mContainer = mContainerBuilder.Build();
        }
        protected override void PrepareViewFirst(Frame rootFrame)
        {
            mRootFrame = new FrameAdapter(rootFrame);
        }
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView(typeof(ShellView));
        }
        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrEmpty(key)) { if (mContainer.TryResolve(service, out instance)) return instance; }
            else { if (mContainer.TryResolveNamed(key, service, out instance)) return instance; }

            throw new Exception($"Could not locate any instances of service {service.Name}.");
        }
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var result = mContainer.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
            return result;
        }
        protected override void BuildUp(object instance)
        {
            mContainer.InjectProperties(instance);
        }
        void SetPointerMode()
        {
            const string propertyName = "Windows.UI.Xaml.Application";
            const string propertyValue = "RequiresPointerMode";
            const string deviceFamilyXBox = "Windows.Xbox";
            if (!ApiInformation.IsPropertyPresent(propertyName, propertyValue)) return;
            var isXBox = AnalyticsInfo.VersionInfo.DeviceFamily == deviceFamilyXBox;
            if (isXBox) Current.RequiresPointerMode = ApplicationRequiresPointerMode.WhenRequested;
        }
    }
}