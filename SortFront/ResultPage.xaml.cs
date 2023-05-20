using SortRequirement;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SortFront;

public partial class ResultPage : ContentPage
{
	public ResultPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Run(() =>
        {
            var array = AppData.UserArray;
               
            AppData.ResultArray = AppData.Solver.Sort(array);
        });
    }
    private void RefreshResult(object sender, EventArgs e)
    {
        if(AppData.ResultArray != null)
        {
            RefreshTitle.IsVisible = false;
            RefreshButton.IsVisible = false;
            WellDone.IsVisible = true;
            GetFileButton.IsVisible = true;
            PathToFileField.IsVisible = true;
        }
        else
        {
            RefreshTitle.Text = "Проводятся вычисления";
        }
    }

    private async void GetResultArray(object sender, EventArgs e)
    {
        try
        {
            string filePath = PathToFileField.Text + "SortedArray.csv";

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine(string.Join(",", AppData.ResultArray));
            }
            PathToFileField.IsVisible = false;
            GetFileButton.IsVisible = false;
            Congratulations.IsVisible = true;
        }
        catch(Exception ex)
        {
            await DisplayAlert("Неверный путь", "Выберите другой путь", "ОК");
        }
    }
    private void SwitchTheme(object sender, EventArgs e)
    {
        var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
        if (mergedDictionaries != null && AppData.IsDarkTheme)
        {
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new LightTheme());
            AppData.IsDarkTheme = false;
        }
        else
        {
            mergedDictionaries.Clear();
            mergedDictionaries.Add(new DarkTheme());
            AppData.IsDarkTheme = true;
        }
    }
}