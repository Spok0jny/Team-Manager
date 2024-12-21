using Team_Manager;

namespace Team_Manager.KlubPages
{
    public partial class Obecnosc : ContentPage
    {
        private readonly LocalDbServices _dbService;

        public List<Zawodnicy> ZawodnicyLista { get; set; }
        public List<Frekwencja> ZawodnicyFrekwencja { get; set; }
        public int WybraneWydarzenieId { get; set; }

        public Obecnosc(LocalDbServices dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            BindingContext = this;
            WczytajZawodnikow();
        }

        private async void WczytajZawodnikow()
        {
    
            ZawodnicyLista = await _dbService.GetZawodnicy();
            var wydarzenia = await _dbService.GetHarmonogram();

         
            wydarzeniePicker.ItemsSource = wydarzenia.Select(w => w.Typ).ToList();
        }

        private async void WydarzeniePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var wybraneWydarzenieIndex = wydarzeniePicker.SelectedIndex;
            if (wybraneWydarzenieIndex == -1)
                return;

            var wydarzenia = await _dbService.GetHarmonogram();
            var wybraneWydarzenie = wydarzenia[wybraneWydarzenieIndex];

            WybraneWydarzenieId = wybraneWydarzenie.Id;

            
            ZawodnicyFrekwencja = new List<Frekwencja>();

            foreach (var zawodnik in ZawodnicyLista)
            {
               
                var frekwencja = await _dbService.GetFrekwencjaByZawodnikAndHarmonogram(zawodnik.ImieNazwisko, WybraneWydarzenieId);
                ZawodnicyFrekwencja.Add(frekwencja ?? new Frekwencja
                {
                    ZawodnikImieNazwisko = zawodnik.ImieNazwisko,  
                    HarmonogramId = WybraneWydarzenieId,
                    CzyObecny = false  
                });
            }


            zawodnicyCollectionView.ItemsSource = ZawodnicyFrekwencja;
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var switchControl = (Switch)sender;
            var frekwencja = (Frekwencja)switchControl.BindingContext;
            frekwencja.CzyObecny = switchControl.IsToggled;

        
            await _dbService.CreateFrekwencja(frekwencja);
        }
    }
}
