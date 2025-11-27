using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FørsteSemester
{
    class Member : User
    {
        

        private string joinedClass = "0";
        public void JoinClass(int classID)
        {
            classID.ToString();
            joinedClass = joinedClass + ";"+ classID;
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
