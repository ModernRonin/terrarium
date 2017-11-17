using Windows.UI.Xaml;
using MonoGame.Framework;

namespace MonoGameUwpXaml
{
    public sealed partial class GamePage
    {
        readonly Game1 _game;
        public GamePage()
        {
            InitializeComponent();

            // Create the game.
            var launchArguments = string.Empty;
            _game = XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);
        }
        void OnStart(object sender, RoutedEventArgs e) { }
        void OnStop(object sender, RoutedEventArgs e) { }
    }
}