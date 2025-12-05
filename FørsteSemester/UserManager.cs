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


        //opretter sti til Members.txt filen som skal være static
        //da den ellers ville kunne tilgå alle filer på ens computer
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); 
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt");

        //metode til at oprette et medlem med relevante parametre
        public static void CreateMember(string name, string surname, char gender, byte age, string city, string username, string password)
        {
           
                Member member = new Member(); //Opretter objekt af Member klassen
                //setter alle oplysninger for member via Set Metoder
                member.SetName(name);
                member.SetSurname(surname);
                member.SetGender(gender);
                member.SetAge(age);
                member.SetCity(city);
                member.SetUserName(username.ToLower());
                member.SetPassword(password);

            //member.SetEmail(email); Dette vil være nice at have med, men er ikke need 
            //member.SetPhoneNumber(PhoneNumber);

            //Sætter unikt UserID til medlemmet baseret på antal medlemmer i systemet
            int membercount = LoadMember().Count + 1;
            while(true)
            {
                //if statement der tjekker det UserID vi kalder membercount allerede findes i vores program
                if (GetUserData(7).Contains(membercount.ToString())) //tjekker om listen returnerert med GetUserData indeholder værdien gemt i membercount
                {
                    membercount = membercount + 1; //tæller membercount variablen op med 1
                }
                else
                {
                    member.SetUserID(membercount); //tildeler membercount som UserID til memberobjektet
                    break;
                }
                
            }


            //kalder SaveMember metoden og sender member objektet med
            SaveMember(member);
        }


        //Metode der tilføjer medlemmet til Members.txt filen
        public static void SaveMember(Member member)
        {   //bruger en StreamWriter til at skrive memberens oplysninger ned på en linje i den text fil filepath peger på
            using (StreamWriter streamWriter = new StreamWriter(filepath,true)) //filepath er stien til den fil vi vil skrive i og vi skriver true for at sige at den skal append hver gang der skrives frem for at overskrive
            {
                //kalder Write på hver af memberens oplysninger gennem Get metoder. Der sættes ; bagerst på hver linjen så den nemt kan splittes
                streamWriter.Write(member.GetName() + ";");
                streamWriter.Write(member.GetSurname() + ";");
                streamWriter.Write(member.GetGender() + ";");
                streamWriter.Write(member.GetAge() + ";");
                streamWriter.Write(member.GetCity() + ";");
                streamWriter.Write(member.GetUserName() + ";");
                streamWriter.Write(member.GetPassword() + ";");
                streamWriter.Write(member.GetUserID());
                streamWriter.WriteLine(); //kalder WriteLine for at gå ned til næste linje i filen så næste member kan skrives ned
            }
            
           

        }

        //indlæser alle medlemmer fra Members.txt filen og returnerer dem som en liste af Member objekter
        public static List<Member> LoadMember()
        {

            //opretter et string array kaldet lines. Vi tildeler arrayet alle linjer i filen Members.txt via ReadAllLines
            string[] lines = System.IO.File.ReadAllLines(filepath);

            
            List<Member> members = new List<Member>(); //liste af typen Member

            //for loop der itererer gennem arrayet lines.
            for (int i = 0; i < lines.Count(); i++)
            {
                string memberData = lines[i]; //opretter en string hvor linjen på plads "i" i lines gemmes
                Member member = new Member(); //opretter et member objekt
                string[] memberSplit = memberData.Split(";"); //kalder Split(";") på stringen memberData og gemmer hvert elememt i arrayet memberSplit 
                List<int> JoinedClasses = new List<int>(); //opretter en liste af typen int

                //setter alle oplysninger for member objektet
                member.SetName(memberSplit[0]);
                member.SetSurname(memberSplit[1]);
                member.SetGender(Convert.ToChar(memberSplit[2]));
                member.SetAge(Convert.ToByte(memberSplit[3]));
                member.SetCity(memberSplit[4]);
                member.SetUserName(memberSplit[5]);
                member.SetPassword(memberSplit[6]);
                member.SetUserID(Convert.ToInt32(memberSplit[7]));
                //for loop der itererer fra index 8 i membesplit og frem
                for (int j = 8; j < memberSplit.Count(); j++)
                {
                    JoinedClasses.Add(Convert.ToInt32(memberSplit[j])); //for hver iteration tilføjes stringen på plads j i membersplit til listen JoinedClasses
                }
                member.SetJoinedClasses(JoinedClasses); //Setter JoinedClasses til listen JoinedClasses
                members.Add(member); //tilføjer member objektet til listen members
            }

            //returner liste af members
            return members;
        }


        //metode til at logge et medlem ind baseret på brugernavn og password
        //ved at iterere gennem brugernavne indtil et password passer dertil
        public static Member Login(string Username, string Password)
        {
            List<string> Usernames = GetUserData(5); //henter en liste af alle usernames i members.txt
            List<string> Passwords = GetUserData(6); //henter en liste af alle passwords i members.txt
            
            //while loop der itererer gennem members og tjekker om username og password i input passer med username og password for en bestemt member i filen members.txt
            int i = 0;
            while( i< LoadMember().Count)
            {
                if (Username == Usernames[i] && Password == Passwords[i]) //hvis der findes et match mellem username og password i input og text fil:
                {
                    Member loginMember = new Member(); //der oprettes et objekt af member klassen
                    loginMember = LoadMember()[i]; //tildeler member objektet det specifikke member objekt vi er kommet til i loopen
                    return loginMember; 

                }
                else //hvis der ikke findes et match øges i med 1 og loopet starter forfra
                {
                    i++;

                }
                
            }
            return null;

        }

        //metode til at logge en admin ind baseret på brugernavn, password og dets ID
        public static Admin AdminLogin(string Username, string Password)
        {
            List<string> Usernames = GetUserData(5); 
            List<string> Passwords = GetUserData(6);
            List<string> UserID = GetUserData(7);
            

            int i = 0;
            while (i < LoadMember().Count)
            {

                if (Username == Usernames[i] && Password == Passwords[i] && UserID[i] == "1")
                {
                    Admin loginAdmin = new Admin();
                    string[] lines = System.IO.File.ReadAllLines(filepath);
                    string adminLine = lines[0];
                    adminLine.Split(";");
                    string[] adminData = adminLine.Split(";");

                    loginAdmin.SetName(adminData[0]);
                    loginAdmin.SetSurname(adminData[1]);
                    loginAdmin.SetGender(Convert.ToChar (adminData[2]));
                    loginAdmin.SetAge(Convert.ToByte (adminData[3]));
                    loginAdmin.SetCity(adminData[4]);
                    loginAdmin.SetUserName(adminData[5]);
                    loginAdmin.SetPassword(adminData[6]);
                    loginAdmin.SetUserID(Convert.ToInt32 (adminData[7]));

                    return loginAdmin;

                }

                else
                {
                    i++;

                }

            }
            return null;
        }

        //metode til at hente en specifik type data fra alle medlemmer i en liste
        //baseret på dets indeks
        public static List<string> GetUserData(int a) 
        {
            List<string> data = new List<string>();
            string dataPoint = "";
            string[] lines = System.IO.File.ReadAllLines(filepath);
            for (int i = 0; i < lines.Count(); i++)
            {
                string memberData = lines[i];
                string[] memberSplit = memberData.Split(";");
                dataPoint = memberSplit[a];
                data.Add(dataPoint);
            }

            return data;

        }

    }
}
