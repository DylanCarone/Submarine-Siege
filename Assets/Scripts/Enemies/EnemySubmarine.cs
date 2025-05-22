using UnityEngine;

public class EnemySubmarine : EnemyBase
{
    [SerializeField] private EnemyUI enemyUI;
    [SerializeField] private SpriteRenderer sp;


    public override void Initialize(int lane, bool spawnFromLeft)
    {
        base.Initialize(lane, spawnFromLeft);
        enemyUI?.SetScore(CalculateScore());
        sp.flipX = spawnFromLeft;

    }
    
    
    public override int CalculateScore()
    {
        int baseScore = config.baseScore;
        int laneBonus = Lane + 2;
        int speedBonus = Mathf.RoundToInt(Mathf.Abs(Speed) * config.speedBonusMultiplier);
        return baseScore + laneBonus + speedBonus;
    }
    
}
