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
        //Deklarerer window og member for at kunne bruge dem i denne klasse
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
                ListBoxItem item = new ListBoxItem();
                    StackPanel stackPanel = new StackPanel();
                    item.Content = stackPanel;
                    TextBlock classNameText = new TextBlock();
                    classNameText.Text = $"Holdnavn: {Classes[i].GetClassName()}";
                    TextBlock classActivityText = new TextBlock();
                    classActivityText.Text = $"Aktivitet: {Classes[i].GetActivity()}";
                    TextBlock ClassGenderText = new TextBlock();
                    ClassGenderText.Text = $"Tilladte Køn: {Køn}";
                    TextBlock AlderText = new TextBlock();
                    AlderText.Text = $"Alders Grænse: {Classes[i].GetRequiredMinAge()} - {Classes[i].GetRequiredMaxAge()} år";  // Der vises aldersgrænse som minimums og maksimums alder      
                TextBlock ledigePladser = new TextBlock();
                    ledigePladser.Text = $"Ledige pladser: {Classes[i].GetAvailableSpots() - Classes[i].GetJoinedAmount()}"; // Der beregnes ledige pladser ved at trække joinedAmount fra availableSpots

                // nedeunder tilføjes alle TextBlock elementerne til stackpanelet
                stackPanel.Children.Add(classNameText);
                    stackPanel.Children.Add(classActivityText);
                    stackPanel.Children.Add(ClassGenderText);
                    stackPanel.Children.Add(AlderText);
                    stackPanel.Children.Add(ledigePladser);


                if (member.GetJoinedClasses().Contains(Classes[i].GetClassID()))
                {
                    ListBoxItem tomitem = new ListBoxItem();
                    ClassesList.Items.Add(tomitem);
                    tomitem.IsEnabled = false;
                    tomitem.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (Classes[i].GetStatus() == true)
                    {
                        //Tilføjer item til listen med holdet
                        item.BorderBrush = Brushes.Red;
                        ClassesList.Items.Add(item);
                    }
                    else
                    {
                        ClassesList.Items.Add(item);
                    }
                }
                
            }
        }
        private void Tilmeld_Click(object sender, RoutedEventArgs e)
        {
            
            List<Class> Teams = member.LoadTeams();
            Class team = new Class();
            int classID = ClassesList.SelectedIndex +1; //Skal ændres hvis vi kunne tænke os at ændre rækkefølgen
            
            int j = 0;
            while (j < Teams.Count)
            {
                if (classID == Teams[j].GetClassID())
                {
                    team = Teams[j];
                }
                j++;
            }

            if (team.GetAvailableSpots() == team.GetJoinedAmount())
            {
                    team.SetStatus(true);
                    MessageBox.Show("Der er desværre ikke flere ledige pladser på dette hold.", "Fyldt Hold");
                return;
            }
            else
            {
                
            }

                char GetRequiredGenderInChar = team.GetRequiredGender();
           
            if (team.GetRequiredGender() == member.GetGender() || team.GetRequiredGender() == 'B')
            {
               
            } else
            {
                MessageBox.Show("Du opfylder ikke kønskravet for dette hold.", "Uopfyldt Krav");
                return;
            }

            if (member.GetAge() < team.GetRequiredMinAge() || member.GetAge() > team.GetRequiredMaxAge())
            {
                MessageBox.Show("Du opfylder ikke alderskravet for dette hold.", "Uopfyldt krav");
                return;
            }
            else
            {
                
            }

                member.JoinClass(classID, member.GetUserID());
            
            int i = 0;
            while (i < Teams.Count)
            {
                if (classID == Teams[i].GetClassID())
                {
                    Teams[i].AddMemberIDToClass(member.GetUserID(), classID);
                    Teams[i].SetJoinedAmount(Teams[i].GetJoinedAmount() + 1);
                }
                i++;
            }
            // så for at blokkere den box efter tilmelding, så man ikke tilmelder sig det samme hold flere gange
            ListBoxItem tomitem = new ListBoxItem();
            ListBoxItem listBoxItem = (ListBoxItem)ClassesList.SelectedItem;
            listBoxItem = tomitem;
            ClassesList.Items.Insert(ClassesList.SelectedIndex,listBoxItem);
            ClassesList.Items.RemoveAt(ClassesList.SelectedIndex);
            tomitem.IsEnabled = false;
            tomitem.Visibility = Visibility.Hidden;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Tilbageknap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            window.Show();
        }
    }
}
