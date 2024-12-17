using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class PostsEvents
    {
        public class PostCreatedEvent : EventArgs
        {
            public string Content { get; set; }
            public string CreatedBy { get; set; }
            public DateTime CreatedAt { get; set; }
        }

        public class PostFailedEvent : EventArgs
        {
            public string ErrorMessage { get; set; }
        }

        public class NavigationEvent : EventArgs
        {
            public string TargetWindow { get; set; }
        }
    }
}
