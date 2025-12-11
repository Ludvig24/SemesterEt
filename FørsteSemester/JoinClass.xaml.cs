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
    /// Interaction logic for JoinClass.xaml
    /// </summary>
    public partial class JoinClass : Window
    {
        //Opretter window og member objekter for at kunne bruge dem i denne klasse
        Window window;
        Member member;

        //Constructor for JoinClass som tager et window og et member objekt som parametre
        internal JoinClass(Window window, Member member)
        {
            // Kopler de parameter der er sendt med fra tidligere vindue sammmen med dette vindues nye parametere
            this.window = window;
            this.member = member;
            InitializeComponent(); // Og opretter komponenterne i vinduet


            List<Class> Classes = member.LoadTeams(); //Der laves en liste af Classes som indeholder alle holdene der er oprettet
            for (int i = 0; i < Classes.Count; i++) //For loop der går igennem alle holdene i listen
            {
                if (Classes[i].GetAvailableSpots() == Classes[i].GetJoinedAmount()) //Tjekker om der er ledige pladser på holdet
                {
                    Classes[i].SetStatus(true); //Hvis der ikke er ledige pladser sættes status til true (fuldt hold)
                }

                    //Nedenfor konverteres char til string for at kunne vises i listen og for at det er mere
                    //brugervenligt at se hele ordet for kønnet end kun engelsk forbukstav
                    char GetRequiredGenderInChar = Classes[i].GetRequiredGender();
                string Køn = "";

                switch (GetRequiredGenderInChar) //Switch case der tjekker værdien af GetRequiredGenderInChar, og tager char forkortelsen og laver om til en string
                {
                    case 'F':

                        Køn = "Kvinde";
                        break; //Der bruges break for at den kører videre og ikke til næste case

                    case 'M':

                        Køn = "Mand";
                        break;

                    case 'B':

                        Køn = "Begge køn";
                        break;
                }

                //Opretter et ListBoxItem for hvert hold, og fylder det med information om holdet ved at lave et stackpanel
                ListBoxItem item = new ListBoxItem(); //Opretter en ListBoxItem
                StackPanel stackPanel = new StackPanel(); //Opretter en instans af StackPanel som vi bruger til at stable en række tekstfelter i WPF vinduet
                item.Content = stackPanel;//Tildeler stackPanel til vores ListBoxItem
                TextBlock classNameText = new TextBlock(); //Opretter en instans af TextBlock
                classNameText.Text = $"Holdnavn: {Classes[i].GetClassName()}";
                    TextBlock classActivityText = new TextBlock();
                    classActivityText.Text = $"Aktivitet: {Classes[i].GetActivity()}";
                    TextBlock ClassGenderText = new TextBlock();
                    ClassGenderText.Text = $"Tilladte Køn: {Køn}";
                    TextBlock AlderText = new TextBlock();
                    AlderText.Text = $"Alders Grænse: {Classes[i].GetRequiredMinAge()} - {Classes[i].GetRequiredMaxAge()} år";  // Der vises aldersgrænse som minimums og maksimums alder      
                TextBlock ledigePladser = new TextBlock();
                    ledigePladser.Text = $"Ledige pladser: {Classes[i].GetAvailableSpots() - Classes[i].GetJoinedAmount()}"; // Der beregnes ledige pladser ved at trække joinedAmount fra availableSpots

                // Nedeunder tilføjes alle TextBlock elementerne til stackpanelet
                    stackPanel.Children.Add(classNameText);
                    stackPanel.Children.Add(classActivityText);
                    stackPanel.Children.Add(ClassGenderText);
                    stackPanel.Children.Add(AlderText);
                    stackPanel.Children.Add(ledigePladser);

                //Her laves en if sætning der tjekker om medlemmet allerede er tilmeldt holdet
                //Hvis medlemmet er tilmeldt holdet, tilføjes et tomt og usynligt item i listen i stedet for holdet.
                //Dette gøres for at forhindre at medlemmet kan tilmelde sig det samme hold flere gange
                //Der tilføjet et tomt item i stedet for at fjerne holdet fra listen, for at bevare rækkefølgen af holdene i listen
                if (member.GetJoinedClasses().Contains(Classes[i].GetClassID()))
                {
                    ListBoxItem tomitem = new ListBoxItem(); //Opretter et tomt ListBoxItem
                    ClassesList.Items.Add(tomitem); //Tilføjer det tomme item til listen
                    tomitem.IsEnabled = false; //Deaktiverer itemet så det ikke kan vælges
                    tomitem.Visibility = Visibility.Hidden; //Gør itemet usynligt
                }
                else
                {
                    if (Classes[i].GetStatus() == true) //Tjekker om holdet er fuldt
                    {
                        //Tilføjer item til listen med holdet
                        item.BorderBrush = Brushes.Red; //Sætter border farven til rød for at indikere at holdet er fuldt
                        ClassesList.Items.Add(item); //Tilføjer item til listen som nu er gjort rødt
                    }
                    else
                    {
                        ClassesList.Items.Add(item); //Hvis holdet ikke er fuldt tilføjes item til listen som normalt
                    }
                }
                
            }
        }
        //Metode der håndterer klik på tilmeld knappen
        private void Tilmeld_Click(object sender, RoutedEventArgs e)
        {
            // Henter listen af hold, som medlemmet kan tilmelde sig
            List<Class> Teams = member.LoadTeams();
            Class team = new Class(); // Opretter et tomt Class objekt til at holde det valgte hold
            int classID = ClassesList.SelectedIndex +1; // Får classID baseret på det valgte indeks i listen (tilføjer 1 fordi indeks starter ved 0)

            //Det som denne while loop gør er at sammenkople det valgte indeks med det hold der er i hold listen med alle hold.
            int j = 0; //Laver en tæller j til at iterere gennem holdene
            while (j < Teams.Count) //Loop der går igennem alle holdene
            {
                if (classID == Teams[j].GetClassID()) //Tjekker om det valgte classID matcher holdets ID 
                {
                    team = Teams[j]; //Hvis der er et match, sættes team objektet til det valgte hold
                }
                j++; //Tælleren øges for at gå til næste hold
            }
            //Tjekker om holdet er fuldt
            if (team.GetAvailableSpots() == team.GetJoinedAmount())
            {
                //Hvis holdet er fuldt, sættes status til true og der vises en besked til brugeren
                team.SetStatus(true);
                    MessageBox.Show("Der er desværre ikke flere ledige pladser på dette hold.", "Fyldt Hold");
                return;
            }
            //Hvis holdet ikke er fuldt, fortsætter processen
            else
            {
                
            }

            //laver en variabel der henter det krævede køn for holdet
            char GetRequiredGenderInChar = team.GetRequiredGender();
            //Tjekker om medlemmet opfylder kønskravet for holdet
            if (team.GetRequiredGender() == member.GetGender() || team.GetRequiredGender() == 'B') //Hvis ens køn er det samme som holdets køn, eller hvis holdets køn er begge køn gå den videre
            {
                //Hvis kønskravet er opfyldt, fortsætter processen
            }
            else
            {
                //Hvis kønskravet ikke er opfyldt, vises en besked til brugeren
                MessageBox.Show("Du opfylder ikke kønskravet for dette hold.", "Uopfyldt Krav");
                return;
            }

            if (member.GetAge() < team.GetRequiredMinAge() || member.GetAge() > team.GetRequiredMaxAge()) //Tjekker om medlemmet opfylder alderskravet for holdet
            {
                //Hvis alderskravet ikke er opfyldt, vises en besked til brugeren
                MessageBox.Show("Du opfylder ikke alderskravet for dette hold.", "Uopfyldt krav");
                return;
            }
            else
            {
                //Hvis alderskravet er opfyldt, fortsætter processen
            }

            // Kalder JoinClass metoden på member objektet for at tilmelde medlemmet til holdet. Gå til JoinClass definition for mere info
            member.JoinClass(classID, member.GetUserID());//Her tilføjes medlemmet til holdet i Members.txt filen

            int i = 0;
            while (i < Teams.Count) //Loop der går igennem alle holdene
            {
                if (classID == Teams[i].GetClassID()) //Tjekker om det valgte classID matcher holdets ID
                {
                    //Hvis der er et match, tilføjes medlemmet til holdet og joinedAmount øges med 1
                    Teams[i].AddMemberIDToClass(member.GetUserID(), classID);//Her tilføjes medlemmet til holdet i Classes.txt filen
                    Teams[i].SetJoinedAmount(Teams[i].GetJoinedAmount() + 1); //Øger joinedAmount med 1
                }
                i++;
            }
            //Når medlemmet tilmelder sig holdet, tilføjes et tomt og usynligt item i listen i stedet for holdet.
            //Dette gøres for at forhindre at medlemmet kan tilmelde sig det samme hold flere gange
            //Der tilføjet et tomt item i stedet for at fjerne holdet fra listen, for at bevare rækkefølgen af holdene i listen
            ListBoxItem tomitem = new ListBoxItem(); //Opretter et tomt ListBoxItem
            ListBoxItem listBoxItem = (ListBoxItem)ClassesList.SelectedItem; //Henter det valgte item i listen
            listBoxItem = tomitem; //Erstatter det valgte item med et tomt item
            ClassesList.Items.Insert(ClassesList.SelectedIndex,listBoxItem);//Indsætter det tomme item på den valgte position
            ClassesList.Items.RemoveAt(ClassesList.SelectedIndex); //Fjerner det oprindelige item
            tomitem.IsEnabled = false; //Deaktiverer det tomme item
            tomitem.Visibility = Visibility.Hidden; //Gør det tomme item usynligt

        }

        // Metode der sender en tilbage til tidligere vindue
        private void Tilbageknap_Click(object sender, RoutedEventArgs e) 
        {
            this.Hide(); // Skjuler det nuværende vindue
            window.Show(); // Viser det tidligere vindue
        }
    }
}
