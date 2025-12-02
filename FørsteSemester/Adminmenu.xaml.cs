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
    /// Interaction logic for Adminmenu.xaml
    /// </summary>
    public partial class Adminmenu : Window
    {
        Admin admin;
        Window window;
         internal Adminmenu(Admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void OpretHold_Click(object sender, RoutedEventArgs e)
        {
            CreateClass createclass = new CreateClass(admin);
            createclass.Show();
            this.Hide();
        }

        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
