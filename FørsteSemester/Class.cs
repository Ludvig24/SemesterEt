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

        List<Member> membersInClass = new List<Member>();

        private string activity;
        private string className;
        private int classID;
        private bool status;
        private int availableSpots;
        private int joinedAmount;
        private bool requiredGender;
        private byte requiredAge;

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

        public bool GetRequiredGender()
        {
            return requiredGender;
        }

        public void SetRequiredGender(bool requiredGender)
        {
            this.requiredGender = requiredGender;
        }

        public byte GetRequiredAge()
        {
            return requiredAge;
        }

        public void SetRequiredAge(byte requiredAge)
        {
            this.requiredAge = requiredAge;
        }





    }
}
