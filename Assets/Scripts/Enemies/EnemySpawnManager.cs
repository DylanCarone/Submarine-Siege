using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] leftSpawnPoints;
    [SerializeField] private Transform[] rightSpawnPoints;

    

    public int PickLane(int preferredLane)
    {
        // If the preferred lane is valid, use it, otherwise return a random lane
        if (preferredLane >= 0 && preferredLane < leftSpawnPoints.Length)
        {
            return preferredLane;
        }
        
        return Random.Range(0, leftSpawnPoints.Length);
    }

    
    public EnemyBase SpawnEnemy(bool spawnFromLeft, EnemyBase enemyPrefab, EnemyConfigSO enemyData, int lane = -1)
    {
        EnemyBase newEnemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        newEnemy.SetConfiguration(enemyData);
        
        if (lane < 0)
        {
            lane = enemyPrefab.PickLane(leftSpawnPoints.Length);
        }
        
        lane = Mathf.Clamp(lane, 0, leftSpawnPoints.Length - 1);


        Transform spawnPoint = spawnFromLeft ? leftSpawnPoints[lane] : rightSpawnPoints[lane];
        float randomYExtra = Random.Range(-0.5f, 0.5f);
        
        newEnemy.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y + randomYExtra, spawnPoint.position.z);
        newEnemy.Initialize(lane, spawnFromLeft);
        
        return newEnemy;
    }

}
