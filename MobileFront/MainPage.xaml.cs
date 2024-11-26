using Microsoft.Maui.Controls;

namespace MobileFront;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        CheckAuthentication();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CheckAuthentication(); 
    }

    private async void CheckAuthentication()
    {
        Preferences.Set("AuthToken", string.Empty);
        var token = Preferences.Get("AuthToken", string.Empty);

        if (string.IsNullOrEmpty(token))
        {
            await Navigation.PushAsync(new LoginPage());
        }
        else
        {
            await Navigation.PushAsync(new SearchPage());
        }

        Navigation.RemovePage(this);
    }
}
