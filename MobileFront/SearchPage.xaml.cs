using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace MobileFront;

public partial class SearchPage : ContentPage, INotifyPropertyChanged
{
    private readonly string _baseEndpoint = "http://10.0.2.2:8002/account";

    

    private bool _isSearchButtonVisible;

    public bool IsSearchButtonVisible
    {
        get => _isSearchButtonVisible;
        set
        {
            _isSearchButtonVisible = value;
            OnPropertyChanged();
        }
    }

    public SearchPage()
    {
        InitializeComponent();
        RegionEntry.Text = "europe";
        ServerEntry.Text = "ru";
        GameNameEntry.Text = "Snowball";
        TagEntry.Text = "RU1";

        BindingContext = this;
    }


    private async void OnSearchClicked(object sender, EventArgs e)
    {
        var region = RegionEntry.Text?.Trim();
        var server = ServerEntry.Text?.Trim();
        var gameName = GameNameEntry.Text?.Trim();
        var tag = TagEntry.Text?.Trim();

        if (string.IsNullOrEmpty(region) || string.IsNullOrEmpty(server) || string.IsNullOrEmpty(gameName) || string.IsNullOrEmpty(tag))
        {
            ShowErrorMessage("Please fill in all fields.");
            return;
        }

        try
        {
            var profile = await FetchProfileAsync(region, server, gameName, tag);
            if (profile != null)
            {
                DisplayProfileInfo(profile);
                ErrorMessageLabel.IsVisible = false;
            }
            else
            {
                ShowErrorMessage("Profile not found.");
            }
        }
        catch (Exception ex)
        {
            ShowErrorMessage($"An error occurred: {ex.Message}");
        }
        UpdateSearchButtonVisibility();
    }


    private async Task<Profile> FetchProfileAsync(string region, string server, string gameName, string tag)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("AuthToken", ""));
        var url = $"{_baseEndpoint}/{region}/{server}/{gameName}/{tag}";

        var response = await client.GetAsync(url);
        response = await client.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Profile>(json);
        }

        return null;
    }

    private void DisplayProfileInfo(Profile profile)
    {
        // Показываем информацию о пользователе
        UserNameLabel.Text = $"{profile.Account.GameName}#{profile.Account.TagLine}";
        TierLabel.Text = $"Tier: {profile.Account.Tier}";
        RankLabel.Text = $"Rank: {profile.Account.Rank}";
        LeaguePointsLabel.Text = $"League Points: {profile.Account.LeaguePoints}";
        WinsLabel.Text = $"Wins: {profile.Account.Wins}";
        LossesLabel.Text = $"Losses: {profile.Account.Losses}";

        // Привязываем список чемпионов
        var championData = profile.Masteries.Select(champion => new ChampionViewModel
        {
            ChampionName = champion.ChampionName,
            ChampionPoints = champion.ChampionPoints,
            ChampionAvatar = $"https://ddragon.leagueoflegends.com/cdn/12.23.1/img/champion/{champion.ChampionName}.png"
        }).ToList();

        ChampionListView.ItemsSource = championData;

        // Показываем элементы
        ProfileInfoLayout.IsVisible = true;
        ChampionListView.IsVisible = true;
        SearchFieldsLayout.IsVisible = false;
        ToggleSearchButton.Text = "Show Search";
        UpdateSearchButtonVisibility();

    }


    private void ShowErrorMessage(string message)
    {
        ErrorMessageLabel.Text = message;
        ErrorMessageLabel.IsVisible = true;
        ChampionListView.IsVisible = false;
        UpdateSearchButtonVisibility();

    }

    private void OnToggleSearchClicked(object sender, EventArgs e)
    {
        // Переключение видимости полей поиска
        SearchFieldsLayout.IsVisible = !SearchFieldsLayout.IsVisible;
        ToggleSearchButton.Text = SearchFieldsLayout.IsVisible ? "Hide Search" : "Show Search";
        UpdateSearchButtonVisibility();

    }

    private void UpdateSearchButtonVisibility()
    {
        IsSearchButtonVisible = ProfileInfoLayout.IsVisible || ChampionListView.IsVisible;
    }



    // Вспомогательный класс для привязки данных к UI
    private class ChampionViewModel
    {
        public string ChampionName { get; set; }
        public int ChampionPoints { get; set; }
        public string ChampionAvatar { get; set; }
    }
}

// Классы для десериализации ответа API
public class Profile
{
    public AccountModel Account { get; set; }
    public List<ChampionMastery> Masteries { get; set; }
}

public class AccountModel
{
    public string Puuid { get; set; }
    public string GameName { get; set; }
    public string TagLine { get; set; }
    public string? AccountId { get; set; }
    public int ProfileIconId { get; set; }
    public long RevisionDate { get; set; }
    public string SummonerId { get; set; }
    public long SummonerLevel { get; set; }
    public string Tier { get; set; }
    public string Rank { get; set; }
    public string LeaguePoints { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
}

public class ChampionMastery
{
    public string PuuId { get; set; }
    public long ChampionPointsUntilNextLevel { get; set; }
    public bool ChestGranted { get; set; }
    public long ChampionId { get; set; }
    public long LastPlayTime { get; set; }
    public int ChampionLevel { get; set; }
    public int ChampionPoints { get; set; }
    public string ChampionName { get; set; }
    public string ChampionNameId { get; set; }
}
