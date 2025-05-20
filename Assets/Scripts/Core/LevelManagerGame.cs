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
    
    private int currentLevel = 1;

    private List<EnemyBase> enemyList = new List<EnemyBase>();

    private int score;


    private void Start()
    {
        score = 0;
        StartCurrentLevel(currentLevel);
    }

    void StartCurrentLevel(int level)
    {
        currentLevel = level;
        int enemiesToSpawn = Mathf.RoundToInt(baseEnemies * Mathf.Pow(growthRate, currentLevel - 1));
        StartCoroutine(SpawnEnemies(enemiesToSpawn));
    }

    IEnumerator SpawnEnemies(int count)
    {
        enemyList.Clear();
        for (int i = 0; i < count; i++)
        {
            bool spawnFromLeft = Random.value >= 0.5f;
            int enemyTypeIndex = GetWeightedRandomEnemyType();
            var enemyToSpawn = enemySpawnManager.SpawnEnemy(spawnFromLeft, enemyTypeIndex);
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
            score += points;
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
