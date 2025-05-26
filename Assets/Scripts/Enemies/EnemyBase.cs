using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Abstract class that serves as the foundation for all enemy types in the game. Implements IDamagable
/// and provides core functionality for enemy movement, health management, damage handling and destruction.
/// </summary>
public abstract class EnemyBase : MonoBehaviour, IDamagable
{

    /// <summary>
    /// Triggered when the enemy is destroyed. The boolean parameter indicates whether
    /// the enemy was destroyed by the player (true)
    /// or by other means (false).
    /// </summary>
    public event Action<EnemyBase, bool> OnEnemyDestroyed;

    /// <summary>
    /// Configuration settings used to define properties and behavior of an enemy,
    /// including health, speed and scoring parameters. This ScriptableObject
    /// provides customizable values used by enemy instances during initialization.
    /// </summary>
    [Tooltip("Reference to the scriptable object containing configuration data for this enemy type")]
    protected EnemyConfigSO config;

    
    
    protected float MovementSpeed { get; set; }
    protected int CurrentLane { get; set; }
    protected int Score { get; set; }
    
    protected int CurrentHealth { get; set; }
    public int Health => CurrentHealth;
    
    protected IMovementBehavior movementBehavior;
    protected IShootingBehavior shootingBehavior;

    
    protected Rigidbody2D rb;
    protected EnemyUI enemyUI;
    protected SpriteRenderer sp;
    protected float baseY;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        enemyUI = GetComponent<EnemyUI>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }
    


    /// <summary>
    /// Initializes the enemy with specific lane and direction settings. Configures movement speed, score,
    /// health and other basic parameters essential for enemy behavior.
    /// </summary>
    /// <param name="lane">The lane in which the enemy is spawned.</param>
    /// <param name="spawnFromLeft">Indicates whether the enemy spawns from the left side of the screen.</param>
    public virtual void Initialize(int lane, bool spawnFromLeft)
    {
        Debug.Log(config.enemyName);
        MovementSpeed = Random.Range(config.minSpeed, config.maxSpeed);
        MovementSpeed = spawnFromLeft ? Mathf.Abs(MovementSpeed) : -Mathf.Abs(MovementSpeed);
        CurrentHealth = config.health;
        Score = config.baseScore;
        CurrentLane = lane;

        if (config.enemySprite && sp)
        {
            sp.sprite = config.enemySprite;
        }

        if (sp)
        {
            sp.flipX = spawnFromLeft;
        }
        
        SetupBehaviors();
        
        enemyUI?.SetScore(Score);
    }
    
    protected virtual void SetupBehaviors()
    {
        // Set up movement behavior based on config type
        switch (config.behaviorType)
        {
            case EnemyBehaviorType.Wobbler:
                //movementBehavior = new WobblyMovement(config.wobbleAmplitude, config.wobbleFrequency, baseY);
                break;
            case EnemyBehaviorType.StopAndShoot:
                //movementBehavior = new StopAndShootMovement(MovementSpeed, config.fireRate, config.stopDuration);
                if (config.canShoot)
                {
                    //shootingBehavior = new BasicShooting(config.bulletDamage, config.bulletSpeed);
                }
                break;
            case EnemyBehaviorType.Basic:
            default:
                movementBehavior = new StraightMovement();
                break;
        }
    }


    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    protected virtual void Update()
    {
        // Handle shooting behavior
    }

    protected virtual void HandleMovement()
    {
        // Handle movement behavior
        
        // This will be the default fallback
        Vector2 moveDelta = new Vector2(1, 0) * (MovementSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + moveDelta);
    }
    
    public virtual int CalculateScore()
    {
        int baseScore = config.baseScore;
        int laneBonus = CurrentLane + 2;
        int speedBonus = Mathf.RoundToInt(Mathf.Abs(MovementSpeed) * config.speedBonusMultiplier);
        return baseScore + laneBonus + speedBonus;
    }

    public void DestroyTarget(bool diedByPlayer)
    {
        OnEnemyDestroyed?.Invoke(this, diedByPlayer);
        HandleDestruction();
    }

    protected virtual void HandleDestruction()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        HandleDamage(damage);
        if (CurrentHealth <= 0)
        {
            DestroyTarget(true);
        }
    }
    // Allow subclasses to add damage effects/visuals
    protected virtual void HandleDamage(int damageAmount)
    {
        // Base implementation does nothing, but subclasses can override
    }
    
    // Method to register for destruction events from outside (safer than direct event subscription)
    public void RegisterDestructionHandler(Action<EnemyBase, bool> handler)
    {
        OnEnemyDestroyed += handler;
    }
    
    public void UnregisterDestructionHandler(Action<EnemyBase, bool> handler)
    {
        OnEnemyDestroyed -= handler;
    }


    public virtual int PickLane(int maxLanes)
    {
        // Use preferred lane from config if valid, otherwise pick at random
        if (config && config.preferredLane >= 0 && config.preferredLane < maxLanes)
        {
            return config.preferredLane;
        }
        
        return Random.Range(0, maxLanes);

    }

    public void SetConfiguration(EnemyConfigSO newConfig)
    {
        config = newConfig;
    }


}
