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
        //Lav exception så man ikke kan skrive ; i input felter da vi bruger det til split

        //ChatGPT inspireret
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //forklar static
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt");
        public static void CreateMember(string name, string surname, char gender, byte age, string city, string username, string password)
        {
           
                Member member = new Member();
                member.SetName(name);
                member.SetSurname(surname);
                member.SetGender(gender);
                member.SetAge(age);
                member.SetCity(city);
                member.SetUserName(username.ToLower());
                member.SetPassword(password);

            //member.SetEmail(email); Dette vil være nice at have med, men er ikke need 
            //member.SetPhoneNumber(PhoneNumber);
            int membercount = LoadMember().Count + 1;
            while(true)
            {
                if (GetUserData(7).Contains(membercount.ToString()))
                {
                    membercount = membercount + 1;
                }
                else
                {
                    member.SetUserID(membercount);
                    break;
                }
                
            }
            


                SaveMember(member);
        }

        public static void SaveMember(Member member)
        {

             
            

            using (StreamWriter streamWriter = new StreamWriter(filepath,true))
            {
               
                
                streamWriter.Write(member.GetName() + ";");
                streamWriter.Write(member.GetSurname() + ";");
                streamWriter.Write(member.GetGender() + ";");
                streamWriter.Write(member.GetAge() + ";");
                streamWriter.Write(member.GetCity() + ";");
                streamWriter.Write(member.GetUserName() + ";");
                streamWriter.Write(member.GetPassword() + ";");
                streamWriter.Write(member.GetUserID());
                streamWriter.WriteLine();
            }
            
           

        }

        public static List<Member> LoadMember()
        {

            //Brug WriteAllLines til at smide 5-10 predefined brugere ind i filen
            string[] lines = System.IO.File.ReadAllLines(filepath);

            List<Member> members = new List<Member>();

            for(int i = 0; i < lines.Count(); i++)
            {
                string memberData = lines[i];
                Member member = new Member();
                string[] memberSplit = memberData.Split(";");

                member.SetName(memberSplit[0]);
                member.SetSurname(memberSplit[1]);
                member.SetGender(Convert.ToChar(memberSplit[2]));
                member.SetAge(Convert.ToByte(memberSplit[3]));
                member.SetCity(memberSplit[4]);
                member.SetUserName(memberSplit[5]);
                member.SetPassword(memberSplit[6]);
                member.SetUserID(Convert.ToInt32(memberSplit[7]));
                members.Add(member);
            }

            //returner liste af members - brug antal members/antal linjer i filen til at definere næste UserID
            return members;
        }

        public static Member Login(string Username, string Password)
        {
            List<string> Usernames = GetUserData(5);
            List<string> Passwords = GetUserData(6);
            int i = 0;
            while( i< LoadMember().Count)
            {
                if (Username == Usernames[i] && Password == Passwords[i])
                {
                    Member loginMember = new Member();
                    loginMember = LoadMember()[i];
                    return loginMember;

                }
                else
                {
                    i++;

                }
                
            }
            return null;

        }

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
