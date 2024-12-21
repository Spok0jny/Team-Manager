using Team_Manager;  

namespace Team_Manager.KlubPages
{
    public partial class Harmonogram : ContentPage
    {
        private readonly LocalDbServices _dbService;

        public List<Team_Manager.Harmonogram> DniTygodnia { get; set; }  

        public Harmonogram(LocalDbServices dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            BindingContext = this;  
            _ = WczytajHarmonogram();  
        }

        public Harmonogram()
        {
        }

  
        private async Task WczytajHarmonogram()
        {
          
            var harmonogram = await _dbService.GetHarmonogram();

            DniTygodnia = harmonogram;  
            KalendarzView.ItemsSource = DniTygodnia;  
        }

       
        private async void DodajWydarzenieButton_Clicked(object sender, EventArgs e)
        {
            
            if (dataPicker.Date == null || string.IsNullOrEmpty(typEntry.Text))
            {
                await DisplayAlert("B³¹d", "Proszê wype³niæ wszystkie pola", "OK");
                return;
            }

         
            var noweWydarzenie = new Team_Manager.Harmonogram
            {
                Data = dataPicker.Date.ToString("yyyy-MM-dd"),  
                Typ = typEntry.Text  
            };

           
            await _dbService.CreateHarmonogram(noweWydarzenie);

           
            typEntry.Text = "";
            dataPicker.Date = DateTime.Now;

            
            await WczytajHarmonogram();
        }
    }
}
