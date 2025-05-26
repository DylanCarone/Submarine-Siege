using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine.SceneManagement;


public class SubmitScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button submitButton;

    private int playerScore;
    async void Start()
    {
        uiPanel.SetActive(false);

        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        
        submitButton.onClick.AddListener(OnSubmitScore);
    }

    public void Show(int score)
    {
        playerScore = score;
        uiPanel.SetActive(true);
    }

    async void OnSubmitScore()
    {
        string playerName = nameInputField.text;

        try
        {
            var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(
                "Submarine_Siege",
                playerScore);
            uiPanel.SetActive(false);
            SceneManager.LoadScene(0);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to submit score: {e.Message}");
        }
    }
}
