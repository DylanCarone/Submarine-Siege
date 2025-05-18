using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using System.Threading.Tasks;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    public string leaderboardID = "Submarine_Siege";
    public int testScore = 1234;
    public string playerName = "Name";

    async void Start()
    {
        await UnityServices.InitializeAsync();
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            UploadScore(testScore);
        }
    }

    async void UploadScore(int score)
    {
        try
        {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardID, score);

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to upload score: {e.Message}");
        }
    }
}
