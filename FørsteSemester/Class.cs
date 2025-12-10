using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Linq;

namespace FørsteSemester
{
    class Class
    {
        //Attributter til Hold
        private string activity;
        private string className;
        private int classID;
        private bool status;
        private int availableSpots;
        private int joinedAmount;
        private char requiredGender;
        private byte requiredMinAge;
        private byte requiredMaxAge;
        private string memberIDsInClass;


        //Metoden AddMemberIDToClass, som tilføje et medlems ID til et specifikt hold 
        public void AddMemberIDToClass(int UserID, int classID)
        {
            //Statiske variabler til at finde stien til tekstfilen, hvor hold data bliver gemt. Kombinere mappen "Documents" med stien til Classes.txt
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Finder stien til mappen "Documents" som er ens for alle computere
            string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt"); // Kombinerer stien til mappen "Documents" med stien til Classes.txt filen

            classID.ToString();

            string MemberIDClass = "0"; //Starter med 0 for at undgå fejl ved første tilføjelse
            MemberIDClass = ";" + UserID; //Tilføjer et semicolon før ID'et for at matche formatet i tekstfilen
            string[] lines = System.IO.File.ReadAllLines(filepath); //Læser alle linjer i tekstfilen Classes.txt og gemmer dem i arrayet lines 
            List<string> ClassIDs = Admin.GetClassData(8); //Opretter en liste over class Id'er via GetClassData metoden

            using (StreamWriter streamWriter = new StreamWriter(filepath)) //har ikke skrevet true - medfører at append er false - vi overskriver filen
            {
                //Opretter int variabel lineNumber
                int lineNumber = 0;

                //Loop der itererer gennem listen ClassIDs og ser hvornår ClassIDs på index i er lig classID variablen
                for (int i = 0; i < ClassIDs.Count; i++)
                {
                    //If statement der tjekker, hvornår det class id i ClassIDs på index i er lig med classID 
                    if (classID.ToString() == ClassIDs[i])
                    {
                        lineNumber = i; //Sætter lineNumber lig med i for at få den linje i lines hvor id'et classID er
                    }
                }

                string[] currentClass = lines[lineNumber].Split(";"); //Gemmer informationerne om den class på linjen "lineNumber" i et string array. Split kaldes for at splitte stringen op ved hvert ;
                int CurrentJoinAmount = Convert.ToInt32(currentClass[4]); //Opretter en int og gemmer værdien på index 4 (som svarer til joinedAmount) i currentClass arrayet. Convert.ToInt32 kaldes for at konverterer fra string til int
                CurrentJoinAmount++;  //Tæller CurrentJoinAmount op med 
                currentClass[4] = CurrentJoinAmount.ToString();  // Tildeler index 4 i currentClass den nye værdi af 
                string classStringJoined = string.Join(";", currentClass);  //Kalder string.Join og kombinerer hvert element i currentClass arrayet med et semicolon og gemmer det i en string
                lines[lineNumber] = classStringJoined; //Erstatter elementet på pladsen "lineNumber" i arrayet lines med den nye string classStringJoined
                lines[lineNumber] = lines[lineNumber] + MemberIDClass; // Tilføjer stringen på pladsen "linenumber" i lines arrayet stringen MemberIDClass

                //For loop der skriver arrayet lines ned i text filen Classes.txt. Loopet kører så længe tællervariablen i er mindre end længden på lines arrayet
                //for hver iteration skrives stringen på index "i" i lines arrayet ned i filen Classes.txt
                for (int i = 0; i < lines.Length; i++)
                {
                    streamWriter.Write(lines[i]);
                    streamWriter.WriteLine();
                }

            }
        }

        //Get Set metoder
        public void getStatus()
        {

        }

        public string GetActivity()
        {
            return activity;
        }

        public void SetActivity(string activity)
        {
            this.activity = activity;
        }

        public string GetClassName()
        {
            return className;
        }

        public void SetClassName(string ClassName)
        {
            this.className = ClassName;
        }

        public int GetClassID()
        {
            return classID;
        }

        public void SetClassID(int classID)
        {
            this.classID = classID;
        }

        public bool GetStatus()
        {
            return status;
        }

        public void SetStatus(bool status)
        {
            this.status = status;
        }

        public int GetAvailableSpots()
        {
            return availableSpots;
        }

        public void SetAvailableSpots(int availableSpots)
        {
            this.availableSpots = availableSpots;
        }

        public int GetJoinedAmount()
        {
            return joinedAmount;
        }

        public void SetJoinedAmount(int joinedamount)
        {
            this.joinedAmount = joinedamount;
        }

        public char GetRequiredGender()
        {
            return requiredGender;
        }

        public void SetRequiredGender(char requiredGender)
        {
            this.requiredGender = requiredGender;
        }

        public byte GetRequiredMaxAge()
        {
            return requiredMaxAge;
        }

        public void SetRequiredMaxAge(byte requiredMaxAge)
        {
            this.requiredMaxAge = requiredMaxAge;
        }
        public byte GetRequiredMinAge()
        {
            return requiredMinAge;
        }

        public void SetRequiredMinAge(byte requiredMinAge)
        {
            this.requiredMinAge = requiredMinAge;
        }

        public string GetMemberIDsInClass()
        {
            return memberIDsInClass;
        }

        public void SetMemberIDsInClass(string[] memberIDsInClass) //
        {
            
            for (int i = 9; i < memberIDsInClass.Length; i++) //Starter fra index 9 for at undgå de første attributter i klassen
            {
                //Tilføjer hvert medlems ID til memberIDsInClass attributten
                this.memberIDsInClass = this.memberIDsInClass + memberIDsInClass[i];

            }

        }

    }
}
