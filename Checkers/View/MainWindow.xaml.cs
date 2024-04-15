using Checkers.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Checkers.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainMenuVM MMVM { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            MMVM = new MainMenuVM();
            chbMultipleJump.IsChecked = MMVM.MultipleJumps;
        }

        private void btNewGame_Click(object sender, RoutedEventArgs e)
        {
            var game = new GameWindow();
            game.Show();
            Close();
        }

        private void chbMultipleJump_Checked(object sender, RoutedEventArgs e)
        {
            MMVM.ChangeMultipleJumps(chbMultipleJump.IsChecked.Value);
        }

        private void CmbOpenGame_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var gameName = (string)cmbOpenGame.SelectedItem;
            if (gameName != null)
            {
                var game = new GameWindow(gameName);
                game.Show();
                Close();
            }
        }

        private void btStatistics_Click(object sender, RoutedEventArgs e)
        {
            var statisticsWindow = new Statistics();
            statisticsWindow.Show();
            Close();
        }
    }
}