using System;
using System.Collections.Generic;
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



        }

        public static void SaveMember()
        {

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
