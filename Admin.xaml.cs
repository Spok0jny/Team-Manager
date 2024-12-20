namespace Team_Manager;

public partial class Admin : ContentPage
{
    private readonly LocalDbServices _dbService;
    private byte[] _selectedPhoto; 

    public Admin(LocalDbServices dbService)
    {
        InitializeComponent();
        _dbService = dbService; 
    }

    private async void OnUploadPhotoClicked(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync();

            if (photo != null)
            {
                using var stream = await photo.OpenReadAsync();
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                _selectedPhoto = memoryStream.ToArray(); 
                PhotoImage.Source = ImageSource.FromStream(() => new MemoryStream(_selectedPhoto));

                await DisplayAlert("Success", "Photo uploaded successfully!", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Unable to pick photo: " + ex.Message, "OK");
        }
    }

    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        try
        {
            if (_selectedPhoto == null)
            {
                await DisplayAlert("Error", "Please upload a photo before adding the event.", "OK");
                return;
            }

            var wydarzenie = new Wydarzenia
            {
                dataWydarzenia = DataEntry.Date,
                autor = AutorEntry.Text,
                tytul = TytulWydarzeniaEntry.Text,
                zdjecie = _selectedPhoto 
            };

            await _dbService.CreateWydarzenie(wydarzenie);

            await DisplayAlert("Success", "Event added successfully!", "OK");

        
            ResetForm();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add event: {ex.Message}", "OK");
        }
    }

    private void ResetForm()
    {
        DataEntry.Date = DateTime.Now;
        AutorEntry.Text = string.Empty;
        TytulWydarzeniaEntry.Text = string.Empty;
        PhotoImage.Source = null;
        _selectedPhoto = null;
    }
}
