using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FørsteSemester
{
    class Admin : User
    {
        //Opretter sti til dokumentmappen og til Classes.txt filen
        //Følgende kode del, bliv vi insprieret af fra AI.
        //AI fortalte her om Enviroment, så vi kun find frem til understående gennem Mircosoft.
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt");

        //Fields/Attribut for Klassen Admin
        List<Member> memberList = new List<Member>();

        public void SletMember()
        {

        }

        //metode til at oprette et hold med parametre for de forskellige attributter et hold har
        public void CreateClass(string activity, string classname, int availableSpots, char requiredGender, byte requiredMaxAge, byte requiredMinAge)
        {
            Class Team = new Class();
            Team.SetActivity(activity);
            Team.SetClassName(classname);
            Team.SetStatus(false);
            Team.SetAvailableSpots(availableSpots);
            Team.SetJoinedAmount(0);
            Team.SetRequiredGender(requiredGender);
            Team.SetRequiredMaxAge(requiredMaxAge);
            Team.SetRequiredMinAge(requiredMinAge);

            int TeamID = LoadTeams().Count + 1; //Tager mængden af hold i systemet og sætter det antal+1 og sætter det som ID for det nye hold
            while (true)
            {
                if (GetClassData(8).Contains(TeamID.ToString())) //ændret GetClassData() fra (9) til (8)
                {
                    TeamID = TeamID + 1;
                }
                else
                {
                    Team.SetClassID(TeamID);
                    break;
                }

            }
            SaveClass(Team); //Gemmer det nye hold i tekstfilen

        }
        
        //Metode til at få en en specifik type date fra alle hold i en liste baseret på deres indeks (int a)
        public static List<string> GetClassData(int a) 
        {
            List<string> data = new List<string>();
            string dataPoint = "";
            string[] lines = System.IO.File.ReadAllLines(filepath); //henter data fra tekstfilen i et array
            for (int i = 0; i < lines.Count(); i++) //looper igennem array og splitter ved hver ;
            {
                string ClassData = lines[i];
                string[] ClassSplit = ClassData.Split(";");
                dataPoint = ClassSplit[a];
                data.Add(dataPoint);
            }

            return data;

        }

        //metode til at putte et hold objekt ind i tekstfilen
        public void SaveClass(Class team)
        {
            using (StreamWriter streamWriter = new StreamWriter(filepath, true))
            {
                streamWriter.Write(team.GetActivity()+ ";");
                streamWriter.Write(team.GetClassName()+";");
                streamWriter.Write(team.GetStatus()+";");
                streamWriter.Write(team.GetAvailableSpots()+";");
                streamWriter.Write(team.GetJoinedAmount()+";");
                streamWriter.Write(team.GetRequiredGender()+";");
                streamWriter.Write(team.GetRequiredMaxAge()+";");
                streamWriter.Write(team.GetRequiredMinAge()+";");
                streamWriter.Write(team.GetClassID());
                if(team.GetMemberIDsInClass() != null)
                {
                    streamWriter.Write(team.GetMemberIDsInClass() + ";");
                }
                
                streamWriter.WriteLine();
            }
        }

        public void DeleteClass()
        {

        }

        public void RemoveMemberFromClass()
        {

        }

        public void RoomRecord()
        {

        }






    }
}
