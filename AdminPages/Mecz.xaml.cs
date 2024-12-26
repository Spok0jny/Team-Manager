namespace Team_Manager.AdminPages;

public partial class Mecz : ContentPage
{
    private readonly LocalDbServices _dbService;
    public List<Zawodnicy> ZawodnicyLista { get; set; }
    public int WybranyZawodnikId { get; set; }

    private int golePierwsza = 0;
    private int goleDruga = 0;

    



    public Mecz(LocalDbServices dbService)
	{
		InitializeComponent();
        _dbService = dbService;
        WczytajZawodnikow();
    }

    private async void WczytajZawodnikow()
    {
        ZawodnicyLista = await _dbService.GetZawodnicy();
        leftZawodnikPicker.ItemsSource = ZawodnicyLista.Select(z => z.ImieNazwisko).ToList();
        rightZawodnikPicker.ItemsSource = ZawodnicyLista.Select(z => z.ImieNazwisko).ToList();
    }

    private void OnLeftZawodnikPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (leftZawodnikPicker.SelectedIndex != -1)
        {
            WybranyZawodnikId = ZawodnicyLista[leftZawodnikPicker.SelectedIndex].Id;
        }
    }
    
    private void OnRightZawodnikPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (rightZawodnikPicker.SelectedIndex != -1)
        {
            WybranyZawodnikId = ZawodnicyLista[rightZawodnikPicker.SelectedIndex].Id;
        }
    }
    private async void OnAddEventClicked(object sender, EventArgs e)
    {
        try
        {
            
            var mecz = new Mecze
            {
                druzyna1 = druzynaPierwszaEntry.Text,
                druzyna2 = druzynaDrugaEntry.Text,
                dataMeczu = DataEntry.Date
            };

            await _dbService.CreateMecz(mecz);

            await DisplayAlert("Success", "Match added successfully!", "OK");


            HardResetForm();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add match: {ex.Message}", "OK");
        }
    }

    private void SoftResetForm()
    {
        leftZawodnikName.Text = string.Empty;
        rightZawodnikName.Text = string.Empty;
        leftMinuteEntry.Text = string.Empty;
        rightMinuteEntry.Text = string.Empty;
    }

    private void HardResetForm()
    {
        druzynaPierwszaEntry.Text = string.Empty;
        druzynaDrugaEntry.Text = string.Empty;
        DataEntry.Date = DateTime.Now;
        leftZawodnikName.Text = string.Empty;
        rightZawodnikName.Text = string.Empty;
        leftMinuteEntry.Text = string.Empty;
        rightMinuteEntry.Text = string.Empty;
    }

    private void leftCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        if (leftCheckBox.IsChecked)
        {
            druzynaPierwszaEntry.Text = "Moja druzyna"; //Mozna ustawic nazwe swojej druzyny w ustawieniach chyba wsm i tam to gdzies zapisac do wymyslenia ogolem
            druzynaPierwszaEntry.IsEnabled = false;
            leftZawodnikName.IsVisible = false;
            leftZawodnikPicker.IsVisible = true;
            rightCheckBox.IsEnabled = false;
        }
        else
        {
            druzynaPierwszaEntry.Text = ""; 
            druzynaPierwszaEntry.IsEnabled = true;
            leftZawodnikName.IsVisible = true;
            leftZawodnikPicker.IsVisible = false;
            rightCheckBox.IsEnabled = true;
        }
    }

    private void rightCheckBoxChanged(object sender, CheckedChangedEventArgs e)
    {
        if (rightCheckBox.IsChecked)
        {
            druzynaDrugaEntry.Text = "Moja druzyna";
            druzynaDrugaEntry.IsEnabled = false;
            rightZawodnikName.IsVisible = false;
            rightZawodnikPicker.IsVisible = true;
            leftCheckBox.IsEnabled = false;
        }
        else
        {
            druzynaDrugaEntry.Text = "";
            druzynaDrugaEntry.IsEnabled = true;
            rightZawodnikName.IsVisible = true;
            rightZawodnikPicker.IsVisible = false;
            leftCheckBox.IsEnabled = true;
        }
    }

    private void leftAddButton(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(leftMinuteEntry.Text))
        {
            DisplayAlert("B³¹d", "Wpisz poprawn¹ minutê.", "OK");
            return; 
        }

       
        if (leftCheckBox.IsChecked == true)
        {
            
            if (leftZawodnikPicker.SelectedIndex == -1)
            {
                DisplayAlert("B³¹d", "Musisz wybraæ zawodnika z listy.", "OK");
                return; 
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(leftZawodnikName.Text))
            {
                DisplayAlert("B³¹d", "Musisz wpisaæ imiê i nazwisko zawodnika.", "OK");
                return; 
            }
        }

        if (leftGoalRadioButton.IsChecked == true)
        {
            leftHandleGoal();
            SoftResetForm();
        }
        else if (leftAsistRadioButton.IsChecked == true)
        {
            leftHandleAsist();
            SoftResetForm();
        }
        else if (leftYellowCardRadioButton.IsChecked == true)
        {
            leftHandleYellowCard();
            SoftResetForm();
        }
        else if (leftRedCardRadioButton.IsChecked == true)
        {
            leftHandleRedCard();
            SoftResetForm();
        }
        else
        {
            DisplayAlert("B³¹d", "Nie wybrano ¿adnej akcji!", "OK");
        }
    }

    //Ogolnie druzyna 1 to ta po lewej, druzyna 2 to ta po prawej okokok
    private async void leftHandleGoal()
    {
        var idMeczuu = _dbService.GetCountMecze();
        golePierwsza += 1;
        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Goal",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);

            await _dbService.UpdateBramki(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Goal",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }
        
    }
    private async void leftHandleAsist()
    {
        var idMeczuu = _dbService.GetCountMecze();

        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Asysta",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);
            await _dbService.UpdateAsysty(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Asysta",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }
    }
    private async void leftHandleYellowCard()
    {
        var idMeczuu = _dbService.GetCountMecze();

        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Zolta kartka",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);
            await _dbService.UpdateZolteKartki(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Zolta kartka",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }
    }
    private async void leftHandleRedCard()
    {
        var idMeczuu = _dbService.GetCountMecze();

        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Czerwona kartka",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);
            await _dbService.UpdateCzerwoneKartki(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 1,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Czerwona kartka",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }
    }

    private void rightAddButton(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(rightMinuteEntry.Text))
        {
            DisplayAlert("B³¹d", "Wpisz poprawn¹ minutê.", "OK");
            return;
        }


        if (rightCheckBox.IsChecked == true)
        {

            if (rightZawodnikPicker.SelectedIndex == -1)
            {
                DisplayAlert("B³¹d", "Musisz wybraæ zawodnika z listy.", "OK");
                return;
            }
        }
        else
        {
            if (string.IsNullOrWhiteSpace(rightZawodnikName.Text))
            {
                DisplayAlert("B³¹d", "Musisz wpisaæ imiê i nazwisko zawodnika.", "OK");
                return;
            }
        }

        if (rightGoalRadioButton.IsChecked == true)
        {
            rightHandleGoal();
            SoftResetForm();
        }
        else if (rightAsistRadioButton.IsChecked == true)
        {
            rightHandleAsist();
            SoftResetForm();
        }
        else if (rightYellowCardRadioButton.IsChecked == true)
        {
            rightHandleYellowCard();
            SoftResetForm();
        }
        else if (rightRedCardRadioButton.IsChecked == true)
        {
            rightHandleRedCard();
            SoftResetForm();
        }
        else
        {
            DisplayAlert("B³¹d", "Nie wybrano ¿adnej akcji!", "OK");
        }
    }


    private async void rightHandleGoal()
    {
        var idMeczuu = _dbService.GetCountMecze();
        goleDruga += 1;
        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Goal",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);

            await _dbService.UpdateBramki(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Goal",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }

    }
    private async void rightHandleAsist()
    {
        var idMeczuu = _dbService.GetCountMecze();

        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Asysta",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);
            await _dbService.UpdateAsysty(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Asysta",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }
    }
    private async void rightHandleYellowCard()
    {
        var idMeczuu = _dbService.GetCountMecze();

        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Zolta kartka",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);
            await _dbService.UpdateZolteKartki(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Zolta kartka",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }
    }
    private async void rightHandleRedCard()
    {
        var idMeczuu = _dbService.GetCountMecze();

        if (leftCheckBox.IsChecked == true)
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Czerwona kartka",
                idZawodnika = WybranyZawodnikId
            };

            await _dbService.CreateAction(nowaAkcja);
            await _dbService.UpdateCzerwoneKartki(WybranyZawodnikId, 1);
        }
        else
        {
            var nowaAkcja = new przebiegMeczu
            {
                idMeczu = int.Parse(idMeczuu.ToString()),
                ktoraDruzyna = 2,
                ktoraMinuta = int.Parse(leftMinuteEntry.Text),
                akcja = "Czerwona kartka",
                nazwaZawodnika = leftZawodnikName.Text
            };

            await _dbService.CreateAction(nowaAkcja);
        }
    }
}