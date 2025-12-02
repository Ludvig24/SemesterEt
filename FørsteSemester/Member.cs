using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FørsteSemester
{
    class Member : User
    {
        //opretter sti til Members filen
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt");

        private string joinedClass = "";

        //metode som tilføjer et holds ID til brugeren i Members filen
        public void JoinClass(int classID, int userID)
        {
            List<string> userIDs = UserManager.GetUserData(7);
            string[] lines = System.IO.File.ReadAllLines(filepath);
            classID.ToString();
            joinedClass = ";"+ classID;
            using (StreamWriter streamWriter = new StreamWriter(filepath)) //har ikke skrevet true - medfører at append er false - vi overskriver filen
            {
                int lineNumber = 0;

                for(int i = 0; i < userIDs.Count; i++)
                {
                    if(userID.ToString() == userIDs[i])
                    {
                        lineNumber = i;
                    }
                }

                lines[lineNumber] = lines[lineNumber] + joinedClass;  
                for(int i = 0; i < lines.Length; i++)
                {
                    streamWriter.Write(lines[i]);
                    streamWriter.WriteLine();
                }
                
            }
        }
        
        public void LeaveClass()
        {

        }

        public void RentRoom()
        {

        }

        public void CancelRentRoom()
        {

        }

        public void DeleteProfile()
        {

        }

        public void ViewStatus()
        {

        }





    }
}
