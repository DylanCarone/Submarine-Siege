
using UnityEngine;

public class StraightMovement : IMovementBehavior
{
    public void Move(Rigidbody2D rb, float speed, float deltaTime)
    {
        Vector2 moveDelta = new Vector2(1, 0) * (speed * deltaTime);
        rb.MovePosition(rb.position + moveDelta);
    }
}
