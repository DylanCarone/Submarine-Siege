using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManagerGame : MonoBehaviour
{
    [Header("References")]
    [SerializeField] EnemySpawnManager enemySpawnManager;

    [Header("Settings")] 
    [SerializeField] private int baseEnemies = 5;

    [SerializeField] private float growthRate = 1.2f;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private EnemySpawnConfigSO spawnConfig;

    [Header("Level Configuration")] [SerializeField]
    private LevelDataSO[] gameLevels;
    
    
    
    private int currentLevel = 1;

    private List<EnemyBase> enemyList = new List<EnemyBase>();
    


    private void Start()
    {
        StartCurrentLevel(currentLevel);
    }

    void StartCurrentLevel(int level)
    {
        currentLevel = level;
        LevelDataSO customLevel = (level - 1 < gameLevels.Length) ? gameLevels[level - 1] : null;
        //int enemiesToSpawn = Mathf.RoundToInt(baseEnemies * Mathf.Pow(growthRate, currentLevel - 1));
        if (customLevel)
        {
            StartCoroutine(SpawnEnemies(customLevel));
        }
    }

    // IEnumerator SpawnEnemies(int count)
    // {
    //     enemyList.Clear();
    //     for (int i = 0; i < count; i++)
    //     {
    //         bool spawnFromLeft = Random.value >= 0.5f;
    //         int enemyTypeIndex = GetWeightedRandomEnemyType();
    //         var enemyToSpawn = enemySpawnManager.SpawnEnemy(spawnFromLeft, enemyTypeIndex);
    //         enemyList.Add(enemyToSpawn);
    //         enemyToSpawn.OnEnemyDestroyed += HandleEnemyDestroyed;
    //         yield return new WaitForSeconds(spawnDelay);
    //     }
    // }
    
    IEnumerator SpawnEnemies(LevelDataSO levelData)
    {
        // create a new list of enemies that will be spawned from the data
        List<EnemyBase> enemiesToSpawn = new List<EnemyBase>();
        
        // clear the current list
        enemyList.Clear();
        
        // loop through each enemy type and add them to this list x amount of times determined by the data
        foreach (var enemyType in levelData.enemyTypes)
        {
            for (int i = 0; i < enemyType.count; i++)
            {
                enemiesToSpawn.Add(enemyType.enemyToSpawn);
            }
            
        }
        
        // shuffle the list
        for (int i = enemiesToSpawn.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (enemiesToSpawn[i], enemiesToSpawn[j]) = (enemiesToSpawn[j], enemiesToSpawn[i]);
        }
        
        // loop through each one and set them up for spawning
        foreach (var enemyPrefab in enemiesToSpawn)
        {
            bool spawnFromLeft = Random.value >= 0.5f;
            var enemyToSpawn = enemySpawnManager.SpawnEnemy(spawnFromLeft, enemyPrefab);
            enemyList.Add(enemyToSpawn);
            enemyToSpawn.OnEnemyDestroyed += HandleEnemyDestroyed;
            yield return new WaitForSeconds(spawnDelay);
        }

    }

    void HandleEnemyDestroyed(EnemyBase enemy, bool diedByPlayer)
    {
        enemyList.Remove(enemy);
        if (diedByPlayer)
        { 
            int points = enemy.CalculateScore();
            PlayerData.Instance.Stats.GainCoins(points);
        }
        if (enemyList.Count == 0)
        {
            StartCoroutine(BeginNextLevel());
        }
    }

    IEnumerator BeginNextLevel()
    {
        yield return new WaitForSeconds(spawnDelay);
        StartCurrentLevel(currentLevel +1);
    }

    int GetWeightedRandomEnemyType()
    {
        float totalWeight = 0f;
        float[] weights = new float[spawnConfig.enemyWeights.Length];

        for (int i = 0; i < weights.Length; i++)
        {
            var enemyWeight = spawnConfig.enemyWeights[i];
            if (currentLevel < enemyWeight.unlockLevel)
            {
                weights[i] = 0f;
            }
            else
            {
                weights[i] = enemyWeight.weightCurve.Evaluate(currentLevel);
            }

            totalWeight += weights[i];
        }

        float rand = Random.value * totalWeight;
        float cumulative = 0f;
        for (int i = 0; i < weights.Length; i++)
        {
            cumulative += weights[i];
            if (rand <= cumulative)
            {
                return i;
            }
        }
        return weights.Length - 1;
    }


}
