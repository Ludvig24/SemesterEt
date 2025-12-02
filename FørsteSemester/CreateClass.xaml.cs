using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for CreateClass.xaml
    /// </summary>
    public partial class CreateClass : Window
    {
        Window window;
        Admin admin;
        internal CreateClass(Admin admin)
        {
            
            InitializeComponent();
            this.admin = admin;
        }

        private void OpretHold_Click(object sender, RoutedEventArgs e)
        {
            char gender = 'B';
            if (KønBox.Text == "Kvinde")
            {
                gender = 'F';
            }
            else if (KønBox.Text == "Mand") 
            { 
                gender = 'M'; 
            }

            admin.CreateClass(AktivitetBox.Text, HoldNavnBox.Text, Convert.ToByte(PladsBox.Text), gender ,Convert.ToByte(AlderMaxBox.Text), Convert.ToByte(AlderMinBox.Text));
        }

        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            window.Show();
        }
    }
}
