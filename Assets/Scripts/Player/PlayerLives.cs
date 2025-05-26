using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLives : MonoBehaviour
{
    public event Action<PlayerLives> OnGameOver;
    
    [SerializeField] private Image[] playerLives;
    private int lives = 4;


    public void PlayerHit()
    {
        lives--;
        for (int i = 0; i < playerLives.Length; i++)
        {
            playerLives[i].enabled = i < lives;
        }

        if (lives <= 0)
        {
            OnGameOver?.Invoke(this);
            Destroy(gameObject);
        }
        
    }
}
