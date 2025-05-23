using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{

    public event Action<EnemyBase, bool> OnEnemyDestroyed; 
    
    [SerializeField] protected EnemyConfigSO config;

    protected float MovementSpeed { get; set; }
    protected int CurrentLane { get; set; }
    protected int Score { get; set; }
    
    protected int CurrentHealth { get; set; }
    public int Health => CurrentHealth;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }


    public virtual void Initialize(int lane, bool spawnFromLeft)
    {
        MovementSpeed = Random.Range(config.minSpeed, config.maxSpeed);
        MovementSpeed = spawnFromLeft ? Mathf.Abs(MovementSpeed) : -Mathf.Abs(MovementSpeed);
        
        Score = config.baseScore;
        
        CurrentHealth = config.health;
        CurrentLane = lane;
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    protected virtual void HandleMovement()
    {
        Vector2 moveDelta = new Vector2(1, 0) * (MovementSpeed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + moveDelta);
    }
    
    public virtual int CalculateScore()
    {
        return config.baseScore;
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



    
}
