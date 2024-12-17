using System.Collections.ObjectModel;

namespace Team_Manager.KlubPages;

public partial class ListaZawodnikow : ContentPage
{
    private readonly LocalDbServices _dbService;
    private ObservableCollection<Zawodnicy> _zawodnicy;
    public ListaZawodnikow(LocalDbServices dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        _zawodnicy = new ObservableCollection<Zawodnicy>();
        listView.ItemsSource = _zawodnicy;
        _ = WczytajListeZawodnikow();
    }

    private async void zapiszButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(imieEntry.Text) && !string.IsNullOrWhiteSpace(nazwiskoEntry.Text))
        {
            var nowyZawodnik = new Zawodnicy
            {
                Imie = imieEntry.Text,
                Nazwisko = nazwiskoEntry.Text
            };

            await _dbService.CreateZawodnik(nowyZawodnik);
            imieEntry.Text = string.Empty;
            nazwiskoEntry.Text = string.Empty;

            await WczytajListeZawodnikow();
        }
        else
        {
            await DisplayAlert("B³¹d", "Uzupe³nij wszystkie pola.", "OK");
        }

    }

    private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
    {

    }
    private async Task WczytajListeZawodnikow()
    {
 
        var listaZawodnikow = await _dbService.GetZawodnicy();

   
        _zawodnicy.Clear();
        foreach (var zawodnik in listaZawodnikow)
        {
            _zawodnicy.Add(zawodnik);
        }
    }
}