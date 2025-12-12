using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FørsteSemester
{
    internal abstract class UserManager
    {
        //Statiske variabler til at finde stien til tekstfilen, hvor Medlem data bliver gemt. Kombinere stien til mappen "Documents" med stien til Members.txt
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Finder stien til mappen "Documents" som er ens for alle computere
        static string membersFilepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt"); // Kombinerer stien til mappen "Documents" med stien til Members.txt filen

        //Metoden CreateMember, hvor der oprettes et medlem med relevante parametre
        public static void CreateMember(string name, string surname, char gender, byte age, string city, string username, string password)
        {

            Member member = new Member(); //Opretter objekt af Member klassen
           //Sætter alle oplysninger for member via Set Metoder
            member.SetName(name);
            member.SetSurname(surname);
            member.SetGender(gender);
            member.SetAge(age);
            member.SetCity(city);
            member.SetUserName(username.ToLower());
            member.SetPassword(password);

            //Sætter unikt UserID til medlemmet baseret på antal medlemmer i systemet
            int membercount = LoadMember().Count + 1;
            while (true)
            {
                //if statement der tjekker det UserID vi kalder membercount allerede findes i vores program
                if (GetUserData(7).Contains(membercount.ToString())) //Tjekker om listen returnerert med GetUserData indeholder værdien gemt i membercount
                {
                    membercount = membercount + 1; //Tæller membercount variablen op med 1
                }
                else
                {
                    member.SetUserID(membercount); //Tildeler membercount som UserID til memberobjektet
                    break;
                }

            }


            //Kalder SaveMember metoden og sender member objektet med
            SaveMember(member);
        }


        //Metode SaveMember, der tilføjer medlemmet til Members.txt filen
        public static void SaveMember(Member member)
        {
            //Bruger en StreamWriter til at skrive memberens oplysninger ned på en linje i den text fil membersFilepath peger på
            //using indebære at den skal åbne og lukke filen korrekt efter brug, så man kan tilgå filen igen senere. Filen kan ikke tilgås flere steder samtidig så længe streamWriter er åben
            using (StreamWriter streamWriter = new StreamWriter(membersFilepath, true)) //Filepath er stien til den fil vi vil skrive i, og vi skriver true for at sige at den skal append(Tilføjer, ikke overskriver) hver gang der skrives frem for at overskrive
            {
                //Kalder Write på hver af memberens oplysninger gennem Get metoder. Der sættes ; bagerst på hver linjen så den nemt kan splittes
                streamWriter.Write(member.GetName() + ";");
                streamWriter.Write(member.GetSurname() + ";");
                streamWriter.Write(member.GetGender() + ";");
                streamWriter.Write(member.GetAge() + ";");
                streamWriter.Write(member.GetCity() + ";");
                streamWriter.Write(member.GetUserName() + ";");
                streamWriter.Write(member.GetPassword() + ";");
                streamWriter.Write(member.GetUserID());
                streamWriter.WriteLine(); //Kalder WriteLine for at gå ned til næste linje i filen så næste member kan skrives ned
            }

        }

        //Metode LoadMember som indlæser alle medlemmer fra Members.txt filen og returnerer dem som en liste af Member objekter
        public static List<Member> LoadMember()
        {

            //Opretter et string array kaldet lines. Vi tildeler arrayet alle linjer i filen Members.txt via ReadAllLines
            string[] lines = System.IO.File.ReadAllLines(membersFilepath);

            List<Member> members = new List<Member>(); //Liste af typen Member

            //for loop der itererer gennem arrayet lines.
            for (int i = 0; i < lines.Count(); i++)
            {
                string memberData = lines[i]; //Opretter en string hvor linjen på plads "i" i lines gemmes
                Member member = new Member(); //Opretter et member objekt
                string[] memberSplit = memberData.Split(";"); //Kalder Split(";") på stringen memberData og gemmer hvert elememt i arrayet memberSplit 

                //Sætter alle oplysninger for member objektet
                member.SetName(memberSplit[0]);
                member.SetSurname(memberSplit[1]);
                member.SetGender(Convert.ToChar(memberSplit[2]));
                member.SetAge(Convert.ToByte(memberSplit[3]));
                member.SetCity(memberSplit[4]);
                member.SetUserName(memberSplit[5]);
                member.SetPassword(memberSplit[6]);
                member.SetUserID(Convert.ToInt32(memberSplit[7]));
                
                List<int> JoinedClasses = new List<int>(); //Opretter en liste af typen int
                //for loop der itererer fra index 8 i membesplit og frem. Dette er fordi at alle classID'er et medlem er tilmeldt starter fra index 8 og frem i members.txt
                for (int j = 8; j < memberSplit.Count(); j++)
                {
                    JoinedClasses.Add(Convert.ToInt32(memberSplit[j])); //For hver iteration tilføjes stringen på plads j i membersplit til listen JoinedClasses
                }
                member.SetJoinedClasses(JoinedClasses); //Sætter JoinedClasses til listen JoinedClasses
                members.Add(member); //Tilføjer member objektet til listen members
            }

            //Returnerer liste af members
            return members;
        }


        //Metode Login, til at logge et medlem ind baseret på brugernavn og password
        //ved at iterere gennem brugernavne indtil et password passer dertil
        public static Member Login(string Username, string Password)
        {
            List<string> Usernames = GetUserData(5); //Henter en liste af alle usernames i members.txt
            List<string> Passwords = GetUserData(6); //Henter en liste af alle passwords i members.txt

            //while loop der itererer gennem members og tjekker om username og password i input passer med username og password for en bestemt member i filen members.txt
            int i = 0;
            while (i < LoadMember().Count)
            {
                if (Username == Usernames[i] && Password == Passwords[i]) //Hvis der findes et match mellem username og password i input og text fil:
                {
                    Member loginMember = new Member(); //Der oprettes et objekt af member klassen
                    loginMember = LoadMember()[i]; //Tildeler member objektet det specifikke member objekt vi er kommet til i loopen
                    return loginMember; //Returnerer member objektet

                }
                else //Hvis der ikke findes et match øges i med 1 og loopet starter forfra
                {
                    i++;

                }

            }
            return null; //Returnerer null hvis der ikke findes noget match efter hele loopen er kørt igennem

        }

        //Metode Login til at logge en admin ind baseret på brugernavn, password og dets ID
        public static Admin AdminLogin(string Username, string Password)
        {
            //Bruger GetUserData() metoden til at hente brugernavne, passwords og UserID's fra filen members.txt
            List<string> Usernames = GetUserData(5);
            List<string> Passwords = GetUserData(6);
            List<string> UserID = GetUserData(7);

            //while loop der itererer gennem members og tjekker om username og password i input passer med username og password for en bestemt member i filen members.txt
            //Der tjekkes om userID'et på den bestemte bruger er 1 da det er vores Admins id
            int i = 0;
            while (i < LoadMember().Count)
            {

                if (Username == Usernames[i] && Password == Passwords[i] && UserID[i] == "1")//Hvis der findes et match mellem username og password i input og text fil samt at UserID på plads i er 1:
                {
                    Admin loginAdmin = new Admin(); //Opretter objekt af klassen Admin
                    string[] lines = System.IO.File.ReadAllLines(membersFilepath); //Læser alle linjer i filen membersFilepath peger på og gemmer det i et string array
                    string adminLine = lines[0]; //Gemmer stringen på index 0 i en string kaldet adminLine
                    string[] adminData = adminLine.Split(";");//Kalder split på stringen ved hvert ";"

                    //Sætter alle oplysninger for Admin objektet med Set metoder
                    loginAdmin.SetName(adminData[0]);
                    loginAdmin.SetSurname(adminData[1]);
                    loginAdmin.SetGender(Convert.ToChar(adminData[2]));
                    loginAdmin.SetAge(Convert.ToByte(adminData[3]));
                    loginAdmin.SetCity(adminData[4]);
                    loginAdmin.SetUserName(adminData[5]);
                    loginAdmin.SetPassword(adminData[6]);
                    loginAdmin.SetUserID(Convert.ToInt32(adminData[7]));

                    return loginAdmin; //Returnerer Admin objektet

                }

                else
                {
                    i++; //Hvis der ikke findes et match øges i med 1 og loopet starter forfra

                }

            }
            return null; //Returnerer null hvis der ikke findes noget match efter hele loopen er kørt igennem
        }

        //Metode GetUserData til at hente en specifik type data fra alle medlemmer i en liste
        //baseret på dets indeks
        public static List<string> GetUserData(int a)
        {
            List<string> data = new List<string>(); //Opretter en liste af strings
            string dataPoint = ""; //Opretter en tom string
            string[] lines = System.IO.File.ReadAllLines(membersFilepath); //Læser alle linjer i den fil membersFilepath peger på og gemmer dem i et string array
            
            //for loop der itererer gennem hver linje i lines
            for (int i = 0; i < lines.Count(); i++)
            {
                string memberData = lines[i]; //Opretter en string kaldet memberData som vi tildeler værdien a lines på index i.
                string[] memberSplit = memberData.Split(";"); //Kalder split på memberData på ";" og gemmer arrayet i memberSplit
                dataPoint = memberSplit[a]; //Tildeler dataPoint værdien af memberSplit på index a
                data.Add(dataPoint); //Tilføjer dataPoint stringen til listen data
            }

            return data; //Returnerer listen data

        }

    }
}
