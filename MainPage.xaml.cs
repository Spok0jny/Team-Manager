using System.Collections.ObjectModel;

namespace Team_Manager;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Event> RecentEvents { get; set; }
    public ImageSource LatestEventPhoto { get; set; }
    public string LatestEventTitle { get; set; }
    public string LatestEventDate { get; set; }

    private readonly LocalDbServices _dbService;

    public MainPage(LocalDbServices dbService)
    {
        InitializeComponent();
        _dbService = dbService;

        RecentEvents = new ObservableCollection<Event>();
        BindingContext = this;

        LoadRecentEvents();
    }



    private async void LoadRecentEvents()
    {
        try
        {
            var events = (await _dbService.GetWydarzenia())
                .OrderByDescending(e => e.dataWydarzenia)
                .ToList();

            RecentEvents.Clear();

            foreach (var e in events)
            {
                RecentEvents.Add(new Event
                {
                    EventTitle = e.tytul,
                    EventDate = e.dataWydarzenia.ToString("dd MMM yyyy"),
                    EventPhoto = ImageSource.FromStream(() => new MemoryStream(e.zdjecie))
                });
            }

            
            if (RecentEvents.Any())
            {
                var latestEvent = RecentEvents.First();
                LatestEventPhoto = latestEvent.EventPhoto;
                LatestEventTitle = latestEvent.EventTitle;
                LatestEventDate = latestEvent.EventDate;

                OnPropertyChanged(nameof(LatestEventPhoto));
                OnPropertyChanged(nameof(LatestEventTitle));
                OnPropertyChanged(nameof(LatestEventDate));
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load events: {ex.Message}", "OK");
        }
    }

    public class Event
    {
        public string EventTitle { get; set; }
        public string EventDate { get; set; }
        public ImageSource EventPhoto { get; set; }
    }
}
