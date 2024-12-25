using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific.AppCompat;

namespace Team_Manager;

public partial class Ustawienia : ContentPage
{
	public Ustawienia()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
		await Navigation.PushAsync(new SzablonPostWydarzenia());
    }
}