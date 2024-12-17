using Project_Polished_Version.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Polished_Version.Events
{
    public class CompanyEvents
    {
        public class NewsfeedLoadedEvent : EventArgs
        {
            public List<NewsFeed> NewsFeedItems { get; set; }
        }

        public class NavigationEvent : EventArgs
        {
            public string TargetWindow { get; set; }
        }

        public class SearchUpdatedEvent : EventArgs
        {
            public string SearchQuery { get; set; }
        }
        public class ProfileLoadedEvent : EventArgs
        {
            public CompanyUser CompanyData { get; set; }
        }

        public class AboutDataLoadedEvent : EventArgs
        {
            public string AboutContent { get; set; }
        }

        public class AboutDataSavedEvent : EventArgs
        {
            public bool IsSuccessful { get; set; }
        }
        public class CompanyListLoadedEvent : EventArgs
        {
            public List<CompanyUser> Companies { get; set; }
        }

        public class CompanyFilteredEvent : EventArgs
        {
            public List<CompanyUser> FilteredCompanies { get; set; }
        }

        public class CompanySelectedEvent : EventArgs
        {
            public CompanyUser SelectedCompany { get; set; }
        }


        public class CompanyRegisteredEvent : EventArgs
        {
            public string CompanyName { get; set; }
            public string CompanyEmail { get; set; }
        }

        public class CompanyInfoAddedEvent : EventArgs
        {
            public int CompanyId { get; set; }
        }

        public class ErrorEvent : EventArgs
        {
            public string ErrorMessage { get; set; }
        }

    }
}
