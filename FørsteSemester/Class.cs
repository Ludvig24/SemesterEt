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

        int[] memberIDsInClass = new int[] {1, 2, 3, 4, 5};

        private string activity;
        private string className;
        private int classID;
        private bool status;
        private int availableSpots;
        private int joinedAmount;
        private char requiredGender;
        private byte requiredMaxAge;
        private byte requiredMinAge;

        public void AddMemberToClass(int ID)
        {
            int i = 0;
            List<int> userID = UserManager.GetUserData(7).ConvertAll(int.Parse);
            while (i < userID.Count)
            {
                if (ID == userID[i])
                {
                    Member joinMember = new Member();
                    joinMember = UserManager.LoadMember()[i];
                    membersInClass.Add(joinMember);
                    return;
                }
                else
                {
                    i++;
                }
            }




            //string joinedName = UserManager.GetUserData(0).ToString();
            //string joinedSurname = UserManager.GetUserData(1).ToString();
            //string joinedID = UserManager.GetUserData(7).ToString();

            //List<string> joinedMemberData  = new List<string>();
            //joinedMemberData.Add(joinedName);
            //joinedMemberData.Add(joinedSurname);
            //joinedMemberData.Add(joinedID);

            /*{
                List<string> names = UserManager.GetUserData(0);
                List<string> surnames = UserManager.GetUserData(1);
                int i = 0;
                while (i < UserManager.LoadMember().Count)
                {
                    if (name == names[i] && surname == surnames[i])
                    {
                        Member loginMember = new Member();
                        loginMember = UserManager.LoadMember()[i];
                        return loginMember;

                    }
                    else
                    {
                        i++;

                    }

                }
                return null;

            }*/

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
        public List<Member> GetMembersInClass()
        {
            return membersInClass;

        }
        public void SetMembersInClass(List<Member> membersInClass)
        {
            this.membersInClass = membersInClass;

        }

        public int[] GetMemberIDsInClass()
        {
            return memberIDsInClass;
        }


    }
}
