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
        Window window;
        Member member;

        internal JoinClass(Window window, Member member)
        {
            this.window = window;
            this.member = member;
            InitializeComponent();



            List<Class> Classes = member.LoadTeams();
            for (int i = 0; i < Classes.Count; i++)
            {
                char GetRequiredGenderInChar = Classes[i].GetRequiredGender();
                //Nedenfor konverteres char til string for at kunne vises i listen og for at det er mere
                //brugervenligt at se hele ordet for kønnet end kun engelsk forbukstav
                string Køn = "";
              
                switch (GetRequiredGenderInChar)
                {
                    case 'F':
                        
                        Køn = "Kvinde";
                        break;
                        
                    case 'M':
                        
                        Køn = "Mand";
                        break;
                    
                    case 'B':
                        
                        Køn = "Begge køn";
                        break;
                }

                if (Classes[i].GetStatus() == false) //Get status refere til om holdet er fyldt, så hvis ikke holdet er fyldt vil denne if statment virke
                {

                    ListBoxItem item = new ListBoxItem();
                    //item.Content = $"{Classes[i].GetClassName()} - {Classes[i].GetActivity()} - Ledige pladser: {Classes[i].GetAvailableSpots()}";
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
                    //TextBlock ClassMaxAge = new TextBlock();
                    //ClassMaxAge.Text = $"Maksimum Alder: {Classes[i].GetRequiredMaxAge()}";
                    TextBlock ledigePladser = new TextBlock();
                    ledigePladser.Text = $"Ledige pladser: {Classes[i].GetAvailableSpots() - Classes[i].GetJoinedAmount()}"; //Fix regnestykke


                    stackPanel.Children.Add(classNameText);
                    stackPanel.Children.Add(classActivityText);
                    stackPanel.Children.Add(ClassGenderText);
                    stackPanel.Children.Add(AlderText);
                    //stackPanel.Children.Add(ClassMaxAge);
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
                        //Tilføjer item til listen med holdet
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
                    FejlBox.Visibility = Visibility.Visible;
                    FejlBox.Text = "Der er desværre ikke flere ledige pladser på dette hold.";
                    
                return;
            }
            else
            {
                FejlBox.Visibility = Visibility.Hidden;
            }

                char GetRequiredGenderInChar = team.GetRequiredGender();
           
            if (team.GetRequiredGender() == member.GetGender() || team.GetRequiredGender() == 'B')
            {
                FejlBox.Visibility = Visibility.Hidden;
            } else
            {
                FejlBox.Visibility = Visibility.Visible;
                FejlBox.Text = "Du opfylder ikke kønskravet for dette hold.";
                return;
            }

            if (member.GetAge() < team.GetRequiredMinAge() || member.GetAge() > team.GetRequiredMaxAge())
            {
                FejlBox.Visibility = Visibility.Visible;
                FejlBox.Text = "Du opfylder ikke alderskravet for dette hold.";
                return;
            }
            else
            {
                FejlBox.Visibility = Visibility.Hidden;
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
