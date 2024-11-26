using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace MobileFront;

public partial class RegisterPage : ContentPage
{
    private readonly string _registerEndpoint = "http://10.0.2.2:8002/register";

    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        var username = UsernameEntry.Text?.Trim();
        var password = PasswordEntry.Text?.Trim();
        var confPassword = ConfPasswordEntry.Text?.Trim();

        // Валидация полей
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confPassword))
        {
            ShowErrorMessage("Please fill in all fields.");
            return;
        }

        if (password != confPassword)
        {
            ShowErrorMessage("Passwords do not match.");
            return;
        }

        try
        {
            var isRegistered = await RegisterAsync(username, password, confPassword);
            if (isRegistered)
            {
                await DisplayAlert("Success", "Registration successful. You can now log in.", "OK");
                await Navigation.PopAsync(); // Возвращаемся на страницу логина
            }
            else
            {
                ShowErrorMessage("Registration failed. Please try again.");
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"An error occurred: {ex.Message}");
        }
    }

    private async Task<bool> RegisterAsync(string username, string password, string confPassword)
    {
        using var client = new HttpClient();

        var registerData = new
        {
            username,
            password,
            confPassword
        };

        var json = JsonSerializer.Serialize(registerData);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await client.PostAsync(_registerEndpoint, content);

        return response.IsSuccessStatusCode;
    }

    private void ShowErrorMessage(string message)
    {
        ErrorMessageLabel.Text = message;
        ErrorMessageLabel.IsVisible = true;
    }



}
