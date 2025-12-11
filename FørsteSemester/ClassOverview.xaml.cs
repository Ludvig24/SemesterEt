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
        //Her opretter vi to objekter som kan bruges i hele klassen
        Window window;
        Member member;
        internal ClassOverview(Window window, Member member)
        {
            //Her opretter vi komponenterne i vinduet. og så gemmer vi window og member som vi fik fra tidligere vindue, til window og member der er i dettevindue
            this.window = window;
            this.member = member;
            InitializeComponent();

            //Opretter en liste der skal indeholde de hold som memberen er tilmeldt
            List<Class> membersClasses = new List<Class>();
            //Henter alle hold fra systemet
            List<Class> Classes = member.LoadTeams();

            //foreach loop der for hvert objekt i listen Classes tjekker om memberens liste af classID'er indeholder classID'et for den class vi er nået til i loopet
            foreach (Class Class in Classes)
            {
                //Hvis memberen er tilmeldt holdet, tilføjes holdet til membersClasses listen
                if (member.GetJoinedClasses().Contains(Class.GetClassID()))
                {
                    membersClasses.Add(Class);
                }
            }   

            //loop der opretter en ListBoxItem for hver class memberen er tilmeldt.
            for (int i = 0; i < membersClasses.Count; i++)
            {
                //Nedenfor konverteres char til string for at kunne vises i listen og for at det er mere
                //brugervenligt at se hele ordet for kønnet end kun engelsk forbukstav
                char GetRequiredGenderInChar = Classes[i].GetRequiredGender(); //Henter kønnet for det pågældende hold
                string Køn = ""; //Tom string variabel til at holde kønnet som tekst
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

                ListBoxItem item = new ListBoxItem();//Opretter en ListBoxItem
                StackPanel stackPanel = new StackPanel();//Opretter en instans af StackPanel som vi bruger til at stable en række tekstfelter i WPF vinduet
                item.Content = stackPanel;//Tildeler stackPanel til vores ListBoxItem
                TextBlock classNameText = new TextBlock();//Opretter en instans af TextBlock
                classNameText.Text = $"Holdnavn: {membersClasses[i].GetClassName()}";
                TextBlock classActivityText = new TextBlock();
                classActivityText.Text = $"Aktivitet: {membersClasses[i].GetActivity()}";
                TextBlock antalPladser = new TextBlock();
                antalPladser.Text = $"Antal pladser: {membersClasses[i].GetAvailableSpots()}";
                TextBlock ledigePladser = new TextBlock();
                ledigePladser.Text = $"Ledige pladser: {membersClasses[i].GetAvailableSpots() - membersClasses[i].GetJoinedAmount()}";
                //Gemmer classID'et i Tag egenskaben af ListBoxItem, så vi kan bruge det senere når vi skal framelde medlemmet
                item.Tag = membersClasses[i].GetClassID();

                //Tilføjer alle TextBlocke som Children af vores stackPanel hvilket medfører at de bliver stablet i WPF
                stackPanel.Children.Add(classNameText);
                stackPanel.Children.Add(classActivityText);
                stackPanel.Children.Add(ledigePladser);
                //Tilføjer ListBoxItem til ListBoxen i WPF vinduet
                ClassesListBox.Items.Add(item);
            }
        }
        private void TilbageKnap_Click(object sender, RoutedEventArgs e)
        {
            //Går tilbage til det tidligere vindue når "TilbageKnap" bliver klikket
            this.Hide();
            //Viser det tidligere vindue igen
            window.Show();
        }

        private void FrameldHold_Click(object sender, RoutedEventArgs e)
        {
            // if statement der tjekker om brugeren har klikket på listboxen
            if (ClassesListBox.SelectedItem == null)
            {
                MessageBox.Show("Vælg et hold først.", "Ingen markering");
                return;
            }

            // Henter det valgte ListBoxItem og dets classID fra Tag egenskaben
            ListBoxItem selectedItem = (ListBoxItem)ClassesListBox.SelectedItem;//Den forstår ikke hvad den er, så minder den lige om den er en ListBoxItem
            int classID = Convert.ToInt32(selectedItem.Tag);

            // Henter alle hold fra systemet
            List<Class> Teams = member.LoadTeams();
            // Frameld medlemmet fra det valgte hold
            member.LeaveClass(classID);

            // Opdater joinedAmount for det pågældende hold i Teams listen
            for (int i = 0; i < Teams.Count; i++)
            {
                // Tjekker om det nuværende holds classID matcher det valgte classID
                if (classID == Teams[i].GetClassID())
                {
                    // Sænker joinedAmount med 1
                    Teams[i].SetJoinedAmount(Teams[i].GetJoinedAmount() - 1); //Den henter det nuværende joinedAmount og sætter det til det nuværende -1
                }
            }

            // Fjerner det valgte hold fra ListBoxen i WPF vinduet
            ClassesListBox.Items.RemoveAt(ClassesListBox.SelectedIndex);
        }
    }
}
