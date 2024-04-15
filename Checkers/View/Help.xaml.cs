using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Checkers.View
{
    /// <summary>
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
            string description = @"Description:
                    The game of checkers is played on an 8x8 board with 12 pieces on each side.
                    The pieces can only move diagonally and can only move forward.
                    If a piece reaches the opposite end of the board, it becomes a king and can move in any direction.
                    The objective of the game is to capture all of the opponent's pieces or block them so they cannot move.
                    The game ends when one player has no legal moves left or all of their pieces are captured.";
            tbDescription.Text = description;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainMeun = new MainWindow();
            mainMeun.Show();
            Close();
        }
    }
}
