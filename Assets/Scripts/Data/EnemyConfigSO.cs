using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Config")]
public class EnemyConfigSO : ScriptableObject
{
    public float minSpeed;
    public float maxSpeed;
    public int baseScore;
    public int speedBonusMultiplier;

    public float Speed { get; private set;}
}
