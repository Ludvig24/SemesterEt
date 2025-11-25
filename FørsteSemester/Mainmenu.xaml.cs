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

namespace FørsteSemester
{
    /// <summary>
    /// Interaction logic for Mainmenu.xaml
    /// </summary>
    public partial class Mainmenu : Window
    {
        Window window;
        public Mainmenu(Window window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void Logud_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            window.Show();
        }
    }
}
