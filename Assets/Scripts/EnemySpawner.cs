using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    /*[SerializeField] private EnemySub enemyPrefab;

    [SerializeField] private Transform[] leftSpawnPoints;
    [SerializeField] private Transform[] rightSpawnPoints;
    
    [SerializeField] float minSpeed = 1f, maxSpeed = 3f;

    private float shootChance;

    public EnemySub SpawnEnemy(bool spawnFromLeft)
    {
        int lane = Random.Range(0, leftSpawnPoints.Length);
        
        Transform spawnPoint = spawnFromLeft ? 
            leftSpawnPoints[lane] 
            : rightSpawnPoints[lane];
        
        float speed = Random.Range(minSpeed, maxSpeed);
        float finalSpeed = spawnFromLeft ? Mathf.Abs(speed) : -Mathf.Abs(speed);
        bool canShoot = Random.value < shootChance;

        EnemySub newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        newEnemy.Initialize(finalSpeed, lane+1, canShoot);

        return newEnemy;
    }
    
    
    public void GetShootChance(int level, float growthRate = 0.2f)
    {
        if (level <= 1) shootChance = 0f; // Level 1: 0% chance
        shootChance = 1f - Mathf.Exp(-growthRate * (level - 1));
    }*/
}
