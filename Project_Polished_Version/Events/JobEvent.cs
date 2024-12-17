using Project_Polished_Version.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class JobEvent
    {
        public class JobCreatedEvent : EventArgs
        {
            public string JobTitle { get; set; }
            public string CompanyName { get; set; }
        }

        public class JobCreationFailedEvent : EventArgs
        {
            public string ErrorMessage { get; set; }
        }

        public class NavigationEvent : EventArgs
        {
            public string TargetWindow { get; set; }
        }
        public class JobsLoadedEvent : EventArgs
        {
            public List<Jobs> CompanyJobs { get; set; }
        }

        public class JobsLoadedEventForJobs : EventArgs
        {
            public List<Jobs> Jobs { get; set; }
        }


        public class JobRemovedEvent : EventArgs
        {
            public Jobs RemovedJob { get; set; }
        }

        public class ErrorEvent : EventArgs
        {
            public string ErrorMessage { get; set; }
        }

        public class ConfirmationEvent : EventArgs
        {
            public string Message { get; set; }
            public Action<bool> OnConfirmation { get; set; }
        }
        public class JobAppliedEvent : EventArgs
        {
            public string Message { get; set; }
        }

        public class SearchEvent : EventArgs
        {
            public string SearchText { get; set; }
        }
    }
}
