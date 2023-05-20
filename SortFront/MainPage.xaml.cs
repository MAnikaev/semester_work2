using CsvHelper;
using System.Globalization;
using System.Xml.Serialization;

namespace SortFront
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void GoToCalculatePage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChooseSettingsPage());
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

        private async void GoToInformationPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GuidePage());
        }
    }
}