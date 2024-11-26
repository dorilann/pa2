using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Maui.Controls;

namespace MobileFront;

public partial class LoginPage : ContentPage
{
    private readonly string _loginEndpoint = "http://10.0.2.2:8002/login";

    public LoginPage()
    {
        InitializeComponent();
        UsernameEntry.Text = "string";
        PasswordEntry.Text = "string";
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        // Считываем данные из полей ввода
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please fill in both fields.");
            return;
        }

        try
        {
            var token = await AuthenticateAsync(username, password);
            if (!string.IsNullOrEmpty(token))
            {
                // Сохраняем токен
                Preferences.Set("AuthToken", token);

                // Переход к следующей странице (например, SearchPage)
                await Navigation.PushAsync(new SearchPage());
            }
            else
            {
                ShowErrorMessage("Invalid credentials. Please try again.");
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"An error occurred: {ex.Message}");
        }
    }

    private async Task<string> AuthenticateAsync(string username, string password)
    {
        using var client = new HttpClient();
        var content = new StringContent(JsonSerializer.Serialize(new { username, password }), Encoding.UTF8, "application/json");

        var response = await client.PostAsync(_loginEndpoint, content);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);
            return jsonResponse?.Token;
        }

        return null;
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }


    private void ShowErrorMessage(string message)
    {
        ErrorMessageLabel.Text = message;
        ErrorMessageLabel.IsVisible = true;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false);
    }

    private class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
