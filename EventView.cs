
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace Team_Manager.ViewModels;
public class WydarzeniaViewModel : INotifyPropertyChanged
{
    public ObservableCollection<BindingClassStronaGlowna> RecentEvents { get; set; } = new();
    public ImageSource LatestEventPhoto { get; set; }
    public string LatestEventTitle { get; set; }
    public string LatestEventDate { get; set; }

    private readonly LocalDbServices _dbService;

    public WydarzeniaViewModel(LocalDbServices dbService)
    {
        _dbService = dbService;
        LoadRecentEvents();
    }

    public async void LoadRecentEvents()
    {
        try
        {
            var events = (await _dbService.GetWydarzenia())
                .OrderByDescending(e => e.dataWydarzenia)
                .ToList();

            RecentEvents.Clear();

            foreach (var e in events)
            {
                RecentEvents.Add(new BindingClassStronaGlowna
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

    public event PropertyChangedEventHandler PropertyChanged;
    private void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
