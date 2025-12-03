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
    /// Interaction logic for ClassOverview.xaml
    /// </summary>
    public partial class ClassOverview : Window
    {

        Window window;
        Member member;
        internal ClassOverview(Window window, Member member)
        {
            this.window = window;
            this.member = member;
            InitializeComponent();

            //Her har jeg koppieret koden fra JoinClass for at vise de hold som medlemmet er tilmeldt

            List<Class> membersClasses = new List<Class>();
            List<Class> Classes = member.LoadTeams();
            foreach (Class cls in Classes)
            {
                if (member.GetJoinedClasses().Contains(cls.GetClassID()))
                {
                    membersClasses.Add(cls);
                }
            }   
            for (int i = 0; i < membersClasses.Count; i++)
            {
                //Nedenfor konverteres char til string for at kunne vises i listen og for at det er mere
                //brugervenligt at se hele ordet for kønnet end kun engelsk forbukstav
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
                    //item.Content = $"{Classes[i].GetClassName()} - {Classes[i].GetActivity()} - Ledige pladser: {Classes[i].GetAvailableSpots()}";
                    StackPanel stackPanel = new StackPanel();
                    item.Content = stackPanel;
                    TextBlock classNameText = new TextBlock();
                    classNameText.Text = $"Holdnavn: {membersClasses[i].GetClassName()}";
                    TextBlock classActivityText = new TextBlock();
                    classActivityText.Text = $"Aktivitet: {membersClasses[i].GetActivity()}";
                    TextBlock antalPladser = new TextBlock();
                    antalPladser.Text = $"Antal pladser: {membersClasses[i].GetAvailableSpots()}";
                    TextBlock ledigePladser = new TextBlock();
                    ledigePladser.Text = $"Ledige pladser: {membersClasses[i].GetAvailableSpots() - Classes[i].GetJoinedAmount()}";


                    stackPanel.Children.Add(classNameText);
                    stackPanel.Children.Add(classActivityText);
                    stackPanel.Children.Add(ledigePladser);

                    ClassesListBox.Items.Add(item);
                    


                
            }
        }
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            window.Show();
        }
    }
}
