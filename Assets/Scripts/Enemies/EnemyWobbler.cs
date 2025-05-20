using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyWobbler : EnemyBase
{
    [SerializeField] private EnemyUI enemyUI;
    [SerializeField] private SpriteRenderer sp;
    [SerializeField] private float sinAmplitude = 1f;
    [SerializeField] private float sinFrequency = 1f;
    
    private float baseY;
    private float timeOffset = 0f;

    private void Start()
    {
        baseY = transform.position.y;
        timeOffset = Random.Range(0f, Mathf.PI * 2f);

    }
    
    public override void Initialize(int lane, bool spawnFromLeft)
    {
        base.Initialize(lane, spawnFromLeft);
        enemyUI?.SetScore(CalculateScore());
        sp.flipX = spawnFromLeft;

    }

    protected override void FixedUpdate()
    {
        Vector2 moveDelta = new Vector2(1, 0) * (Speed * Time.fixedDeltaTime);
        Vector2 newPosition = rb.position + moveDelta;

        float sinY = sinAmplitude * Mathf.Sin((Time.time * sinFrequency) + timeOffset);
        newPosition.y = baseY + sinY;
        
        rb.MovePosition(newPosition);
    }
    
    public override int CalculateScore()
    {
        int baseScore = config.baseScore;
        int laneBonus = Lane * 10;
        int speedBonus = Mathf.RoundToInt(Mathf.Abs(Speed) * config.speedBonusMultiplier);
        return baseScore + laneBonus + speedBonus;
    }
}
