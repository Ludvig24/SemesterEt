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
    /// Interaction logic for MemberOverView.xaml
    /// </summary>
    public partial class MemberOverView : Window
    {
        Admin admin;
        Window window;
        
        public MemberOverView()
        {
            InitializeComponent();
        }
        internal MemberOverView(Window window, Admin admin)
        {
            InitializeComponent();
            this.window = window;
            this.admin = admin;
            List<Member> members = UserManager.LoadMember(); //opretter liste af members
            for (int i = 0; i < members.Count; i++) //loop der går igennem listen af members
            {
                MembersListBox.Items.Add("Brugernavn: " + members[i].GetUserName() + ", Navn: " + members[i].GetName() + " " + members[i].GetSurname() + ", Alder: " + members[i].GetAge() + ", Køn: " + members[i].GetGender() + ", By: " + members[i].GetCity()); //udskriver medlem i listbox
                MembersListBox.Items.Add("-----------------------------------------------------" );

            }
        }

        //Går tilbage til Adminmenu vinduet når "TilbageKnap" bliver klikket
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Adminmenu adminmenu = new Adminmenu(admin);
            adminmenu.Show();
        }
    }
}
