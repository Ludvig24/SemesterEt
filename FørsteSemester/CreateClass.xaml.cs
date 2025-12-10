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


           
            if ((AktivitetBox.Text + HoldNavnBox.Text).Contains(";"))
            {
                MessageBox.Show("Aktivitet og Holdnavn må ikke indeholde ;"); //Besked der viser at aktivitet eller holdnavn indeholder semikolon
                return; //Afslutter metoden hvis betingelsen er opfyldt
            }

            //if sætning som tjekker det køn der er valgt i dropdown og sætter det tilsvarende char
            if (KønBox.Text == "Kvinde")
            {
                gender = 'F';
            }
            else if (KønBox.Text == "Mand")
            {
                gender = 'M';
            }

            if (AktivitetBox.Text == string.Empty || HoldNavnBox.Text == string.Empty || PladsBox.Text == string.Empty || AlderMaxBox.Text == string.Empty || AlderMinBox.Text == string.Empty || AlderMaxBox.Text == string.Empty || AlderMinBox.Text == string.Empty)
            //Tjekker om nogle af tekstboksene er tomme eller om aldersgrænserne er udenfor rimelige værdier
            {
                MessageBox.Show("Udfyld venligst alle felter korrekt!"); //Besked der viser at nogle felter ikke er udfyldt korrekt
                return; //Afslutter metoden hvis betingelsen er opfyldt
            }

            //Tjekker om aktivitet og holdnavn kun indeholder bogstaver
            string allowedOnlyLetters = "abcdefghijklmnopqrstuvwxyzæøå"; //string over alle tilladte bogstaver i inputfelterne aktivitet og holdnavn
            string activityClassName = ( HoldNavnBox.Text).ToLower(); //variabel der indeholder aktivitet og holdnavn, gør dem til lover så det kan sammenlignes
            for (int i = 0; i < activityClassName.Length; i++) //for loop der itererer activityClassName variabel som et array
            {
                if (!allowedOnlyLetters.Contains(activityClassName[i])) //if statement der tjekker om activityClassName indeholder noget andet end de tilladte bogstaver defineret i allowedOnlyLetters
                {
                    MessageBox.Show("Aktivitet og Holdnavn må kun indeholde bogstaver!"); //Besked der viser at aktivitet eller holdnavn indeholder ulovlige tegn
                    return; //Afslutter metoden hvis betingelsen er opfyldt
                }
            }

            if (Convert.ToByte(AlderMinBox.Text) > Convert.ToByte(AlderMaxBox.Text))
            {
                MessageBox.Show("Den maksimale alder skal være højere end den minimale alder!"); //Besked der viser at aldersgrænserne er forkerte
                return; //Afslutter metoden hvis betingelsen er opfyldt
            }

            if (Convert.ToByte(AlderMinBox.Text) <= 14)
            {
                if(Convert.ToByte(AlderMaxBox.Text) >= 130)
                {
                    MessageBox.Show("Den maksimale alder må ikke være over 130 år!"); //Besked der viser at den maksimale alder er for høj
                    return; //Afslutter metoden hvis betingelsen er opfyldt
                }
                MessageBox.Show("Den minimale alder skal være mindst 14 år!"); //Besked der viser at den minimale alder er for lav
                return; //Afslutter metoden hvis betingelsen er opfyldt
            }
            if (Convert.ToByte(AlderMaxBox.Text) >= 130)
            {
                if (Convert.ToByte(AlderMinBox.Text) <= 14)
                {
                    MessageBox.Show("Den minimale alder skal være mindst 14 år!"); //Besked der viser at den minimale alder er for lav
                    return; //Afslutter metoden hvis betingelsen er opfyldt
                }
                MessageBox.Show("Den maksimale alder må ikke være over 130 år!"); //Besked der viser at den maksimale alder er for høj
                return; //Afslutter metoden hvis betingelsen er opfyldt
            }
            if (Convert.ToByte(PladsBox.Text) <= 0)
            {
                MessageBox.Show("Antal pladser skal være større end 0!"); //Besked der viser at antal pladser er for lavt
                return; //Afslutter metoden hvis betingelsen er opfyldt
            }
            if(KønBox.Text == "")
            {
                MessageBox.Show("Vælg venligst et køn!"); //Besked der viser at køn ikke er valgt
                return; //Afslutter metoden hvis betingelsen er opfyldt
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
