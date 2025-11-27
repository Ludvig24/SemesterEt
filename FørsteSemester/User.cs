using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FørsteSemester
{
    class User 
    {
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //forklar static
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt");

        List<Class> ClassList = new List<Class>();


        private string name;
        private string surname;
        private char gender;
        private byte age;
        private string city;
        private string password;
        private string userName;
        private string email;
        private int phoneNumber;
        private int userID;

        public List<Class> LoadTeams()
        {
            string[] lines = System.IO.File.ReadAllLines(filepath);
            List<Class> teamsList = new List<Class>();


            for (int i = 0; i < lines.Count(); i++)
            {


                string teamData = lines[i];
                Class Teams = new Class();
                string[] Teamsplit = teamData.Split(";");

                Teams.SetActivity(Teamsplit[0]);
                Teams.SetClassName(Teamsplit[1]);
                Teams.SetClassID(Convert.ToInt32(Teamsplit[2]));
                Teams.SetStatus(Convert.ToBoolean(Teamsplit[3]));
                Teams.SetAvailableSpots(Convert.ToInt32(Teamsplit[4]));
                Teams.SetJoinedAmount(Convert.ToInt32(Teamsplit[5]));
                Teams.SetRequiredGender(Convert.ToChar(Teamsplit[6]));
                Teams.SetRequiredMaxAge(Convert.ToByte(Teamsplit[7]));
                Teams.SetRequiredMinAge(Convert.ToByte(Teamsplit[8]));
                Teams.SetMemberIDsInClass(Teamsplit);



                teamsList.Add(Teams);
            }

            return teamsList;
        }

        public void ShowRooms()
        {

        }

        public string GetName()
        {
            return name;
        }

        public void SetName(string name)
        {
            this.name = name;   
        }

        public string GetSurname()
        {
            return surname;
        }

        public void SetSurname(string surname)
        {
            this.surname = surname;
        }

        public char GetGender()
        {
            return gender;
        }

        public void SetGender(char gender)
        {
            this.gender = gender;
        }

        public byte GetAge()
        {
            return age;
        }

        public void SetAge(byte age)
        {
            this.age = age;
        }

        public string GetCity()
        {
            return city;
        }

        public void SetCity(string city)
        {
            this.city = city;
        }

        public string GetUserName()
        {
            return userName;
        }

        public void SetUserName(string userName)
        {
            this.userName = userName;
        }

        public string GetPassword()
        {
            return password;
        }

        public void SetPassword(string password)
        {
            this.password = password;
        }

        public string GetEmail()
        {
            return email;
        }

        public void SetEmail(string email)
        {
            this.email = email;
        }

        public int GetPhoneNumber()
        {
            return phoneNumber;
        }

        public void SetPhoneNumber(int phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public int GetUserID()
        {
            return userID;
        }

        public void SetUserID(int userID)
        {
            this.userID = userID;
        }
    }
}
