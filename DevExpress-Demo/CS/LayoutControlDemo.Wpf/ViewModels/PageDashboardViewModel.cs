using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LayoutControlDemo {
    public class PageDashboardViewModel {
        public List<Agent> Agents { get { return LayoutControlDemo.Agents.DataSource; } }
        public IList<Listing> Listings { get { return LayoutControlDemo.Listings.DataSource; } }
    }
    public class Agent {
        public Agent(string uri) {
            PhotoUri = new Uri(LayoutControlDemoModule.UriPrefix + uri, UriKind.Relative);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public Uri PhotoUri { get; private set; }
    }
    public static class Agents {
        public static readonly List<Agent> DataSource =
            new List<Agent> {
                new Agent ("/Images/Agents/1.jpg") { FirstName = "Anthony", LastName = "Peterson", Phone = "(555) 444-0782"},
                new Agent ("/Images/Agents/2.jpg") { FirstName = "Cindy", LastName = "Haneline", Phone = "(555) 638-9820"},
                new Agent ("/Images/Agents/3.jpg") { FirstName = "Albert", LastName = "Walker", Phone = "(555) 232-2303"},
                new Agent ("/Images/Agents/4.jpg") { FirstName = "Rachel", LastName = "Scott", Phone = "(555) 249-1614"},
                new Agent ("/Images/Agents/5.jpg") { FirstName = "Vernon", LastName = "Rounds", Phone = "(555) 682-5403"},
                new Agent ("/Images/Agents/6.jpg") { FirstName = "Andrew", LastName = "Carter", Phone = "(555) 578-3946"}
            };
    }
}
