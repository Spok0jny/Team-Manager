namespace Team_Manager;

public partial class Klub : ContentPage
{
	private readonly LocalDbServices _dbService;
	public Klub(LocalDbServices dbService)
	{
		InitializeComponent();
		_dbService = dbService;

	}
}