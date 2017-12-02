using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using ModernRonin.Terrarium.Logic;

namespace ModernRonin.Terrarium.Client.Windows.ViewModels
{
    public class ShellViewModel : Screen
    {
        readonly ISimulation mSimulation;
        string mToggleRunText = "Start";
        public ShellViewModel(ISimulation simulation, Action<SwapChainPanel> setupView)
        {
            SetupView = setupView;
            mSimulation = simulation;
        }
        public Action<SwapChainPanel> SetupView { get; }
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