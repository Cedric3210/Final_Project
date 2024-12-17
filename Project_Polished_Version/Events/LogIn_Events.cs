using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class LogIn_Events 
    {
        public class UserAuthenticatedEvent : EventArgs
        {
            public int UserId { get; set; }
            public bool IsCompany { get; set; }
        }
       
        
        public class DataLoadedEvent : EventArgs { }
    }
}
