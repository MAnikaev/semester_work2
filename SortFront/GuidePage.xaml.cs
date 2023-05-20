namespace SortFront;

public partial class GuidePage : ContentPage
{
	public GuidePage()
	{
		InitializeComponent();
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