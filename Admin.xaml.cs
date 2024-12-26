namespace Team_Manager;

public partial class Admin : ContentPage
{

    private readonly LocalDbServices _dbService;
    public  Admin(LocalDbServices dbService)
    {
        InitializeComponent();
        _dbService = dbService;
    }

    private async void onWydarzenieButtonClicked(object sender, EventArgs e)
    {
        var dbService = App.Services.GetRequiredService<LocalDbServices>();
        var wydarzeniePage = new AdminPages.Wydarzenie(dbService);
        await Navigation.PushAsync(wydarzeniePage);
    }

    private async void onMeczButtonClicked(object sender, EventArgs e)
    {
        var dbService = App.Services.GetRequiredService<LocalDbServices>();
        var meczPage = new AdminPages.Mecz(dbService);
        await Navigation.PushAsync(meczPage);
    }
}
