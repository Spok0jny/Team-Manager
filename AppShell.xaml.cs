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
            await Navigation.PushAsync(new KlubPages.ListaZawodnikow());
        }
        private async void OnHarmonogramClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KlubPages.Harmonogram());
        }
        private async void OnObecnoscClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new KlubPages.Obecnosc());
        }
    }
}
