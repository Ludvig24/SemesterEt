using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FørsteSemester
{
    class Admin : User
    {


        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //forklar static
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt");


        List<Member> memberList = new List<Member>();

        public void SletMember()
        {

        }

        public void CreateClass(string activity, string classname, int availableSpots, char requiredGender, byte requiredMaxAge, byte requiredMinAge)
        {
            Class Team = new Class();
            Team.SetActivity(activity);
            Team.SetClassName(classname);
            Team.SetStatus(false);
            Team.SetAvailableSpots(availableSpots);
            Team.SetJoinedAmount(0);
            Team.SetRequiredGender(requiredGender);
            Team.SetRequiredMaxAge(requiredMaxAge);
            Team.SetRequiredMinAge(requiredMinAge);

            int TeamID = LoadTeams().Count + 1;
            while (true)
            {
                if (GetClassData(9).Contains(TeamID.ToString()))
                {
                    TeamID = TeamID + 1;
                }
                else
                {
                    Team.SetClassID(TeamID);
                    break;
                }

            }
            SaveClass(Team);

        }
        
        public static List<string> GetClassData(int a)
        {
            List<string> data = new List<string>();
            string dataPoint = "";
            string[] lines = System.IO.File.ReadAllLines(filepath);
            for (int i = 0; i < lines.Count(); i++)
            {
                string ClassData = lines[i];
                string[] ClassSplit = ClassData.Split(";");
                dataPoint = ClassSplit[a];
                data.Add(dataPoint);
            }

            return data;

        }

        public void SaveClass(Class team)
        {
            using (StreamWriter streamWriter = new StreamWriter(filepath, true))
            {
                streamWriter.Write(team.GetActivity()+ ";");
                streamWriter.Write(team.GetClassName()+";");
                streamWriter.Write(team.GetStatus()+";");
                streamWriter.Write(team.GetAvailableSpots()+";");
                streamWriter.Write(team.GetJoinedAmount()+";");
                streamWriter.Write(team.GetRequiredGender()+";");
                streamWriter.Write(team.GetRequiredMaxAge()+";");
                streamWriter.Write(team.GetRequiredMinAge()+";");
                streamWriter.Write(team.GetClassID()+";");
                streamWriter.Write(team.GetMemberIDsInClass()+";");
                streamWriter.WriteLine();
            }
        }



        public void DeleteClass()
        {

        }

        public void RemoveMemberFromClass()
        {

        }

        public void RoomRecord()
        {

        }






    }
}
