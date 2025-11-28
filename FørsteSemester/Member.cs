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
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //forklar static
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt");

        private string joinedClass = "";
        public void JoinClass(int classID)
        {
            classID.ToString();
            joinedClass = ";"+ classID;
            using (StreamWriter streamWriter = new StreamWriter(filepath, true))
            {
                streamWriter.Write(
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
