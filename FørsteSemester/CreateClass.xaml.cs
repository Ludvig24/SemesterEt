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
        // Her opretter vi et admin objekt som kan bruges i hele klassen
        Admin admin;

        // Konstruktor for CreateClass klassen, som tager et Admin objekt som parameter
        internal CreateClass(Admin admin)
        {
            // Her opretter vi komponenterne i vinduet. og så gemmer vi admin som vi fik fra tidligere vindue til admin der er i dettevindue
            this.admin = admin;
            InitializeComponent(); // Initialiserer komponenterne i vinduet
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
            admin.CreateClass(AktivitetBox.Text, HoldNavnBox.Text, Convert.ToByte(PladsBox.Text), gender ,Convert.ToByte(AlderMaxBox.Text), Convert.ToByte(AlderMinBox.Text)); //Opretter holdet
            MessageBox.Show("Hold oprettet!"); //Besked der viser at holdet er oprettet
            this.Close();
            Adminmenu adminmenu = new Adminmenu(admin);
            adminmenu.Show();
        }

        //Går tilbage(gemmer nuværende vindue og laver en ny adminvindue) til Adminmenu vinduet når "TilbageKnap" bliver klikket
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Adminmenu adminmenu = new Adminmenu(admin); //Opretter et nyt Adminmenu vindue
            adminmenu.Show();
        }
    }
}
