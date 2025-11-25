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
            InitializeComponent();
            UserManager.LoadMember();
            UserManager.GetUserData(5);
            UserManager.GetUserData(3);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            CreateMember createMemberWindow = new CreateMember(this);
            createMemberWindow.Show();
            
            this.Hide();
        }

        private void Loggin_Click(object sender, RoutedEventArgs e)
        {
            Mainmenu createMainmenu = new Mainmenu();
            createMainmenu.Show();
            this.Hide();
        }
    }
}