using Project_Polished_Version.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class EmailEvents
    {
        public class EmailComposedEvent : EventArgs
        {
            public string RecipientEmail { get; set; }
            public string Subject { get; set; }
            public string MessageBody { get; set; }
        }

        public class EmailSentEvent : EventArgs
        {
            public string RecipientEmail { get; set; }
        }

        public class EmailErrorEvent : EventArgs
        {
            public string ErrorMessage { get; set; }
        }

        public class EmailsLoadedEvent : EventArgs
        {
            public List<Email> Emails { get; set; }
        }

        public class EmailSelectedEvent : EventArgs
        {
            public Email SelectedEmail { get; set; }
        }

        public class EmailsFilteredEvent : EventArgs
        {
            public List<Email> FilteredEmails { get; set; }
        }
        public class NavigationEvent : EventArgs
        {
            public string TargetWindow { get; set; }
        }
    }
}
