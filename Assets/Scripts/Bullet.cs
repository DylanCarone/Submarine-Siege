using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private MoveDirection moveDirection;
    private Rigidbody2D rb;

    public float Speed { get; private set; }
    public int Damage { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Initialize(float speed, int damage)
    {
        Speed = speed;
        Damage = damage;
    }

    private void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(0, (float)moveDirection)  * (Speed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + moveDelta;
        rb.MovePosition(newPosition); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        other.GetComponent<IDamagable>()?.TakeDamage(Damage);
        Destroy(gameObject);
    }
}


public enum MoveDirection
{
    Down = -1,
    Up = 1
}