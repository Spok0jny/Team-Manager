namespace Team_Manager.AdminPages;

public partial class Mecz : ContentPage
{
    private readonly LocalDbServices _dbService;
    public List<Zawodnicy> ZawodnicyLista { get; set; }
    public int WybranyZawodnikId { get; set; }



    public Mecz(LocalDbServices dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        WczytajZawodnikow();
    }

    private async void WczytajZawodnikow()
    {
        ZawodnicyLista = await _dbService.GetZawodnicy();
        zawodnikPicker.ItemsSource = ZawodnicyLista.Select(z => z.ImieNazwisko).ToList();
    }

    private void OnZawodnikPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (zawodnikPicker.SelectedIndex != -1)
        {
            WybranyZawodnikId = ZawodnicyLista[zawodnikPicker.SelectedIndex].Id;
        }
    }
    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        try
        {
            
            var mecz = new Mecze
            {
                druzyna1 = druzynaPierwszaEntry.Text,
                druzyna2 = druzynaDrugaEntry.Text,
                dataMeczu = DataEntry.Date
            };

            await _dbService.CreateMecz(mecz);

            await DisplayAlert("Success", "Event added successfully!", "OK");


            ResetForm();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add event: {ex.Message}", "OK");
        }
    }

    private void ResetForm()
    {
        DataEntry.Date = DateTime.Now;
        druzynaPierwszaEntry.Text = string.Empty;
        druzynaDrugaEntry.Text = string.Empty;
    }
}