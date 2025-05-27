using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Config")]
public class EnemyConfigSO : ScriptableObject
{

    [Header("Basic Stats")]
    [Tooltip("Represents the name of the enemy")]
    public string enemyName = "Enemy";
    
    [Tooltip("Represents the health of an enemy. This value determines how much damage the enemy can take before being destroyed.")]
    public int health = 1;

    [Tooltip("Defines the minimum movement speed for an enemy. Determines the lower bound for the random speed range assigned during initialization.")]
    public float minSpeed = 1f;

    [Tooltip("Specifies the maximum movement speed for an enemy. Sets the upper limit for the random speed range assigned during initialization.")]
    public float maxSpeed = 3f;

    [Tooltip("Represents the base score awarded for defeating this enemy. Can be combined with other factors like lane position and movement speed.")]
    public int baseScore = 10;

    [Tooltip("Multiplier applied to the enemy's speed to calculate the speed-based score bonus. Determines how speed affects the total score.")]
    public int speedBonusMultiplier = 5;
    
    [Header("Sprite")]
    [Tooltip("The visual representation of the enemy. Assign a sprite asset to define how the enemy appears in the game.")]
    public Sprite enemySprite;

    [Tooltip("The color tint applied to the enemy sprite. Default is white (no tint).")]
    public Color enemySpriteColor = new Color(1,1,1,1);

    [Header("Behavior Type")]
    [Tooltip("Determines how the enemy moves and acts. Options are only Basic for now")]
    public EnemyBehaviorType behaviorType = EnemyBehaviorType.Basic;

    
    [Tooltip("Determines whether this enemy can shoot projectiles.")]
    [HideInInspector] public bool canShoot = false;
    
    [Tooltip("How frequently the enemy fires projectiles, measured in seconds between shots.")]
    [HideInInspector] public float fireRate = 2f;
    
    [Tooltip("Amount of damage each bullet deals to the player.")]
    [HideInInspector] public int bulletDamage = 1;
    
    [Tooltip("How fast enemy projectiles travel, measured in units per second.")]
    [HideInInspector] public float bulletSpeed = 5f;
    
    
    [Range(-1,3)] 
    [Tooltip("Set to -1 for no preference. Values 0-3 indicate the specific lane this enemy prefers to spawn in.")]
    [HideInInspector] public int preferredLane = -1; 
}

public enum EnemyBehaviorType
{
    Basic          // Simple movement
}