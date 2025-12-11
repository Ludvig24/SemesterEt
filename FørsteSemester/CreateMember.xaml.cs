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
        Window window; //Window objekt
        public CreateMember(Window window) //Constructor til CreateMember som tager et Window objekt som parameter
        {
            InitializeComponent();
            this.window = window; //Tildeler window objekt fra constructor til den window attribut i klassen

        }
        
        //Click metode til at oprette en Member
        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            byte age; //Tom byte variabel
            char gender = 'x'; //Default placeholder til køn variabel
            string allowedOnlyLetters = "abcdefghijklmnopqrstuvwxyzæøå"; //String over alle tilladte bogstaver i inputfelterne fornavn, efternavn og by
            string NameSurCity = (FornavnBox.Text + EfternavnBox.Text + ByBox.Text).ToLower(); //Variabel der indeholder fornavn, efternavn og by

            //if statement der tjekker om der er tomme input felter
            if (BrugernavnBox.Text == string.Empty || PasswordBox.Text == string.Empty || FornavnBox.Text == string.Empty || EfternavnBox.Text == string.Empty ||  ByBox.Text == string.Empty || AlderBox.Text == string.Empty)
            {
                //Fejlbesked vises hvis et eller flere felter er tomme
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Ingen felter må være tomme";
                return;
            }

            //if statement der tjekker om det valgte biologiske køn er mand eller kvinde
            if (KønBox.Text == "Kvinde")
            {
                //Hvis valgte køn er kvinde sættes gender char variabel til F
                gender = 'F';
            } 
            else if (KønBox.Text == "Mand")
            {
                //Hvis valgte køn er mand sættes gender char variabel til M
                gender = 'M';
            }
            else
            {
                //Fejlbesked vises hvis der ikke vælges noget i dropdown menuen
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Ingen felter må være tomme";
                return;
            }

            //for loop der itererer NameSurCity variabel som et array
            for (int i = 0; i < NameSurCity.Length; i++)
            {
                //if statement der tjekker om NameSurCity indeholder noget andet end de tilladte bogstaver defineret i allowedOnlyLetters
                if (!allowedOnlyLetters.Contains(NameSurCity[i]))
                {   
                    //Hvis allowedOnlyLetters ikke indeholder char variablen på index "i" i NameSurCity vises fejlbesked
                    Fejlbox.Visibility = Visibility.Visible;
                    Fejlbox.Text = "Må kun indeholde bogstaver i Fornavn, Efternavn og by";
                    return;
                }
            }

            //if statement der tjekker om der indgår semikolon i brugernavn eller password input felterne
            if ((BrugernavnBox.Text + PasswordBox.Text).Contains(";"))
            {
                //Fejlbesked vises
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Må ikke indeholde ;";
                return;
            }


            //if statement der tjekker om Byte.TryParse på AlderBox.Text lykkes.
            //TryParse prøver at konvertere stringen i AlderBox.Text til en byte og gemmer resultatet i age variablen
            if (Byte.TryParse(AlderBox.Text, out age) != true) //hvis TryParse lykkes retunere den true og gemmer resultatet i variablen age 
            {
                //hvis TryParse ikke lykkes vises fejlbesked
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Du må kun anvende tal i alder";

                return;

            }

            //if statement der tjekker om age ligger mellem 14 og 130
            if (age <14 || age > 130)
            {
                //Hvis age er udenfor intervallet vises fejlbesked
                Fejlbox.Visibility = Visibility.Visible;
                Fejlbox.Text = "Du skal være mellem 14 og 130 år";
                return;
            }

            //for loop der itererer gemmen alle members
            for (int i = 0; i < UserManager.LoadMember().Count(); i++)
            {
                //For hver iteration tjekker if statement om BrugernavnBox.Text er lig med et username i tekstfilen Members.txt
                if (BrugernavnBox.Text == UserManager.GetUserData(5)[i]) //Henter alle usernames gennem GetUserData(5) som returner en liste af username strings. Tjekker hvert username i listen via [i]
                {
                    //Hvis brugernavnet allerede er taget vises fejlbesked
                    Fejlbox.Visibility = Visibility.Visible;
                    Fejlbox.Text = "Brugernavn er allerede taget";
                    return;
                }


            }


            
            //Kalder CreateMember() metoden og sender alle input felter, gender, og age med som parametre
            UserManager.CreateMember(FornavnBox.Text, EfternavnBox.Text, gender, age, ByBox.Text, BrugernavnBox.Text, PasswordBox.Text);
            //Gør det lækkert at gå tilbage til tidligere vindue efter oprettelse af medlem
            this.Close(); //Lukker CreateMember vinduet
            window.Show(); //Åbner login vinduet
        }

        //Click metode til tilbage knappen
        private void Tilbage_Click(object sender, RoutedEventArgs e)
        {
           
            window.Show(); //Kalder show på login vinduet
            this.Close(); //Kalder close på CreateMember vinduet
        }
    }
}
