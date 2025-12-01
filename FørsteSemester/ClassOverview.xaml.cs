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
    /// Interaction logic for ClassOverview.xaml
    /// </summary>
    public partial class ClassOverview : Window
    {

        Window window;
        Member member;
        internal ClassOverview(Window window, Member member)
        {
            this.window = window;
            this.member = member;
            InitializeComponent();
        }

    }
}
