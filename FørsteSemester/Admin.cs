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
        static string classesFilepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt"); //Kombinerer stien til mappen "Documents" med stien til Classes.txt filen
        
        //Metoden CreateClass, hvor vi opretter et hold med følgende parametre, med de forskellige attributter et hold skal have
        public void CreateClass(string activity, string classname, int availableSpots, char requiredGender, byte requiredMaxAge, byte requiredMinAge)
        {
            //Sætter attributterne et hold skal have
            Class Team = new Class();
            Team.SetActivity(activity);
            Team.SetClassName(classname);
            Team.SetStatus(false); // Sætter status til false, så holdet som standart ikke er fyldt. Status er nemlig om holdet er fyldt
            Team.SetAvailableSpots(availableSpots);
            Team.SetJoinedAmount(0); // Sætter joinedAmount til 0, da der ikke er nogen medlemmer på holdet ved oprettelse
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
            string[] lines = System.IO.File.ReadAllLines(classesFilepath); //Henter data fra tekstfilen i et array
            for (int i = 0; i < lines.Count(); i++) //Looper igennem arrayet lines og splitter ved hver ;
            {
                string ClassData = lines[i]; //Henter en linje fra tekstfilen
                string[] ClassSplit = ClassData.Split(";"); //Splitter linjen ved ; og gemmer det i et array
                dataPoint = ClassSplit[a]; //Henter den ønskede data baseret på det givne indeks a
                data.Add(dataPoint); //Tilføjer den ønskede data til listen
            }

            return data; //Returnerer listen med den ønskede data
        }

        //Metoden SaveClass, som gemmer et hold og tiføjer hold objekt til en tekstfil
        public void SaveClass(Class team)
        {
            //Bruger en StreamWriter til at skrive Class oplysninger ned på en linje i den text fil membersFilepath peger på
            //using indebære at den skal åbne og lukke filen korrekt efter brug, så man kan tilgå filen igen senere. Filen kan ikke tilgås flere steder samtidig så længe streamWriter er åben
            using (StreamWriter streamWriter = new StreamWriter(classesFilepath, true))//Filepath er stien til den fil vi vil skrive i, og vi skriver true for at sige at den skal append(Tilføjer, ikke overskriver) hver gang der skrives frem for at overskrive
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
                
                if(team.GetMemberIDsInClass() != null) //Tjekker om der er nogle memberID'er i team objektet - hvis ikke skrives der ingenting i filen
                {
                    streamWriter.Write(team.GetMemberIDsInClass() + ";"); //Hvis GetMemberIDsInClass() returnerer noget skrives de ned i filen efterfulgt af et semikolon
                }
                
                streamWriter.WriteLine();
            }
        }
    }
}
