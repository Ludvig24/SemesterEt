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
    /// Interaction logic for Adminmenu.xaml
    /// </summary>
    public partial class Adminmenu : Window
    {
        //Her opretter vi et admin objekt som kan bruges i hele klassen
        Admin admin;
        //Konstruktor for Adminmenu klassen, som tager et Admin objekt som parameter
        internal Adminmenu(Admin admin)
        {
            //Her opretter vi komponenterne i vinduet. og så gemmer vi admin som vi fik fra tidligere vindue til admin der er i dettevindue
            InitializeComponent();
            this.admin = admin;
        }

        //Her opretter vi et nyt vindue, lukker det gamle vindue ned og viser det nye der lige er blevet oprettet
        private void OpretHold_Click(object sender, RoutedEventArgs e)
        {
            CreateClass createclass = new CreateClass(admin);
            createclass.Show();
            this.Hide();
        }

        //Her opretter vi et nyt vindue, lukker det gamle vindue ned og viser det nye der lige er blevet oprettet
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        //Her opretter vi et nyt vindue, lukker det gamle vindue ned og viser det nye der lige er blevet oprettet
        private void OversightOverHold_Click(object sender, RoutedEventArgs e)
        {
            AdminClassOverview adminClassOverview = new AdminClassOverview(this, admin);
            adminClassOverview.Show();
            this.Hide();
        }

        //Her opretter vi et nyt vindue, lukker det gamle vindue ned og viser det nye der lige er blevet oprettet
        private void Medlemsoversigt_Click(object sender, RoutedEventArgs e)
        {
           
            MemberOverView memberOverView = new MemberOverView(this, admin);
            memberOverView.Show();
            this.Hide();
        }
    }
    
}
