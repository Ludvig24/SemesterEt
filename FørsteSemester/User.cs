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
        //Opretter sti til Classes filen
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt");

        List<Class> ClassList = new List<Class>();

        //attributter til User klassen
        private string name;
        private string surname;
        private char gender;
        private byte age;
        private string city;
        private string password;
        private string userName;
        private int userID;

        //metode til at loade alle hold fra tekstfilen og gemme dem i en liste af Class objekter
        public List<Class> LoadTeams()
        {
            string[] lines = System.IO.File.ReadAllLines(filepath); //array der indeholder alle linjer i tekstfilen
            List<Class> teamsList = new List<Class>();//opretter en liste til at gemme Class objekterne


            for (int i = 0; i < lines.Count(); i++)
            {
                string teamData = lines[i]; //laver array indeks i om til en string variabel
                Class Teams = new Class();
                string[] Teamsplit = teamData.Split(";"); //splitter ved ; for at tilføje dataen til et array

                Teams.SetActivity(Teamsplit[0]);
                Teams.SetClassName(Teamsplit[1]);
                Teams.SetStatus(Convert.ToBoolean(Teamsplit[2]));
                Teams.SetAvailableSpots(Convert.ToInt32(Teamsplit[3]));
                Teams.SetJoinedAmount(Convert.ToInt32(Teamsplit[4]));
                Teams.SetRequiredGender(Convert.ToChar(Teamsplit[5]));
                Teams.SetRequiredMinAge(Convert.ToByte(Teamsplit[6]));
                Teams.SetRequiredMaxAge(Convert.ToByte(Teamsplit[7]));
                Teams.SetClassID(Convert.ToInt32(Teamsplit[8]));
                Teams.SetMemberIDsInClass(Teamsplit);

                teamsList.Add(Teams); //tilføjer det oprettede Class objekt til listen
            }

            return teamsList;
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
