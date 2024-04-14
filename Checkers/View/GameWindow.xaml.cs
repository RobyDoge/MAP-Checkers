using System.Windows;
using Checkers.ViewModel;

namespace Checkers.View
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public GameWindow()
        {
            InitializeComponent();
            this.DataContext = new GameVM();
        }
        public GameWindow(string gameName)
        {
            InitializeComponent();
            this.DataContext = new GameVM(gameName);
        }

        private void btnBackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
    }
}
