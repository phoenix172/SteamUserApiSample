using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = "__API__KEY";
        string userVanityName = "phoenix172"; // This can be a vanity name or a SteamID64
        string gameId = "1444480"; // Replace with the actual game ID

        var steamClient = new SteamClient(apiKey);

        string? userId = await steamClient.GetSteamID64Async(userVanityName);
        if (string.IsNullOrEmpty(userId))
        {
            Console.WriteLine("Failed to retrieve SteamID64.");
            return;
        }

        var ownsGame = await steamClient.CheckUserOwnsGameAsync(userId, gameId);

        Console.WriteLine(ownsGame ? "User owns Turing Complete." : "User does not own Turing Complete.");
    }
}
