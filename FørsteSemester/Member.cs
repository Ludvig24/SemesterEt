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
        //Statiske variabler til at finde stien til tekstfilen, hvor hold data og Medlem data bliver gemt. Kombinere mappen "Documents" med stien til Classes.txt eller Members.txt
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Finder stien til mappen "Documents" som er ens for alle computere
        static string membersFilepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt"); // Kombinerer stien til mappen "Documents" med stien til Members.txt filen
        static string classesFilepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt"); // Kombinerer stien til mappen "Documents" med stien til Classes.txt filen

        //Attributter for klassen Member
        private string joinedClass = "";
        private List<int> joinedClasses = new List<int>();

        //Metode JoinClass, som tilføjer et holds ID til brugeren i Members filen
        public void JoinClass(int classID, int userID)
        {
            joinedClasses.Add(classID); //Tilføjer HoldId til listen joinedClasses
            List<string> userIDs = UserManager.GetUserData(7); //Henter alle UserIDs med GetUserData() metoden
            string[] lines = System.IO.File.ReadAllLines(membersFilepath); //Læser alle linjer stien membersFilepath peger på (members.txt)
            classID.ToString(); //Konverterer classID til en string
            joinedClass = ";" + classID; //Sætter joinedClass lig semikolon + classID for at matche formattet i tekstfilen Members.txt
            //Bruger en StreamWriter til at skrive memberens opdaterede oplysninger ned på en linje i den text fil membersFilepath peger på
            using (StreamWriter streamWriter = new StreamWriter(membersFilepath)) //Vi åbner en StreamWriter til at skrive i filen Classes.txt. Vi skriver ikke "true" som andet parameter, fordi vi vil overskrive filen med de opdaterede data
            {
                int lineNumber = 0; //Opretter variabel, der holder styr på linjenummer i tekstfilen

                //for loop, der itererer gennem userIDs listen
                for (int i = 0; i < userIDs.Count; i++)
                {
                    //if statement, der tjekker om userID fra input er det samme som userIDs på index i
                    if (userID.ToString() == userIDs[i])
                    {
                        lineNumber = i; //Hvis if statement udsagn er sandt tildeles linenumber i
                    }
                }

                lines[lineNumber] = lines[lineNumber] + joinedClass;  //Vi appender joinedclass variablen på pladsen "linenumber" i arrayet lines

                //for loop der kører så længe i er mindre end længden på lines
                for (int i = 0; i < lines.Length; i++)
                {
                    //Smider det vi lige har sat sammen ind i vores textfil
                    streamWriter.Write(lines[i]); // For hver iteration kaldes Write på streamWriter objektet. Lines på index i skrives ned i tekstfilen Members.txt
                    streamWriter.WriteLine(); //Kalder writeLine efter hver linje er skrevet
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
            string[] memberLines = System.IO.File.ReadAllLines(membersFilepath); //Læser alle linjer stien membersFilepath peger på (members.txt)

            //foreach loop der iterer gennem arrayet lines
            int x = 0; //Tællervariabel der anvendes længere nede
            foreach (string line in lines)
            {
                //Opretter array der indeholder dataet fra en bestemt linje i Classes.txt
                string[] lineArrayClass = line.Split(";"); //For hver iteration kaldes Split() på ";" på den string loopet er nået til og resultatet gemmes i et string array

                //if statement der tjekker om plads 8 i lineArrayClass (klassens id) er lig med classID variablen fra input
                if (lineArrayClass[8] == classID.ToString())
                {
                    //Hvis if statement er sandt køres et for loop der itererer gennem arrayet lineArrayClass fra plads 9, da det er derfra og frem, hvor userIDs i classes er gemt
                    for (int i = 9; i < lineArrayClass.Count(); i++)
                    {
                        //if statement tjekker for hver iteration om memberens id står i arrayet lineArrayClass
                        if (lineArrayClass[i] == GetUserID().ToString())
                        {
                            int currentJoinedAmount = Convert.ToInt32(lineArrayClass[4]);//Hvis if statement er sandt gemmes antallet af tilmeldte brugere, som står på index 4 i linearray i en int variabel
                            currentJoinedAmount--; //Vi trækker 1 fra currentJoinedAmount
                            lineArrayClass[4] = currentJoinedAmount.ToString(); //Tildeler den nye værdi af currentJoinedAmount på plads 4 i arrayet lineArrayClass

                            List<string> lineList = new List<string>(lineArrayClass); //Omkonverterer lineArrayClass til en liste så vi kan bruge RemoveAt metoden
                            lineList.RemoveAt(i); // Vi kalder RemoveAt() for at fjerne elementet på plads "i" i lineList
                            lines[x] = string.Join(";", lineList);//Kalder join på lineList og sender ";" med som parameter.
                                                                  //Dette sammenskriver hver elememt i lineList sammen til en string, hvor hvert element er separeret med et semikolon.
                                                                  //Resultatet gemmes i lines arrayet på plads x (den iteration i foreach loopet vi er nået til)
                        }
                    }
                }
                x++; //Tæller x op med 1
            }

            //Bruger en StreamWriter til at skrive klassens opdaterede oplysninger ned på en linje i den text fil classesFilepath peger på
            using (StreamWriter streamWriter = new StreamWriter(classesFilepath)) //Har ikke overloadet med true - medfører at append er false - vi overskriver dermed filen
            {
                //for loop der itererer gennem lines arrayet
                for (int i = 0; i < lines.Length; i++)
                {
                    //for hver iteration skrives den string på index "i" ned i txt filen Classes
                    streamWriter.Write(lines[i]);
                    streamWriter.WriteLine();

                }
            }

            //Fjerner classID fra medlemmet i Members.txt filen
            int z = 0; //Tællervariabel for while loop
            int linenumber = 0; //Int variabel der holder styr på linjenummer i tekstfilen Members.txt
            List<string> userIDs = UserManager.GetUserData(7); //Gemmer alle userIDs fra textfilen
                                                               //Members.txt i en liste af strings
            //while loop der itererer gennem userIDs listen
            while (z < userIDs.Count())
            {
                //if statement der tjekker om userID'et for memberen er det samme som userIDs på index z
                if (userIDs[z] == GetUserID().ToString())
                {
                    linenumber = z; //Hvis if statement er sandt tildeles variablen linenumber z
                }

                z++;
            }

            string member = memberLines[linenumber]; //Opretter en string og tildeler den element
                                                     //på index "lineumber" i arrayet memberLines

            string[] memberSplit = member.Split(";"); //Splitter stringen member ved hvert semikolon
                                                      //så vi har et array kaldet memberSplit med alt memberens data

            //while loop der itererer fra index 8 i memberSplit arrayet. Index 8 og frem er nemlig hvor classID'erne for de hold medlemmet er tilmeldt er gemt
            int y = 8;
            while (y < memberSplit.Count())
            {
                //if statement der tjekker om index y i memberSplit er lig med classID fra input
                if (memberSplit[y] == classID.ToString())
                {
                    //Hvis if statement er sandt fjernes classID'et fra memberSplit arrayet
                    List<string> memberList = new List<string>(memberSplit); //Omkonverterer memberSplit til
                                                                             //en liste så vi kan bruge RemoveAt metoden
                    memberList.RemoveAt(y); //Kalder RemoveAt() på memberList for at fjerne elementet på index y
                    memberLines[linenumber] = string.Join(";", memberList); //Kalder join på memberList og sender ";" med som parameter.
                                                                            //Dette sammenskriver hver elememt i newMemberLine sammen til en string,
                                                                            //hvor hvert element er separeret med et semikolon.
                                                                            //Resultatet gemmes i MemberLines arrayet på plads linenumber
                }

                y++;
            }

            //Bruger en StreamWriter til at skrive memberens opdaterede oplysninger ned på en linje
            //i den text fil membersFilepath peger på (Members.txt)
            using (StreamWriter streamWriter = new StreamWriter(membersFilepath)) //Har ikke overloadet med true -
                                                                                  //medfører at append er false -
                                                                                  //vi overskriver dermed filen
            {
                //for loop der itererer gennem memberLines arrayet
                for (int i = 0; i < memberLines.Length; i++)
                {
                    //For hver iteration skrives den string på index "i" ned i txt filen Members
                    streamWriter.Write(memberLines[i]);
                    streamWriter.WriteLine();
                }
            }
        }


        //Get Set metoder 
        //Bruges til at hente og sætte atributterne i andre klasser
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

