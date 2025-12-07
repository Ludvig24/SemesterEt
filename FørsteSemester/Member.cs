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
            string[] lines = System.IO.File.ReadAllLines(filepath); //Læser alle linjer stien filepath peger på
            classID.ToString(); //konverterer classID til en string
            joinedClass = ";"+ classID; //sætter joinedClass lig semikolon + classID for at matche formattet i tekstfilen Members.txt
            //bruger en StreamWriter til at skrive memberens opdaterede oplysninger ned på en linje i den text fil filepath peger på
            using (StreamWriter streamWriter = new StreamWriter(filepath)) //har ikke overloadet med true - medfører at append er false - vi overskriver dermed filen
            {
                int lineNumber = 0; //opretter variabel der holder styr på linjenummer i tekstfilen

                //for loop der itererer gennem userIDs listen
                for(int i = 0; i < userIDs.Count; i++)
                {
                    //if statement der tjekker om userID fra input er det samme som userIDs på index i
                    if(userID.ToString() == userIDs[i])
                    {
                        lineNumber = i; //hvis if statement udsagn er sandt tildeles linenumber i
                    }
                }

                lines[lineNumber] = lines[lineNumber] + joinedClass;  //vi appender joinedclass variablen på pladsen "linenumber" i arrayet lines
                
                //for loop der kører så længe i er mindre end længden på lines
                for(int i = 0; i < lines.Length; i++)
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
            //Vi fjerner holdID
            joinedClasses.Remove(classID);

            string[] lines = System.IO.File.ReadAllLines(classesFilepath);
            string[] memberLines = System.IO.File.ReadAllLines(filepath);

            int x = 0;
            foreach (string line in lines)
            {
                string[]lineArray = line.Split(";");

                if (lineArray[8]==classID.ToString())
                {
                    for (int i = 9; i < lineArray.Count(); i++)
                    {
                        if (lineArray[i] == GetUserID().ToString())
                        {
                            int currentJoinedAmount = Convert.ToInt32(lineArray[i]);
                            currentJoinedAmount--;
                            lineArray[4] = currentJoinedAmount.ToString();

                            List<string> lineList = new List<string>(lineArray);
                            lineList.RemoveAt(i);
                            string[] newLine = lineList.ToArray();
                            lines[x] = string.Join(";", newLine);

                            
                        }
                    }
                }
                x++;

            }

            


            using (StreamWriter streamWriter = new StreamWriter(classesFilepath))
            {
                for (int i = 0; i < lines.Length; i++)
                {
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
