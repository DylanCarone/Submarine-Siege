using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float clampDistance = 3.5f;


    
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    
    private void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(input.Movement, 0) * (speed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + moveDelta;
        newPosition.x = Mathf.Clamp(newPosition.x, -clampDistance, clampDistance);
        
        rb.MovePosition(newPosition);
    }
}
