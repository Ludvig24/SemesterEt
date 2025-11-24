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

            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName; //ChatGPT inspireret
            string filepath = Path.Combine(directory, "Members.txt"); 

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

        public static void LoadMember()
        {
            //returner liste af members - brug antal members/antal linjer i filen til at definere næste UserID
        }

        public static void Login()
        {

        }

    }
}
