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
        Window window;
        public CreateMember(Window window)
        {
            InitializeComponent();
            this.window = window;
        }

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            byte age;
            char gender = 'x';

            if (KønBox.Text == "Kvinde")
            {
                gender = 'F';
            } else gender = 'M';

            if ((FornavnBox.Text + EfternavnBox.Text + ByBox.Text).Contains(";" )) //Enten fang special tegn eller tal
            {
               
                
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Må ikke indeholde specialtegn og tal";
                return;
            }

            if ((BrugernavnBox.Text + PasswordBox.Text).Contains(";"))
            {
               
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Må ikke indeholde ;";
                return;
            }

            
            if (Byte.TryParse(AlderBox.Text, out age) != true)
            {
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Du må kun anvende tal";
                
                return;

            }
            if (age <14 || age > 130)
            {
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Du skal være mellem 14 og 130 år";
                return;
            }
            for (int i = 0; i < UserManager.LoadMember().Count(); i++)
            {
                if (BrugernavnBox.Text == UserManager.GetUserData(5)[i])
                {
                    Fejlbox.Visibility = Visibility.Visible;
                    Fejlbox.Text = "Brugernavn er allerede taget";
                    return;
                }


            }

            UserManager.CreateMember(FornavnBox.Text, EfternavnBox.Text, gender, age, ByBox.Text, BrugernavnBox.Text, PasswordBox.Text);
        }

        

        private void Tilbage_Click(object sender, RoutedEventArgs e)
        {
            window.Show();
            this.Close();
        }
    }
}
