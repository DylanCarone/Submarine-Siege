using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Config")]
public class EnemyConfigSO : ScriptableObject
{

    [Header("Basic Stats")] public string enemyName = "Enemy";
    /// <summary>
    /// Represents the health of an enemy. This value determines how much damage
    /// the enemy can take before being destroyed. The initial value is
    /// configured via the associated EnemyConfigSO and can be modified based on game logic.
    /// </summary>
    public int health = 1;

    /// <summary>
    /// Defines the minimum movement speed for an enemy. This value is specified in
    /// the enemy's configuration and determines the lower bound for the random
    /// speed range assigned during initialization.
    /// </summary>
    public float minSpeed = 1f;

    /// <summary>
    /// Specifies the maximum movement speed for an enemy. This value sets the upper limit for
    /// the random speed range assigned to an enemy during initialization. It is defined in the
    /// associated EnemyConfigSO and can influence the enemy's behavior and difficulty.
    /// </summary>
    public float maxSpeed = 3f;

    /// <summary>
    /// Represents the base score awarded for defeating this enemy. The base score is defined
    /// in the associated EnemyConfigSO and can be used to calculate the total score by combining
    /// other factors such as lane position and movement speed.
    /// </summary>
    public int baseScore = 10;

    /// <summary>
    /// Represents the multiplier applied to the speed of an enemy to calculate the speed-based score bonus.
    /// This value is defined in the configuration data and determines how significant
    /// the enemy's movement speed is in contributing to the overall score calculation.
    /// </summary>
    public int speedBonusMultiplier = 5;
    
    public Sprite enemySprite;


    [Header("Behavior Type")]
    public EnemyBehaviorType behaviorType = EnemyBehaviorType.Basic;

    [Header("Shooting Parameters")] public bool canShoot = false;
    public float fireRate = 2f;
    public int bulletDamage = 1;
    public float bulletSpeed = 5f;
    
    [Header("Preferred Lane")]
    [Range(-1,3)] [Tooltip("Set to -1 for no preference.")]
    public int preferredLane = -1; 
    


}


public enum EnemyBehaviorType
{
    Basic,          // Simple movement
    Wobbler,        // Wave-like movement
    StopAndShoot,   // Stops to fire at player
    Custom          // For specialized behaviors
}
