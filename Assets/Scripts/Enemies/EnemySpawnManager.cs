using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyBase[] enemyPrefabs;
    [SerializeField] private Transform[] leftSpawnPoints;
    [SerializeField] private Transform[] rightSpawnPoints;

    
    public EnemyBase SpawnEnemy(bool spawnFromLeft, int enemySpawnIndex)
    {
        int lane = Random.Range(0, leftSpawnPoints.Length);
        Transform spawnPoint = spawnFromLeft ? leftSpawnPoints[lane] : rightSpawnPoints[lane];
        EnemyBase enemyPrefab = enemyPrefabs[enemySpawnIndex];
        
        EnemyBase newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        newEnemy.Initialize(lane, spawnFromLeft);
        return newEnemy;
    }

}
