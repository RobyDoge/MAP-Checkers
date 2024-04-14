using System.Windows;

namespace Checkers.View
{
    /// <summary>
    /// Interaction logic for EndGame.xaml
    /// </summary>
    public partial class EndGame : Window
    {
        public bool ReturnToMainMenu { get; set; }
        public EndGame(string winningPlayer)
        {
            InitializeComponent();
            tbWinningPlayer.Content = $"Congratulation {winningPlayer}!";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReturnToMainMenu = true;
            Close();
        }
    }
}
