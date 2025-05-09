using Team_Manager.ViewModels;

namespace Team_Manager;

public partial class WszystkieWydarzenia : ContentPage
{
    public WydarzeniaViewModel ViewModel { get; }

    public WszystkieWydarzenia(LocalDbServices dbService)
    {
        InitializeComponent();
        ViewModel = new WydarzeniaViewModel(dbService);
        BindingContext = ViewModel;
    }
}
