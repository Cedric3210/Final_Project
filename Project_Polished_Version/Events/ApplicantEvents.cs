using Project_Polished_Version.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project_Polished_Version.Events
{
    public class ApplicantEvents
    {
        public class NewsfeedLoadedEvent : EventArgs
        {
            public List<NewsFeed> NewsFeeds { get; set; }
        }

        public class PostRemovedEvent : EventArgs
        {
            public int PostId { get; set; }
        }

        public class NavigationEvent : EventArgs
        {
            public string TargetView { get; set; }
        }
        public class ProfileInitializedEvent : EventArgs
        {
            public ApplicantUser User { get; set; }
        }

        public class ProfileUpdatedEvent : EventArgs
        {
            public string Section { get; set; }
            public string Content { get; set; }
        }

        public class FriendRequestSentEvent : EventArgs
        {
            public int FriendId { get; set; }
        }
        public class UserListLoadedEvent : EventArgs
        {
            public List<ApplicantUser> Users { get; set; }
        }

        public class UserFilteredEvent : EventArgs
        {
            public List<ApplicantUser> FilteredUsers { get; set; }
        }

        public class UserSelectedEvent : EventArgs
        {
            public ApplicantUser SelectedUser { get; set; }
        }
        public class ResumeListLoadedEvent : EventArgs
        {
            public List<Resume> Resumes { get; set; }
        }
        public class ShowUserProfileEvent : EventArgs
        {
            public int UserId { get; set; }
            public string FullName { get; set; }
            public string JobTitle { get; set; }
            public string Address { get; set; }
        }

        public class NoDataFoundEvent : EventArgs { }


        public class UserRegisteredEvent : EventArgs
        {
            public int UserId { get; set; }
        }

        public class RowsInsertedEvent : EventArgs
        {
            public int UserId { get; set; }
        }

        public class ErrorEvent : EventArgs
        {
            public string ErrorMessage { get; set; }
        }
        public class EditEvent : EventArgs
        {
            public string InfoType { get; set; }
            public TextBox TextBox { get; set; }
            public Button Button { get; set; }
        }

        public class RequestCompanyEmailEvent : EventArgs { }

        public class ProvideCompanyEmailEvent : EventArgs
        {
            public string CompanyEmail { get; set; }
        }
    }
}
