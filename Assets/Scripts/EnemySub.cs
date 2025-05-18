using System;
using TMPro;
using UnityEngine;

public class EnemySub : MonoBehaviour
{

    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 1f;

    private float fireTimer = 0f;
    
    public event Action<EnemySub, bool> OnEnemyDestroyed;
    
    public float Speed { get; private set; }
    public int Lane { get; private set; }
    public int Score { get; private set; }

    public bool CanShoot { get; private set; }
    private Rigidbody2D rb;
    
    
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if (CanShoot)
        {
            ShootBullet();
        }
    }

    public void Initialize(float speed, int lane, bool canShoot)
    {
        Speed = speed;
        Lane = lane;
        CanShoot = canShoot;
        Score = CalculateScore(lane, speed);
        scoreText.text = Score.ToString();

        sp.flipX = speed > 0;
    }

    private void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(1, 0)  * (Speed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + moveDelta;
        rb.MovePosition(newPosition); 
    
    }

    public void DestroyEnemyByPlayer()
    {
        OnEnemyDestroyed?.Invoke(this, true);
        Destroy(gameObject);
    }
    public void DestoryEnemyByTime()
    {
        OnEnemyDestroyed?.Invoke(this, false);
        Destroy(gameObject);
    }
    

    int CalculateScore(int lane, float speed)
    {
        int baseScore = lane * 10;
        int speedBonus = Mathf.RoundToInt(Mathf.Abs(speed) * 5);
        return baseScore + speedBonus;
    }

    void ShootBullet()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            fireTimer = 0f;
        }
    }
    
}
