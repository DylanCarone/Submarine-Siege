using UnityEngine;

public class Movement2DController : MonoBehaviour
{
    [SerializeField] private AnimationCurve speedOverTime;
    [SerializeField] private float baseSpeed = 3f;
    [SerializeField] private Vector2 direction = Vector2.right;
    
    private Rigidbody2D rb;
    private float elapsedTime = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D required");
            enabled = false;
        }
    }
    
    void Update()
    {
        elapsedTime += Time.deltaTime;
    }
    
    void FixedUpdate()
    {
        // Get current speed from the animation curve
        float currentSpeed = baseSpeed * speedOverTime.Evaluate(elapsedTime);
        
        // Apply movement
        rb.linearVelocity = direction.normalized * currentSpeed;
    }
    
    // Call this when the enemy hits something and needs to change direction
    void OnCollisionEnter2D(Collision2D collision)
    {
        direction = -direction;
    }

}