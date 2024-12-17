using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class HashedPasswordEvent
    {
        public class PasswordVisibilityChangedEvent : EventArgs
        {
            public bool IsPasswordVisible { get; set; }
            public string Password { get; set; }
        }
    }
}
