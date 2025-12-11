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
    /// Interaction logic for AdminClassOverview.xaml
    /// </summary>
    public partial class AdminClassOverview : Window
    {
        //Her opretter vi to objekter som kan bruges i hele klassen
        Window window;
        Admin admin;

        internal AdminClassOverview(Window window, Admin admin) //Constructor til AmdminClassOverview - tager et Window og et Admin objekt som parameter
        {
            //Her opretter vi komponenterne i vinduet. og så gemmer vi window og admin som vi fik fra tidligere vindue, til window og admin der er i dettevindue
            this.window = window;
            this.admin = admin;
            InitializeComponent();

            List<Class> membersClasses = new List<Class>(); 
            List<Class> Classes = admin.LoadTeams();//Liste over oprettede hold
            
            //For loop der for hver Class opretter en ListBoxItem der indholder informationerne omkring den Class
            for (int i = 0; i < Classes.Count; i++)
            {
                //Nedenfor konverteres char til string for at kunne vises i listen og for at det er mere
                //brugervenligt at se hele ordet for kønnet end kun engelsk forbogstav
                char GetRequiredGenderInChar = Classes[i].GetRequiredGender();
                string Køn = "";
                if (GetRequiredGenderInChar == 'F')
                {
                    Køn = "Kvinde";
                }
                else if (GetRequiredGenderInChar == 'M')
                {
                    Køn = "Mand";
                }
                if (GetRequiredGenderInChar == 'B')
                {
                    Køn = "Begge køn";
                }

                ListBoxItem item = new ListBoxItem();//Opretter en ListBoxItem

                StackPanel stackPanel = new StackPanel(); //Opretter en instans af StackPanel som vi bruger til at stable en række tekstfelter i WPF vinduet
                item.Content = stackPanel; //Tildeler stackPanel til vores ListBoxItem
                TextBlock classNameText = new TextBlock(); //Opretter en instans af TextBlock
                classNameText.Text = $"Holdnavn: {Classes[i].GetClassName()}"; //Tildeler Textblocken navnet på den Class på index "i" i Classes gennem GetClassName()
                TextBlock classActivityText = new TextBlock();
                classActivityText.Text = $"Aktivitet: {Classes[i].GetActivity()}";
                TextBlock ClassGenderText = new TextBlock();
                ClassGenderText.Text = $"Tilladte Køn: {Køn}";
                TextBlock AlderText = new TextBlock();
                AlderText.Text = $"Alders Grænse: {Classes[i].GetRequiredMinAge()} - {Classes[i].GetRequiredMaxAge()} år";               
                TextBlock antalTilmeldte = new TextBlock();
                antalTilmeldte.Text = $"Antal Tilmeldte: {Classes[i].GetJoinedAmount()}";
                TextBlock antalPladser = new TextBlock();
                antalPladser.Text = $"Antal Pladser: {Classes[i].GetAvailableSpots()}";

                //Tilføjer alle TextBlocke som Children af vores stackPanel hvilket medfører at de bliver stablet i WPF
                stackPanel.Children.Add(classNameText);
                stackPanel.Children.Add(classActivityText);
                stackPanel.Children.Add(ClassGenderText);
                stackPanel.Children.Add(AlderText);
                stackPanel.Children.Add(antalTilmeldte);
                stackPanel.Children.Add(antalPladser);

                //Tilføjer vores ListBoxItem item til selve ListBoxen ClassesListBox
                ClassesListBox.Items.Add(item);
            }
        }

        //Knap der lukker vinduet og går tilbage til Adminmenu vinduet
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Adminmenu adminmenu = new Adminmenu(admin);     
            adminmenu.Show();
        }
    }
}
