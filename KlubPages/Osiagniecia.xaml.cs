using Team_Manager;

namespace Team_Manager.KlubPages
{
    public partial class Osiagniecia : ContentPage
    {
        private readonly LocalDbServices _dbService;

        public List<OsiagnieciaDruzyny> OsiagnieciaDruzyny { get; set; }
        public List<OsiagnieciaZawodnika> OsiagnieciaZawodnika { get; set; }
        public List<Zawodnicy> ZawodnicyLista { get; set; }
        public int WybranyZawodnikId { get; set; }
        public DateTime DataOsiagniecia { get; set; }

        public Osiagniecia(LocalDbServices dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            BindingContext = this;
            WczytajOsiagnieciaDruzyny();
            WczytajZawodnikow();
        }

       
        private async void WczytajOsiagnieciaDruzyny()
        {
            OsiagnieciaDruzyny = await _dbService.GetOsiagnieciaDruzyny();
            OnPropertyChanged(nameof(OsiagnieciaDruzyny)); // Odœwie¿enie CollectionView po wczytaniu danych
        }

        private async void WczytajZawodnikow()
        {
            ZawodnicyLista = await _dbService.GetZawodnicy();
            zawodnikPicker.ItemsSource = ZawodnicyLista.Select(z => z.ImieNazwisko).ToList();
        }

    
        private async void OnZawodnikPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (zawodnikPicker.SelectedIndex != -1)
            {
                WybranyZawodnikId = ZawodnicyLista[zawodnikPicker.SelectedIndex].Id;
                OsiagnieciaZawodnika = await _dbService.GetOsiagnieciaZawodnika(WybranyZawodnikId);
                OnPropertyChanged(nameof(OsiagnieciaZawodnika));  // Odœwie¿enie CollectionView dla osi¹gniêæ zawodnika
            }
        }

  
        private async void OnDodajOsiagniecieClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(noweOsiagniecieEntry.Text))
            {
                await DisplayAlert("B³¹d", "Proszê wpisaæ osi¹gniêcie", "OK");
                return;
            }

            var osiagniecie = new OsiagnieciaDruzyny
            {
                NazwaOsiagniecia = noweOsiagniecieEntry.Text,
                DataOsiagniecia = dataOsiagnieciaPicker.Date 
            };

            await _dbService.CreateOsiagniecieDruzyny(osiagniecie);

       
            WczytajOsiagnieciaDruzyny();
        }

       
        private async void OnDodajZawodnikOsiagniecieClicked(object sender, EventArgs e)
        {
            if (WybranyZawodnikId == 0)
            {
                await DisplayAlert("B³¹d", "Proszê wybraæ zawodnika", "OK");
                return;
            }

            if (string.IsNullOrEmpty(zawodnikOsiagniecieEntry.Text))
            {
                await DisplayAlert("B³¹d", "Proszê wpisaæ osi¹gniêcie zawodnika", "OK");
                return;
            }

            var osiagniecie = new OsiagnieciaZawodnika
            {
                ZawodnikId = WybranyZawodnikId,
                NazwaOsiagniecia = zawodnikOsiagniecieEntry.Text,
                DataOsiagniecia = zawodnikDataOsiagnieciaPicker.Date 
            };

            await _dbService.CreateOsiagniecieZawodnika(osiagniecie);

           
            OnZawodnikPickerSelectedIndexChanged(null, null); // odswiezenie osiagniec zawodnika
        }
    }
}
