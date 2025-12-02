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
                if (Classes[i].GetStatus() == false) //Get status refere til om holdet er fyldt, så hvis ikke holdet er fyldt vil denne if statment virke
                {
                    char GetRequiredGenderInChar = Classes[i].GetRequiredGender();

                    if (GetRequiredGenderInChar = F)
                    {   if (member.GetGender() != 'F')
                        {
                            continue;
                        }
                    }
                    else if (GetRequiredGenderInChar == 'M')
                    {
                        if (member.GetGender() != 'M')
                        {
                            continue;
                        }

                        ListBoxItem item = new ListBoxItem();
                    //item.Content = $"{Classes[i].GetClassName()} - {Classes[i].GetActivity()} - Ledige pladser: {Classes[i].GetAvailableSpots()}";
                    StackPanel stackPanel = new StackPanel();
                    item.Content = stackPanel;
                    TextBlock classNameText = new TextBlock();
                    classNameText.Text = $"Holdnavn: {Classes[i].GetClassName()}";
                    TextBlock classActivityText = new TextBlock();
                    classActivityText.Text = $"Aktivitet: {Classes[i].GetActivity()}";
                    TextBlock ClassGenderText = new TextBlock();
                    ClassGenderText.Text = $"Tilladte Køn: {Classes[i].GetRequiredGender()}";
                    TextBlock ClassMinAge = new TextBlock();
                    ClassMinAge.Text = $"Minimum Alder: {Classes[i].GetRequiredMinAge()}";
                    TextBlock ClassMaxAge = new TextBlock();
                    ClassMaxAge.Text = $"Maksimum Alder: {Classes[i].GetRequiredMaxAge()}";


                    stackPanel.Children.Add(classNameText);
                    stackPanel.Children.Add(classActivityText);
                    stackPanel.Children.Add(ClassGenderText);
                    stackPanel.Children.Add(ClassMinAge);
                    stackPanel.Children.Add(ClassMaxAge);

                    ClassesList.Items.Add(item);
                }

            }
        }
        private void Tilmeld_Click(object sender, RoutedEventArgs e)
        {

            List<Class> Teams = member.LoadTeams();
            Class team = new Class();
            int classID = 1;

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
                    //messagebox tingeling
                    return;
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


        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
