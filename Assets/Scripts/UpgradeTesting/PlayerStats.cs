using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    [SerializeField] PlayerConfig config;
    
    public float MoveSpeed { get; private set; }
    public int MaxHealth { get; private set; }
    public int Coins { get; private set; }



    public void Initialize()
    {
        if (config != null)
        {
            MoveSpeed = config.StartingSpeed;
            MaxHealth = config.StartingHealth;
            Coins = 100;
        }
        else
        {
            Debug.LogWarning("PlayerConfig not assigned to PlayerStats!");
        }
    }
    
    public void GainCoins(int coins)
    {
        Coins += coins;
    }

    public void SpendCoins(int coinsToSpend)
    {
        Coins -= coinsToSpend;
    }
}