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
        Admin admin;

        internal CreateClass(Admin admin)
        {
            this.admin = admin;
            InitializeComponent();
        }

        //Opretter et hold baseret på input fra tekstboksene når knappen "OpretHold" bliver klikket
        private void OpretHold_Click(object sender, RoutedEventArgs e)
        {
            char gender = 'B'; //standardværdi for begge køn

            //if sætning som tjekker det køn der er valgt i dropdown og sætter det tilsvarende char
            if (KønBox.Text == "Kvinde")
            {
                gender = 'F';
            }
            else if (KønBox.Text == "Mand") 
            { 
                gender = 'M'; 
            }

            //Kalder CreateClass metoden fra Admin klassen med de værdier der er indtastet i tekstboksene
            admin.CreateClass(AktivitetBox.Text, HoldNavnBox.Text, Convert.ToByte(PladsBox.Text), gender ,Convert.ToByte(AlderMaxBox.Text), Convert.ToByte(AlderMinBox.Text));
            MessageBox.Show("Hold oprettet!"); //Besked der viser at holdet er oprettet
            this.Close();
            Adminmenu adminmenu = new Adminmenu(admin);
            adminmenu.Show();
        }

        //Går tilbage til Adminmenu vinduet når "TilbageKnap" bliver klikket
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Adminmenu adminmenu = new Adminmenu(admin);
            adminmenu.Show();
        }
    }
}
