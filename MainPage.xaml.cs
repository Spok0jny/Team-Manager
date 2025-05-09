using System;
using Team_Manager.ViewModels;

namespace Team_Manager;

public partial class MainPage : ContentPage
{
    private readonly LocalDbServices _dbService;

    public WydarzeniaViewModel ViewModel { get; }

    public MainPage(LocalDbServices dbService)
    {
        InitializeComponent();
        _dbService = dbService;
        ViewModel = new WydarzeniaViewModel(dbService);
        BindingContext = ViewModel;
    }

    private async void Button_Wszystkie_Wydarzenia_Clicked(object sender, EventArgs e)
    {
      
        await Navigation.PushAsync(new WszystkieWydarzenia(_dbService));
    }
}
