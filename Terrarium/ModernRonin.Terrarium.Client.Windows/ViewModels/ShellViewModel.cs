using Windows.UI.Xaml;
using Caliburn.Micro;

namespace ModernRonin.Terrarium.Client.Windows.ViewModels
{
    public class ShellViewModel : Screen
    {
        readonly ISampleService mService;
        public ShellViewModel(ISampleService service) => mService = service;
        public string Message => mService.Message;
        public void ExitApplication()
        {
            Application.Current.Exit();
        }
    }
}