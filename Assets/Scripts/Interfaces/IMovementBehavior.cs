
using UnityEngine;

public interface IMovementBehavior
{
    void Move(Rigidbody2D rb, float speed, float deltaTime);
}
