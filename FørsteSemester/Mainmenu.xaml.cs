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
        Window window;
        Member member;
        JoinClass joinClass;

        internal Mainmenu(Window window, Member member)
        {
            InitializeComponent();
            this.window = window;
            this.member = member;
            JoinClass joinClass = new JoinClass(this, member);
            this.joinClass = joinClass;
            joinClass.Hide();
        }

        private void Logud_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            window.Show();
        }

        private void Tilmeld_Click(object sender, RoutedEventArgs e) 
        {      
           joinClass.Show();

            //this.Hide();
        }

        private void MineHold_Click(object sender, RoutedEventArgs e)
        {
            ClassOverview classOverview = new ClassOverview(this, member);
            classOverview.Show();

                this.Hide();
        }
    }
    
    
}
