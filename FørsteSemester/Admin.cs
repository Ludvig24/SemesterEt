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

        public void CreateClass(string activity, string classname, byte availableSpots, char requiredGender, byte requiredMaxAge, byte requiredMinAge)
        {
            Class Team = new Class(); // Det skal hedde andet end class
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
            
        }
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
                //Teams.SetMembersInClass();

                teamsList.Add(Teams);
            }

            return teamsList;
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
            using (StreamWriter streamWriter = new StreamWriter(filepath))
            {
                streamWriter.Write(team.GetActivity());
                streamWriter.Write(team.GetClassName());
                streamWriter.Write(team.GetStatus());
                streamWriter.Write(team.GetAvailableSpots());
                streamWriter.Write(team.GetJoinedAmount());
                streamWriter.Write(team.GetRequiredGender());
                streamWriter.Write(team.GetRequiredMaxAge());
                streamWriter.Write(team.GetRequiredMinAge());
                streamWriter.Write(team.GetMembersInClass());// hvordan ser det ud i en txt?
                streamWriter.Write(team.GetClassID());

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
