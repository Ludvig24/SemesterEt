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
            member.SetUserName(username);
            member.SetPassword(password);
            //member.SetEmail(email);
            //member.SetPhoneNumber(PhoneNumber);
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
                streamWriter.Write(member.GetPassword());
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


                members.Add(member);
            }

            //returner liste af members - brug antal members/antal linjer i filen til at definere næste UserID
            return members;
        }

        public static void Login()
        {

        }

        //kunne laves generel hvor man bare sender et index i arrayet med som parameter
        
        /*public static List<string> GetUserNames()
        {
            List<string> usernames = new List<string>();
            string username = "";
            string[] lines = System.IO.File.ReadAllLines(filepath);
            for(int i = 0; i < lines.Count(); i++)
            {
                string memberData = lines[i];
                string[] memberSplit = memberData.Split(";");
                username = memberSplit[5];
                usernames.Add(username); 
            }

            return usernames;

        }*/

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
