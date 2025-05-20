using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{

    public event Action<EnemyBase> OnEnemyDestroyed; 
    
    [SerializeField] protected EnemyConfigSO config;

    public float Speed { get; protected set; }
    public int Score { get; protected set; }
    public int Lane { get; protected set; }

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

    public void TakeDamage()
    {
        OnEnemyDestroyed?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        TakeDamage();
    }
}
