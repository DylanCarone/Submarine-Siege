using UnityEngine;

public class PongPrototype : MonoBehaviour
{
    public Transform leftPaddle, rightPaddle, ball;
    public float paddleSpeed = 10f;
    public float ballSpeed = 12f;

    private Vector2 ballDirection;
    private int leftScore = 0, rightScore = 0;



    void Start()
    {
        ResetBall();
    }
    
    
    void Update()
    {
        // Paddle movement (W/S for left, Up/Down for right)
        float leftMove = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;
        float rightMove = Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0;
        leftPaddle.Translate(Vector3.up * leftMove * paddleSpeed * Time.deltaTime);
        rightPaddle.Translate(Vector3.up * rightMove * paddleSpeed * Time.deltaTime);

        // Ball movement
        ball.Translate(ballDirection * ballSpeed * Time.deltaTime);

        // Collision with top/bottom
        if (Mathf.Abs(ball.position.y) > 4.5f) ballDirection.y *= -1;

        // Collision with paddles
        if (Mathf.Abs(ball.position.x - leftPaddle.position.x) < 0.5f &&
            Mathf.Abs(ball.position.y - leftPaddle.position.y) < 1.5f && ballDirection.x < 0)
            ballDirection.x *= -1;

        if (Mathf.Abs(ball.position.x - rightPaddle.position.x) < 0.5f &&
            Mathf.Abs(ball.position.y - rightPaddle.position.y) < 1.5f && ballDirection.x > 0)
            ballDirection.x *= -1;

        // Scoring
        if (ball.position.x < -9f)
        {
            rightScore++;
            ResetBall();
        }
        if (ball.position.x > 9f)
        {
            leftScore++;
            ResetBall();
        }
    }
    
    
    void ResetBall()
    {
        ball.position = Vector3.zero;
        ballDirection = new Vector2(Random.value < 0.5f ? -1 : 1, Random.Range(-0.5f, 0.5f)).normalized;
    }
    
}
