using Windows.UI.Xaml;
using MonoGame.Framework;

namespace MonoGameUwpXaml
{
    public sealed partial class GamePage
    {
        readonly Game1 mGame;
        public GamePage()
        {
            InitializeComponent();

            // Create the game.
            var launchArguments = string.Empty;
            mGame = XamlGame<Game1>.Create(launchArguments, Window.Current.CoreWindow, SwapChainPanel);
        }
        void OnStart(object sender, RoutedEventArgs e) { }
        void OnStop(object sender, RoutedEventArgs e) { }
    }
}