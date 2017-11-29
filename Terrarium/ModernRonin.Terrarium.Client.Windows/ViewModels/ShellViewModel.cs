using System;
using Windows.UI.Xaml;
using Caliburn.Micro;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Client.Windows.ViewModels
{
    public class ShellViewModel : Screen
    {
        readonly ISimulation mSimulation;
        public ShellViewModel(ISimulation simulation) => mSimulation = simulation;
        public Func<ISimulationState> UpdateSimulationState => () => mSimulation.CurrentState;
        public void ExitApplication()
        {
            Application.Current.Exit();
        }
    }
}