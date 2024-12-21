using Microsoft.Maui.Controls;

namespace Team_Manager
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Navigated += OnShellNavigated;
        }

        private void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
  
            if (e.Current.Location.OriginalString.Contains("Klub"))
            {
              
                FlyoutBehavior = FlyoutBehavior.Flyout;
                FlyoutMenu.IsVisible = true;
            }
            else
            {
            
                FlyoutBehavior = FlyoutBehavior.Disabled;
                FlyoutMenu.IsVisible = false;
            }
        }


        private async void OnOsiagnieciaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KlubPages.Osiagniecia());
        }
        private async void OnListaZawodnikowClicked(object sender, EventArgs e)
        {
            //stad brac nawigacja jak nie dziala
            //daniel kocham cie za to<33
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
    }
}
