using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{

    public event Action<EnemyBase, bool> OnEnemyDestroyed; 
    
    [SerializeField] protected EnemyConfigSO config;

    public float Speed { get; protected set; }
    public int Score { get; protected set; }
    public int Lane { get; protected set; }
    public int Health { get; protected set; }

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }


    public virtual void Initialize(int lane, bool spawnFromLeft)
    {
        Speed = Random.Range(config.minSpeed, config.maxSpeed);
        Speed = spawnFromLeft ? Mathf.Abs(Speed) : -Mathf.Abs(Speed);
        Score = config.baseScore;
        Health = config.health;
        Lane = lane;
    }

    protected virtual void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(1, 0) * (Speed * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + moveDelta);
    }

    public virtual int CalculateScore()
    {
        return config.baseScore;
    }

    public void DestroyTarget(bool diedByPlayer)
    {
        OnEnemyDestroyed?.Invoke(this, diedByPlayer);
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            DestroyTarget(true);
        }
    }

    
}
