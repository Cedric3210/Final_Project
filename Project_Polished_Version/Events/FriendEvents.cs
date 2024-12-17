using Project_Polished_Version.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class FriendEvents
    {
        public class FriendsFetchedEvent : EventArgs
        {
            public List<ApplicantUser> Friends { get; set; }
        }

        public class FriendAcceptedEvent : EventArgs
        {
            public ApplicantUser AcceptedFriend { get; set; }
        }

        public class FriendUnfriendedEvent : EventArgs
        {
            public ApplicantUser UnfriendedFriend { get; set; }
        }

        public class ErrorEvent : EventArgs
        {
            public string ErrorMessage { get; set; }
        }
    }
}
