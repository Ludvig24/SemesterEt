using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FørsteSemester
{
    class User 
    {


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
        


        public void ShowRooms()
        {

        }

        public void ShowClass()
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
