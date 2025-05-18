using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float clampDistance = 3.5f;
    
    private float movement;

    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(movement, 0) * speed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + moveDelta;
        newPosition.x = Mathf.Clamp(newPosition.x, -clampDistance, clampDistance);
        
        rb.MovePosition(newPosition);
    }
}
