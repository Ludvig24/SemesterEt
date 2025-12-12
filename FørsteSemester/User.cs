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
        //Statiske variabler til at finde stien til tekstfilen, hvor hold data bliver gemt. Kombinere stien til mappen "Documents" med stien til Classes.txt
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);  //Finder stien til mappen "Documents" som er ens for alle computere
        static string classesFilepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt"); // Kombinerer stien til mappen "Documents" med stien til Classes.txt filen


        //Attributter til User klassen
        private string name;
        private string surname;
        private char gender;
        private byte age;
        private string city;
        private string password;
        private string userName;
        private int userID;

        //Metode LoadTeams til at loade alle hold fra tekstfilen og gemme dem i en liste af Class objekter
        public List<Class> LoadTeams()
        {
            string[] lines = System.IO.File.ReadAllLines(classesFilepath); //Array der indeholder alle linjer i tekstfilen
            List<Class> teamsList = new List<Class>();//Opretter en liste til at gemme Class objekterne

            for (int i = 0; i < lines.Count(); i++)
            {
                string teamData = lines[i]; //Laver array indeks i om til en string variabel
                Class Teams = new Class();
                string[] Teamsplit = teamData.Split(";"); //Splitter ved ; for at tilføje dataen til et array
                //Vi sætter atributter med set metoder på Class objektet Teams
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

                teamsList.Add(Teams); //Tilføjer det oprettede Class objekt til listen
            }

            return teamsList; //Returnerer listen
        }

        //Get set metoder
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
