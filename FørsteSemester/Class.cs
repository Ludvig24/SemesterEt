using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FørsteSemester
{
    class Class
    {

        private string activity;
        private string className;
        private int classID;
        private bool status;
        private int availableSpots;
        private int joinedAmount;
        private char requiredGender;
        private byte requiredMaxAge;
        private byte requiredMinAge;
        private string memberIDsInClass;

        public void AddMemberIDToClass(int UserID, int classID)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //forklar static
            string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt");

            
            classID.ToString();

            string MemberIDClass = "0";

            MemberIDClass = ";" + UserID;

            string[] lines = System.IO.File.ReadAllLines(filepath);
            List<string> ClassIDs = Admin.GetClassData(8);
            using (StreamWriter streamWriter = new StreamWriter(filepath)) //har ikke skrevet true - medfører at append er false - vi overskriver filen
            {
                
                int lineNumber = 0;

                for (int i = 0; i < ClassIDs.Count; i++)
                {
                    if (classID.ToString() == ClassIDs[i])
                    {
                        lineNumber = i;
                    }
                }

                lines[lineNumber] = lines[lineNumber] + MemberIDClass;
                for (int i = 0; i < lines.Length; i++)
                {
                    streamWriter.Write(lines[i]);
                }

            }
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

        public void SetMemberIDsInClass(string[] memberIDsInClass)
        {
            
            for (int i = 9; i < memberIDsInClass.Length; i++) 
            {
                this.memberIDsInClass = this.memberIDsInClass + memberIDsInClass[i];

            }

        }

    }
}
