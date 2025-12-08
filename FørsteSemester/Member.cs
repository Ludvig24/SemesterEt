using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FørsteSemester
{
    class Member : User
    {
        //Opretter sti til mappen "Documents" også kombinere det med stien ind til Members.txt filen og Classes.txt
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt");
        static string classesFilepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt");

        private string joinedClass = "";
        private List<int> joinedClasses = new List<int>();

        //metode som tilføjer et holds ID til brugeren i Members filen
        public void JoinClass(int classID, int userID)
        {
            joinedClasses.Add(classID); //Tilføjer HoldId til listen joinedClasses
            List<string> userIDs = UserManager.GetUserData(7); //henter alle UserIDs med GetUserData() metoden
            string[] lines = System.IO.File.ReadAllLines(filepath); //Læser alle linjer stien filepath peger på (members.txt)
            classID.ToString(); //konverterer classID til en string
            joinedClass = ";" + classID; //sætter joinedClass lig semikolon + classID for at matche formattet i tekstfilen Members.txt
            //bruger en StreamWriter til at skrive memberens opdaterede oplysninger ned på en linje i den text fil filepath peger på
            using (StreamWriter streamWriter = new StreamWriter(filepath)) //har ikke overloadet med true - medfører at append er false - vi overskriver dermed filen
            {
                int lineNumber = 0; //opretter variabel der holder styr på linjenummer i tekstfilen

                //for loop der itererer gennem userIDs listen
                for (int i = 0; i < userIDs.Count; i++)
                {
                    //if statement der tjekker om userID fra input er det samme som userIDs på index i
                    if (userID.ToString() == userIDs[i])
                    {
                        lineNumber = i; //hvis if statement udsagn er sandt tildeles linenumber i
                    }
                }

                lines[lineNumber] = lines[lineNumber] + joinedClass;  //vi appender joinedclass variablen på pladsen "linenumber" i arrayet lines

                //for loop der kører så længe i er mindre end længden på lines
                for (int i = 0; i < lines.Length; i++)
                {
                    streamWriter.Write(lines[i]); // for hver iteration kaldes Write på streamWriter objektet. lines på index i skrives ned i tekstfilen Members.txtx
                    streamWriter.WriteLine(); //kalder writeLine efter hver linje er skrevet
                }

            }
        }

        //Metoden LeaveClass, som fjerner et medlem fra et specifikt hold
        //Metoden fjerner et UserID fra Classes.txt og et ClassID fra Members.txt
        public void LeaveClass(int classID)
        {
            //Vi fjerner et classID fra listen joinedClasses
            joinedClasses.Remove(classID);

            string[] lines = System.IO.File.ReadAllLines(classesFilepath);  //Læser alle linjer stien classesFilepath peger på
            string[] memberLines = System.IO.File.ReadAllLines(filepath); //Læser alle linjer stien filepath peger på (members.txt)

            //foreach loop der iterer gennem arrayet lines
            int x = 0; //tællervariabel der anvendes længere nede
            foreach (string line in lines)
            {
                //opretter array der indeholder dataet fra en bestemt linje i Classes.txt
                string[] lineArray = line.Split(";"); //for hver iteration kaldes Split() på ";" på den string loopet er nået til og resultatet gemmes i et string array

                //if statement der tjekker om plads 8 i lineArray (klassens id) er lig med classID variablen fra input
                if (lineArray[8] == classID.ToString())
                {
                    //hvis if statement er sandt køres et for loop der itererer gennem arrayet lineArray fra plads 9 da det er derfra og frem hvor userIDs i classes er gemt
                    for (int i = 9; i < lineArray.Count(); i++)
                    {
                        //if statement tjekker for hver iteration om memberens id står i arrayet lineArray
                        if (lineArray[i] == GetUserID().ToString())
                        {
                            int currentJoinedAmount = Convert.ToInt32(lineArray[4]);//Hvis if statement er sandt gemmes antallet af tilmeldte brugere som står på index 4 i linearray i en int variabel
                            currentJoinedAmount--; //vi trækker 1 fra currentJoinedAmount
                            lineArray[4] = currentJoinedAmount.ToString(); // tildeler den nye værdi af currentJoinedAmount på plads 4 i arrayet lineArray

                            List<string> lineList = new List<string>(lineArray); //omkonverterer lineArray til en liste så vi kan bruge RemoveAt metoden
                            lineList.RemoveAt(i); // vi kalder RemoveAt() for at fjerne elementet på plads "i" i lineList
                            string[] newLine = lineList.ToArray(); //vi konverterer lineList tilbage til et nyt array kaldet newLine med ToArray() metoden
                            lines[x] = string.Join(";", newLine);//kalder join på newLine og sender ";" med som parameter. Dette sammenskriver hver elememt i newLine sammen til en string hvor hvert element er separeret med et semikolon. Resultatet gemmes i lines arrayet på plads x (den iteration i foreach loopet vi er nået til)


                        }
                    }
                }
                x++; //tæller x op med 1

            }



            //bruger en StreamWriter til at skrive memberens opdaterede oplysninger ned på en linje i den text fil classesFilepath peger på
            using (StreamWriter streamWriter = new StreamWriter(classesFilepath)) //har ikke overloadet med true - medfører at append er false - vi overskriver dermed filen
            {
                //for loop der itererer gennem lines arrayet
                for (int i = 0; i < lines.Length; i++)
                {
                    //for hver iteration skrives den string på index "i" ned i txt filen Classes
                    streamWriter.Write(lines[i]);
                    streamWriter.WriteLine();

                }
            }

            int z = 0;
            int linenumber = 0;
            List<string> userIDs = UserManager.GetUserData(7);
            while (z < userIDs.Count())
            {
                if (userIDs[z] == GetUserID().ToString())
                {
                    linenumber = z;
                }

                z++;
            }

            string member = memberLines[linenumber];
            string[] memberSplit = member.Split(";");

            int y = 8;
            while (y < memberSplit.Count())
            {
                if (memberSplit[y] == classID.ToString())
                {
                    List<string> memberList = new List<string>(memberSplit);
                    memberList.RemoveAt(y);
                    string[] newMemberLine = memberList.ToArray();
                    memberLines[linenumber] = string.Join(";", newMemberLine);
                }

                y++;
            }

            using (StreamWriter streamWriter = new StreamWriter(filepath))
            {
                for (int i = 0; i < memberLines.Length; i++)
                {
                    streamWriter.Write(memberLines[i]);
                    streamWriter.WriteLine();
                }
            }
        }

        //Metoden DeleteProfile, 
        public void DeleteProfile()
        {

        }

        public void ViewStatus()
        {

        }

        public List<int> GetJoinedClasses()
        {
            return joinedClasses;
        }
        public void SetJoinedClasses(List<int> joinedClasses)
        {
            this.joinedClasses = joinedClasses;
        }


    }
}

