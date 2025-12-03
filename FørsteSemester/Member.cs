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
        //Opretter sti til mappen "Documents" også kombinere det med stien ind til Members.txt filen
        static string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        static string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Members.txt");

        private string joinedClass = "";
        private List<int> joinedClasses = new List<int>();

        //metode som tilføjer et holds ID til brugeren i Members filen
        public void JoinClass(int classID, int userID)
        {
            joinedClasses.Add(classID); //Tilføjer HoldId til en liste
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
        
        //Metoden LeaveClass,
        public void LeaveClass(int classID)
        {
            //Vi fjerner holdID
            joinedClasses.Remove(classID);

            //Opretter sti til mappen "Documents" også kombinere det med stien ind til Class.txt filen
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filepath = Path.Combine(dir, "GitHub\\SemesterEt\\FørsteSemester\\Classes.txt");
            
            string[] lines = System.IO.File.ReadAllLines(filepath);

            int x = 0;
            foreach (string line in lines)
            {
                string[]lineArray = line.Split(";");

                if (lineArray[8]==classID.ToString())
                {
                    for (int i = 9; i < lineArray.Count(); i++)
                    {
                        if (lineArray[i] == GetUserID().ToString())
                        {
                            List<string> lineList = new List<string>(lineArray);
                            lineList.RemoveAt(i);
                            string[] newLine = lineList.ToArray();
                            lines[x] = string.Join(";", newLine);
                        }
                    }
                }

                x++;
            }

            using (StreamWriter streamWriter = new StreamWriter(filepath))
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    streamWriter.Write(lines[i]);
                    streamWriter.WriteLine();
                }
            }
        }

        //Metoden DeleteProfile, 
        public void DeleteProfile()
        {

        }

        public void ViewStatus()
        {

        }





    }
}
