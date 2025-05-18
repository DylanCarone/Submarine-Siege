using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private MoveDirection moveDirection;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(0, (float)moveDirection)  * (speed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + moveDelta;
        rb.MovePosition(newPosition); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<EnemySub>()?.DestroyEnemyByPlayer();
        other.GetComponent<PlayerLives>()?.PlayerHit();
        Destroy(gameObject);
    }
}


public enum MoveDirection
{
    Down = -1,
    Up = 1
}