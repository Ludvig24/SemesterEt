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
    /// Interaction logic for Mainmenu.xaml
    /// </summary>
    public partial class Mainmenu : Window
    {
        //Her opretter vi to objekter som kan bruges i hele klassen
        Window window;
        Member member;

        internal Mainmenu(Window window, Member member) //Constructor til Mainmenu som tager et Window objekt og et Member objekt som parametre
        {
            InitializeComponent();
            this.window = window; //Tildeler window objekt fra constructor til det window attribut i klassen
            this.member = member;//Tildeler member objekt fra constructor til den member attribut i klassen

        }

        private void Logud_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); //lukker Mainmenu vinduet
            window.Show(); //viser login vinduet
        }

        private void Tilmeld_Click(object sender, RoutedEventArgs e) 
        {
            JoinClass joinClass = new JoinClass(this, member); //Opretter et JoinClass vindue og sender Mainmenu vinduet og Member objektet som parametre
            joinClass.Show(); //viser JoinClass vinduet
            this.Hide(); //gemmer Mainmenu vinduet


        }

        private void MineHold_Click(object sender, RoutedEventArgs e)
        {
            ClassOverview classOverview = new ClassOverview(this, member); //Opretter et ClassOverview vindue og sender Mainmenu vinduet og Member objektet som parametre
            classOverview.Show(); //viser ClassOverview vinduet

            this.Hide(); //gemmer Mainmenu vinduet
        }
    }
    
    
}
