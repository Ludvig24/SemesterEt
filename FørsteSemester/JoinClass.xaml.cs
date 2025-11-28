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
        }

        private void Tilmeld_Click(object sender, RoutedEventArgs e)
        {
            int classID = 1;
            member.JoinClass(classID, member.GetUserID());
            List<Class> Teams = member.LoadTeams();

            int i = 0;
            while (i < Teams.Count)
            {
                if (classID == Teams[i].GetClassID())
                {
                    Teams[i].AddMemberIDToClass(member.GetUserID(), classID);

                }
                i++;
            }


        }
    }
}
