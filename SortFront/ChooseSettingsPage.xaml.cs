using SortRequirement;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SortFront;

public partial class ChooseSettingsPage : ContentPage
{
	public ChooseSettingsPage()
	{
		InitializeComponent();
	}

	private async void GoToResultPage(object sender, EventArgs e)
	{
        if (AppData.UserArray != null && AppData.Solver != null)
            await Navigation.PushAsync(new ResultPage());
        else
            await DisplayAlert("Не все файлы выбраны", "Выберите все файлы", "ОК");
	}

	private async void ChooseArray(object sender, EventArgs e)
	{
        try
        {
            FileResult result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, new[] { ".csv" } },
                    })
            });

            var content = await File.ReadAllTextAsync(result.FullPath);
            AppData.UserArray = ParseCsvToArray(content.Substring(0, content.Length - 2));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Неверные данные", "Выберите корректный файл", "ОК");
        }
    }

    private int[] ParseCsvToArray(string csvString)
    {
        try
        {
            var array = new List<int>();
            foreach (var elem in csvString.Split(','))
            {
                array.Add(int.Parse(elem));
            }

            return array.ToArray();
        }
        catch (Exception ex)
        {
            DisplayAlert("Некорректные данные", "Выберите другой файл", "ОК");
            return null;
        }
    }

    private async void ChooseAssembly(object sender, EventArgs e)
	{
        try
        {
            FileResult result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.WinUI, new[] { ".dll" } },
                    })
            });

            var assembly = Assembly.LoadFrom(result.FullPath);
            var implementation = assembly.GetTypes()
                                    .FirstOrDefault(t => typeof(IArraySorter).IsAssignableFrom(t) && !t.IsInterface);
            if (implementation == null)
            {
                await DisplayAlert("Неверная сборка", "Выберите другой файл", "ОК");
                return;
            }
            AppData.Solver = Activator.CreateInstance(implementation) as IArraySorter;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Неверные данные", "Выберите корректный файл", "ОК");
            return;
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