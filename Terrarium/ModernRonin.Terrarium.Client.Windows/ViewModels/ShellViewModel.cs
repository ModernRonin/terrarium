using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Caliburn.Micro;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Client.Windows.ViewModels
{
    public class ShellViewModel : Screen
    {
        readonly ISimulation mSimulation;
        string mToggleRunText= "Start";
        public ShellViewModel(ISimulation simulation) => mSimulation = simulation;
        public Func<ISimulationState> UpdateSimulationState => () => mSimulation.CurrentState;
        public string ToggleRunText
        {
            get => mToggleRunText;
            set
            {
                if (value == mToggleRunText) return;
                mToggleRunText = value;
                NotifyOfPropertyChange(() => ToggleRunText);
            }
        }
        public void ExitApplication()
        {
            Application.Current.Exit();
        }
        public async Task ToggleRun()
        {
            if (mSimulation.IsRunning)
            {
                await mSimulation.Stop();
                ToggleRunText = "Start";
            }
            else
            {
                mSimulation.Start();
                ToggleRunText = "Stop";
            }
        }
    }
}