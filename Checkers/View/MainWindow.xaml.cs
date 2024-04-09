using System.Windows;

namespace Checkers.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btNewGame_Click(object sender, RoutedEventArgs e)
        {
            var game = new GameWindow();
            game.Show();
            Close();
        }
    }
}