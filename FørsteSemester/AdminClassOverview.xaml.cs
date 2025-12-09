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
        Window window;
        Admin admin;

        internal AdminClassOverview(Window window, Admin admin)
        {
            this.window = window;
            this.admin = admin;
            InitializeComponent();

            //Her har jeg koppieret koden fra JoinClass for at vise de hold som medlemmet er tilmeldt
            //Her har jeg kopieret koden fra JoinClass for at vise de hold som medlemmet er tilmeldt

            List<Class> membersClasses = new List<Class>();
            List<Class> Classes = admin.LoadTeams();
            List<Class> membersClasses = new List<Class>(); 
            List<Class> Classes = admin.LoadTeams();//liste over oprettede hold
            
            for (int i = 0; i < Classes.Count; i++)
            {
                //Nedenfor konverteres char til string for at kunne vises i listen og for at det er mere
                //brugervenligt at se hele ordet for kønnet end kun engelsk forbukstav
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

                ListBoxItem item = new ListBoxItem();
                ListBoxItem item = new ListBoxItem();//opretter en ListBoxItem

                StackPanel stackPanel = new StackPanel();
                item.Content = stackPanel;
                TextBlock classNameText = new TextBlock();
                classNameText.Text = $"Holdnavn: {Classes[i].GetClassName()}";
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

                stackPanel.Children.Add(classNameText);
                stackPanel.Children.Add(classActivityText);
                stackPanel.Children.Add(ClassGenderText);
                stackPanel.Children.Add(AlderText);
                stackPanel.Children.Add(antalTilmeldte);
                stackPanel.Children.Add(antalPladser);

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
