using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FørsteSemester
{
    class Class
    {

        //List<Member> membersInClass = new List<Member>();

        private string activity;
        private string className;
        private int classID;
        private bool status;
        private int availableSpots;
        private int joinedAmount;
        private char requiredGender;
        private byte requiredMaxAge;
        private byte requiredMinAge;
        private string memberIDsInClass = "";

        public void AddMemberIDToClass(int ID)
        {
            memberIDsInClass = memberIDsInClass + ";" + ID.ToString();
        }
        public void getStatus()
        {

        }

        public string GetActivity()
        {
            return activity;
        }

        public void SetActivity(string activity)
        {
            this.activity = activity;
        }

        public string GetClassName()
        {
            return className;
        }

        public void SetClassName(string ClassName)
        {
            this.className = className;
        }

        public int GetClassID()
        {
            return classID;
        }

        public void SetClassID(int classID)
        {
            this.classID = classID;
        }

        public bool GetStatus()
        {
            return status;
        }

        public void SetStatus(bool status)
        {
            this.status = status;
        }

        public int GetAvailableSpots()
        {
            return availableSpots;
        }

        public void SetAvailableSpots(int availableSpots)
        {
            this.availableSpots = availableSpots;
        }

        public int GetJoinedAmount()
        {
            return joinedAmount;
        }

        public void SetJoinedAmount(int joinedamount)
        {
            this.joinedAmount = joinedAmount;
        }

        public char GetRequiredGender()
        {
            return requiredGender;
        }

        public void SetRequiredGender(char requiredGender)
        {
            this.requiredGender = requiredGender;
        }

        public byte GetRequiredMaxAge()
        {
            return requiredMaxAge;
        }

        public void SetRequiredMaxAge(byte requiredMaxAge)
        {
            this.requiredMaxAge = requiredMaxAge;
        }
        public byte GetRequiredMinAge()
        {
            return requiredMinAge;
        }

        public void SetRequiredMinAge(byte requiredMinAge)
        {
            this.requiredMinAge = requiredMinAge;
        }

        public string GetMemberIDsInClass()
        {
            return memberIDsInClass;
        }


    }
}
