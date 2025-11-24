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
    /// Interaction logic for CreateMember.xaml
    /// </summary>
    public partial class CreateMember : Window
    {
        public CreateMember()
        {
            InitializeComponent();
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            char gender = 'x';

            if (KønBox.Text == "Kvinde")
            {
                gender = 'F';
            } else gender = 'M';

            UserManager.CreateMember(FornavnBox.Text, EfternavnBox.Text, gender, Convert.ToByte(AlderBox.Text),  ByBox.Text, BrugernavnBox.Text, PasswordBox.Text);
        }
    }
}
