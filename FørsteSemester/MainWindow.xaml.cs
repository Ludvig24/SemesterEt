using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FørsteSemester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Kommand der initialiserer komponenterne i vinduet
            InitializeComponent();
        }

        //Metode til at åbne CreateMember vinduet
        private void CreateMember_Click(object sender, RoutedEventArgs e)
        {
            //Opretter et CreateMember vindue og sender en instands af MainWindow som parameter
            CreateMember createMemberWindow = new CreateMember(this);
            createMemberWindow.Show();
            //Lukker MainWindow vinduet
            this.Hide();
        }

        //Metode til at logge ind som enten medlem eller admin
        private void Loggin_Click(object sender, RoutedEventArgs e)
        {
            Member loginMember = UserManager.Login(BrugernavnBox.Text.ToLower(), PasswordBox.Text); //Checker om der er et medlem med det brugernavn og password
            Admin loginAdmin = UserManager.AdminLogin(BrugernavnBox.Text.ToLower(), PasswordBox.Text); //Checker om der er en admin med det brugernavn og password

            if (loginAdmin != null) //Hvis at loginAdmin ikke er null køres admin login
            {
                Adminmenu createAdminmenu = new Adminmenu(loginAdmin);
                createAdminmenu.Show();
                this.Hide();
            }

            else if (loginMember != null) //Hvis loginMember ikke er null køres member login
            {
                Mainmenu createMainmenu = new Mainmenu(this, loginMember); //Opretter et Mainmenu vindue og sender MainWindow vinduet og Member objektet som parametre
                BrugernavnBox.Clear();//Rydder brugernavn og password felterne så man kan gåtilbage til login vinduet uden at de tidligere værdier står der
                PasswordBox.Clear();
                createMainmenu.Show();
                this.Hide();
            }
            else //Ellers vises fejllogin beskeden
            {
                Fejllogin.Visibility = Visibility.Visible;

            }
           


            
        }
    }
}