using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemySubmarine genericEnemyPrefab;

    public EnemyBase CreateEnemey(EnemyConfigSO config, Vector3 position, int lane, bool spawnFromLeft)
    {
        EnemySubmarine enemy = Instantiate(genericEnemyPrefab, position, Quaternion.identity);
        enemy.SetConfiguration(config);
        enemy.Initialize(lane, spawnFromLeft);

        return enemy;
    }
}
