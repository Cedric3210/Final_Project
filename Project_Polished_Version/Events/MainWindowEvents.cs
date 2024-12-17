using Project_Polished_Version.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class MainWindowEvents
    {
        public class RequestUserDictionaryEvent : EventArgs { }

        // Event to provide the user dictionary
        public class UserSelectedEvent : EventArgs
        {
            public string SelectedName { get; set; }
        }

        public class ProvideUserDictionaryEvent : EventArgs
        {
            public Dictionary<int, ApplicantUser> UserDictionary { get; set; }
        }

        // Event to request the company dictionary
        public class RequestCompanyDictionaryEvent : EventArgs { }

        // Event to provide the company dictionary
        public class ProvideCompanyDictionaryEvent : EventArgs
        {
            public Dictionary<int, CompanyUser> CompanyDictionary { get; set; }
        }
        public class ShowUserProfileEvent : EventArgs
        {
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string JobTitle { get; set; }
            public string Address { get; set; }
        }
    }
}
