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
    internal class Admin : User
    {
        //Statiske variabler til at finde stien til tekstfilen, hvor hold data bliver gemt. Kombinerer stien til mappen "Documents" med stien til Classes.txt
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Finder stien til mappen "Documents" som er ens for alle computere
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt"); //Kombinerer stien til mappen "Documents" med stien til Classes.txt filen
        
        //Metoden CreateClass, hvor vi opretter et hold med følgende parametre, med de forskellige attributter et hold skal have
        public void CreateClass(string activity, string classname, int availableSpots, char requiredGender, byte requiredMaxAge, byte requiredMinAge)
        {
            //Setter attributterne et hold skal have
            Class Team = new Class();
            Team.SetActivity(activity);
            Team.SetClassName(classname);
            Team.SetStatus(false);
            Team.SetAvailableSpots(availableSpots);
            Team.SetJoinedAmount(0);
            Team.SetRequiredGender(requiredGender);
            Team.SetRequiredMaxAge(requiredMaxAge);
            Team.SetRequiredMinAge(requiredMinAge);
            
            //Genererer et unikt hold ID ved at tjekke, hvor mange hold der allerede er oprettet og sikre, at det nye ID ikke allerede er i brug
            int TeamID = LoadTeams().Count + 1;//Sætter TeamID til at være lig med antallet af hold, der allerede er oprettet + 1
            while (true)
            {
                if (GetClassData(8).Contains(TeamID.ToString())) //Tjekker om det genererede TeamID allerede findes i listen af hold ID'er
                {
                    TeamID = TeamID + 1; //Hvis det gør, øges TeamID med 1 og tjekket gentages
                }
                else
                {
                    Team.SetClassID(TeamID); //Hvis det ikke gør, sættes holdets ID til det genererede TeamID
                    break; //Bryder while loopet
                }

            }
            SaveClass(Team); //Gemmer det nye hold i tekstfilen

        }
        
        //Metode til at få en en specifik type date fra alle hold i en liste baseret på deres indeks (int a)
        public static List<string> GetClassData(int a) 
        {
            List<string> data = new List<string>(); //Opretter en liste til at gemme den ønskede data
            string dataPoint = ""; //Tom string variabel til at holde den ønskede data midlertidigt
            string[] lines = System.IO.File.ReadAllLines(filepath); //Henter data fra tekstfilen i et array
            for (int i = 0; i < lines.Count(); i++) //Looper igennem arrayet lines og splitter ved hver ;
            {
                string ClassData = lines[i]; 
                string[] ClassSplit = ClassData.Split(";");
                dataPoint = ClassSplit[a];
                data.Add(dataPoint);
            }

            return data;
        }

        //Metoden SaveClass, som gemmer et hold og tiføjer hold objekt til en tekstfil
        public void SaveClass(Class team)
        {
            using (StreamWriter streamWriter = new StreamWriter(filepath, true))
            {
                //Skriver attriutterne ned og skiller ved ;
                streamWriter.Write(team.GetActivity()+ ";");
                streamWriter.Write(team.GetClassName()+";");
                streamWriter.Write(team.GetStatus()+";");
                streamWriter.Write(team.GetAvailableSpots()+";");
                streamWriter.Write(team.GetJoinedAmount()+";");
                streamWriter.Write(team.GetRequiredGender()+";");
                streamWriter.Write(team.GetRequiredMaxAge()+";");
                streamWriter.Write(team.GetRequiredMinAge()+";");
                streamWriter.Write(team.GetClassID());
                
                if(team.GetMemberIDsInClass() != null) //tjekker om der er nogle memberID'er i team objektet - hvis ikke skrives der ingenting i filen
                {
                    streamWriter.Write(team.GetMemberIDsInClass() + ";"); //hvis GetMemberIDsInClass() returnerer noget skrives de ned i filen efterfulgt af et semikolon
                }
                
                streamWriter.WriteLine();
            }
        }
    }
}
