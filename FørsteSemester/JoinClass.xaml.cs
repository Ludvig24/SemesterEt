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

            //Ludvigs- for at tilføje til listen?
            // ClassesList.Items.Add()
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
