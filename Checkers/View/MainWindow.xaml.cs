using Checkers.ViewModel;
using System.Windows;

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
    }
}