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
    /// Interaction logic for InputGameName.xaml
    /// </summary>
    public partial class InputGameName : Window
    {
        public InputGameName()
        {
            InitializeComponent();
        }
        public string GameName { get; private set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameName = tbGameName.Text;
            Close();
        }
    }
}
