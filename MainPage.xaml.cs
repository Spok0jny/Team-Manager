using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace Team_Manager
{
    public partial class MainPage : ContentPage
    {
       
        public ObservableCollection<Event> RecentEvents { get; set; }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            RecentEvents = new ObservableCollection<Event>
            {
                new Event { EventTitle = "Daniel B przechodzi do FC Mendoza", EventDate = "15.12.2024" },
                new Event { EventTitle = "Lewandowski Robert z hattrickiem", EventDate = "14.12.2024" },
                new Event { EventTitle = "Daniel B marnuje podanie Roberta Lewandowskiego!", EventDate = "14.12.2024" },
                new Event { EventTitle = "Daniel B z polamana noga po wejsciu Virgila Van Dika", EventDate = "09.11.2024" }
            };
        }

       
        public class Event
        {
            public string EventTitle { get; set; }
            public string EventDate { get; set; }
        }
    }
}
