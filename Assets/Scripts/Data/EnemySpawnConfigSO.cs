using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Enemy Spawn Config")]
public class EnemySpawnConfigSO : ScriptableObject
{
    [System.Serializable]
    public class EnemyWeight
    {
        public string enemyName; // For clarity
        public int unlockLevel; // level at which this enemy can spawn
        public AnimationCurve weightCurve; // Probability curve over levels (0-1)
    }

    public EnemyWeight[] enemyWeights;
}
