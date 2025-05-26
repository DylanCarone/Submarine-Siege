using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    /// <summary>
    /// Holds the player's configuration data, such as starting speed and health.
    /// This object is necessary for properly initializing the player's stats.
    /// Expected to reference a <see cref="PlayerConfig"/> ScriptableObject.
    /// </summary>
    [SerializeField] PlayerConfig config;
    
    public float MoveSpeed { get; private set; }
    public int MaxHealth { get; private set; }
    public int Coins { get; private set; }


    /// <summary>
    /// Initializes the player's statistics by setting the move speed,
    /// maximum health, and coins based on the associated PlayerConfig.
    /// If the PlayerConfig is not assigned, a warning message will be logged.
    /// </summary>
    public void Initialize()
    {
        if (config != null)
        {
            MoveSpeed = config.StartingSpeed;
            MaxHealth = config.StartingHealth;
            Coins = 0;
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