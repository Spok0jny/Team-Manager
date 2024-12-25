namespace Team_Manager
{
    public partial class Klub : ContentPage
    {
        private readonly LocalDbServices _dbService;

        public Klub(LocalDbServices dbService)
        {
            InitializeComponent();
            _dbService = dbService;
        }

        
        private async void OnListaZawodnikowClicked(object sender, EventArgs e)
        {

            var dbService = App.Services.GetRequiredService<LocalDbServices>();
            var listaZawodnikowPage = new KlubPages.ListaZawodnikow(dbService);
            await Navigation.PushAsync(listaZawodnikowPage);
        }

        
        private async void OnHarmonogramClicked(object sender, EventArgs e)
        {
            var dbService = App.Services.GetRequiredService<LocalDbServices>();
            var harmonogramPage = new KlubPages.Harmonogram(dbService);
            await Navigation.PushAsync(harmonogramPage);
        }

        private async void OnObecnoscClicked(object sender, EventArgs e)
        {
            var dbService = App.Services.GetRequiredService<LocalDbServices>();
            var obecnoscPage = new KlubPages.Obecnosc(dbService);
            await Navigation.PushAsync(obecnoscPage);
        }

        
        private async void OnOsiagnieciaClicked(object sender, EventArgs e)
        {
           
            var dbService = App.Services.GetRequiredService<LocalDbServices>();
            var osiagnieciaPage = new KlubPages.Osiagniecia(dbService);
            await Navigation.PushAsync(osiagnieciaPage);
           
        }
    }
}
