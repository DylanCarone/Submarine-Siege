using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerLives player;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private SubmitScoreUI scoreUI;

    private void OnEnable()
    {
        player.OnGameOver += EndGame;
    }

    private void OnDisable()
    {
        player.OnGameOver -= EndGame;
    }

    private void EndGame(PlayerLives player)
    {
        scoreUI.Show(levelManager.Score);
    }


    
}
