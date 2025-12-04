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
        string UsynligClassID;
        internal ClassOverview(Window window, Member member)
        {
            this.window = window;
            this.member = member;
            InitializeComponent();

            //Her har jeg koppieret koden fra JoinClass for at vise de hold som medlemmet er tilmeldt

            List<Class> membersClasses = new List<Class>();
            List<Class> Classes = member.LoadTeams();
            foreach (Class cls in Classes)
            {
                if (member.GetJoinedClasses().Contains(cls.GetClassID()))
                {
                    membersClasses.Add(cls);
                }
            }   
            for (int i = 0; i < membersClasses.Count; i++)
            {
                //Nedenfor konverteres char til string for at kunne vises i listen og for at det er mere
                //brugervenligt at se hele ordet for kønnet end kun engelsk forbukstav
                char GetRequiredGenderInChar = Classes[i].GetRequiredGender();
                string Køn = "";
                if (GetRequiredGenderInChar == 'F')
                {
                    Køn = "Kvinde";
                }
                else if (GetRequiredGenderInChar == 'M')
                {
                    Køn = "Mand";
                }
                if (GetRequiredGenderInChar == 'B')
                {
                    Køn = "Begge køn";
                }

                ListBoxItem item = new ListBoxItem();
                //item.Content = $"{Classes[i].GetClassName()} - {Classes[i].GetActivity()} - Ledige pladser: {Classes[i].GetAvailableSpots()}";
                StackPanel stackPanel = new StackPanel();
                item.Content = stackPanel;
                TextBlock classNameText = new TextBlock();
                classNameText.Text = $"Holdnavn: {membersClasses[i].GetClassName()}";
                TextBlock classActivityText = new TextBlock();
                classActivityText.Text = $"Aktivitet: {membersClasses[i].GetActivity()}";
                TextBlock antalPladser = new TextBlock();
                antalPladser.Text = $"Antal pladser: {membersClasses[i].GetAvailableSpots()}";
                TextBlock ledigePladser = new TextBlock();
                ledigePladser.Text = $"Ledige pladser: {membersClasses[i].GetAvailableSpots() - membersClasses[i].GetJoinedAmount()}";
                // store the class ID on the ListBoxItem.Tag (preferred) so we can read it later from the selected item
                item.Tag = membersClasses[i].GetClassID();


               

                stackPanel.Children.Add(classNameText);
                stackPanel.Children.Add(classActivityText);
                stackPanel.Children.Add(ledigePladser);

                ClassesListBox.Items.Add(item);




            }
        }
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            window.Show();
        }

        private void FrameldHold_Click(object sender, RoutedEventArgs e)
        {
            // Ensure an item is selected
            if (ClassesListBox.SelectedItem == null)
            {
                MessageBox.Show("Vælg et hold først.", "Ingen markering", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Get selected ListBoxItem and read the class ID from its Tag
            ListBoxItem selectedItem = (ListBoxItem)ClassesListBox.SelectedItem;
            int classID = Convert.ToInt32(selectedItem.Tag);

            // Do the leave operation using the selected class ID
            List<Class> Teams = member.LoadTeams();
            member.LeaveClass(classID);

            // Update joined counts in memory (if needed)
            for (int i = 0; i < Teams.Count; i++)
            {
                if (classID == Teams[i].GetClassID())
                {
                    Teams[i].SetJoinedAmount(Teams[i].GetJoinedAmount() - 1);
                }
            }

            // Refresh the list (simple approach: clear; you may want to repopulate)
            ClassesListBox.Items.RemoveAt(ClassesListBox.SelectedIndex);
        }
    }
}
/*
    ListBoxItem item = new ListBoxItem();
    //item.Content = $"{Classes[i].GetClassName()} - {Classes[i].GetActivity()} - Ledige pladser: {Classes[i].GetAvailableSpots()}";
    StackPanel stackPanel = new StackPanel();
    item.Content = stackPanel;
    TextBlock classNameText = new TextBlock();
    classNameText.Text = $"Holdnavn: {membersClasses[i].GetClassName()}";
    TextBlock classActivityText = new TextBlock();
    classActivityText.Text = $"Aktivitet: {membersClasses[i].GetActivity()}";
    TextBlock antalPladser = new TextBlock();
    antalPladser.Text = $"Antal pladser: {membersClasses[i].GetAvailableSpots()}";
    TextBlock ledigePladser = new TextBlock();
    ledigePladser.Text = $"Ledige pladser: {membersClasses[i].GetAvailableSpots() - membersClasses[i].GetJoinedAmount()}";
    TextBlock UsynligID = new TextBlock();
    UsynligID.Text = $"{membersClasses[i].GetClassID()}";
    item.SetValue(TagProperty, membersClasses[i].GetClassID());


UsynligClassID = UsynligID.Text;

stackPanel.Children.Add(classNameText);
    stackPanel.Children.Add(classActivityText);
    stackPanel.Children.Add(ledigePladser);

    ClassesListBox.Items.Add(item);




}
}
private void TilbageKnap_Click(object sender, RoutedEventArgs e)
{
this.Hide();
window.Show();
}

private void FrameldHold_Click(object sender, RoutedEventArgs e)
{

List<Class> Teams = member.LoadTeams();
int classID = Convert.ToInt32(UsynligClassID);

member.LeaveClass(classID);
ListBoxItem selectedItem = (ListBoxItem)ClassesListBox.SelectedItem;
StackPanel selectedStackPanel = (StackPanel)selectedItem.Content;


int i = 0;
while (i < Teams.Count)
{
if (classID == Teams[i].GetClassID())
{
    Teams[i].SetJoinedAmount(Teams[i].GetJoinedAmount() - 1);
}
i++;
}
ClassesListBox.Items.Clear();

}
}
}
*/