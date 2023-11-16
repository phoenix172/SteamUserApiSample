class SteamClient
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public SteamClient(string apiKey)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
    }

    public async Task<string?> GetSteamID64Async(string userIdentifier)
    {
        string requestUri = $"http://api.steampowered.com/ISteamUser/ResolveVanityURL/v0001/?key={_apiKey}&vanityurl={userIdentifier}";
        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var template = new { response = new { success = 0, steamid = string.Empty } };
            var result = JsonSerializeExtensions.DeserializeAnonymousType(content, template);

            return result?.response.steamid;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    public async Task<bool> CheckUserOwnsGameAsync(string userId, string gameId)
    {
        string requestUri = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key={_apiKey}&steamid={userId}&format=json";
        try
        {
            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            var template = new { response = new { games = new[] { new { appid = 0 } } } };
            var result = JsonSerializeExtensions.DeserializeAnonymousType(content, template);

            if (result is null) return false;

            foreach (var game in result.response.games)
            {
                if (game.appid == int.Parse(gameId))
                    return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
            return false;
        }
    }
}
