using System.Collections.ObjectModel;
using Team_Manager.AdminPages;

namespace Team_Manager.KlubPages;

public partial class ListaZawodnikow : ContentPage
{
    private readonly LocalDbServices _dbService;
    private ObservableCollection<Zawodnicy> _zawodnicy;
    private ObservableCollection<Zawodnicy> _zawodnicyLawka;
    public List<Zawodnicy> ListaUsunZawodnika { get; set; }

    public ListaZawodnikow(LocalDbServices dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        _zawodnicy = new ObservableCollection<Zawodnicy>();
        _zawodnicyLawka = new ObservableCollection<Zawodnicy>();
        listViewLawka.ItemsSource = _zawodnicyLawka;
        listViewSklad.ItemsSource = _zawodnicy;
        _ = WczytajListeZawodnikow();
        WczytajUsunZawodnikow();
    }

    private async void zapiszButton_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(imieEntry.Text) &&
            !string.IsNullOrWhiteSpace(nazwiskoEntry.Text) &&
            pozycjaEntry.SelectedItem != null &&
            !string.IsNullOrWhiteSpace(numerEntry.Text) &&
            int.TryParse(wiekEntry.Text, out int wiek) &&
            int.TryParse(numerEntry.Text, out int numer))
        {
            var nowyZawodnik = new Zawodnicy
            {
                Numer = numer,
                Pozycja = pozycjaEntry.SelectedItem.ToString(),
                Imie = imieEntry.Text,
                Nazwisko = nazwiskoEntry.Text,
                Wiek = wiek,
                KoniecKontraktu = koniecKontraktuPicker.Date.ToString("dd/MM/yyyy")
            };

            await _dbService.CreateZawodnik(nowyZawodnik);

            //test
            numerEntry.Text = string.Empty;
            pozycjaEntry.SelectedItem = null;
            imieEntry.Text = string.Empty;
            nazwiskoEntry.Text = string.Empty;
            wiekEntry.Text = string.Empty;
            koniecKontraktuPicker.Date = DateTime.Now;

            await WczytajListeZawodnikow();
        }
        else
        {
            await DisplayAlert("B³¹d", "Uzupe³nij wszystkie pola poprawnie.", "OK");
        }
    }



    private async Task WczytajListeZawodnikow()
    {
        var listaZawodnikow = await _dbService.GetZawodnicy();

        _zawodnicy.Clear();
        _zawodnicyLawka.Clear();
        foreach (var zawodnik in listaZawodnikow)
        {

            _zawodnicy.Add(zawodnik);
        }
    }

    private void OnFrameTapped(object sender, EventArgs e)
    {
        DisplayAlert("ok", "ok", "ok");
    }
    private async void WczytajUsunZawodnikow()
    {

        var ListaUsunZawodnika = await _dbService.GetZawodnicy();
        DeletePicker.ItemsSource = ListaUsunZawodnika.Select(w => w.ImieNazwisko).ToList();

    }

    private void Button_Usun_Clicked(object sender, EventArgs e)
    {

        WczytajUsunZawodnikow();
        var wybranyZawodnik = DeletePicker.SelectedItem?.ToString();
        if (wybranyZawodnik != null)
        {
            var zawodnik = _zawodnicy.FirstOrDefault(z => z.ImieNazwisko == wybranyZawodnik);
            if (zawodnik != null)
            {
                _dbService.DeleteZawodnik(zawodnik);
                _zawodnicy.Remove(zawodnik);
                DeletePicker.SelectedItem = null;
            }
        }



    }

    private void ToLawkaSwap_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;


        if (button?.CommandParameter is Zawodnicy zawodnicy)
        {

            if (_zawodnicy.Contains(zawodnicy))
            {
                _zawodnicy.Remove(zawodnicy);
            }


            if (!_zawodnicyLawka.Contains(zawodnicy))
            {
                _zawodnicyLawka.Add(zawodnicy);
            }
        }
    }

    private void ToSkladSwap_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;


        if (button?.CommandParameter is Zawodnicy zawodnicy)
        {

            if (_zawodnicyLawka.Contains(zawodnicy))
            {
                _zawodnicyLawka.Remove(zawodnicy);
            }


            if (!_zawodnicy.Contains(zawodnicy))
            {
                _zawodnicy.Add(zawodnicy);
            }
        }
    }
}