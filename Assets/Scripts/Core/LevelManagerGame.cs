using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManagerGame : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Reference to the Enemy Spawn Manager component.")]
    [SerializeField] EnemySpawnManager enemySpawnManager;

    [SerializeField] private EnemyBase enemyPrefab;
    [SerializeField] private SelectUpgrade upgradeSelector;

    [Header("Settings")] 
    [SerializeField] private int baseEnemies = 5;
    [SerializeField] private float growthRate = 1.2f;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private EnemySpawnConfigSO spawnConfig;

    [Header("Level Configuration")] [SerializeField]
    [Tooltip("Array of level data scriptable objects defining each level's enemy composition")]
    private LevelDataSO[] gameLevels;
    
    
    /// The current level number the player is on
    private int currentLevel = 1;

    /// List of all enemies currently on the map. Used to track when all enemies have been destroyed.
    private List<EnemyBase> enemyList = new List<EnemyBase>();
    
    
    


    private void Start()
    {
        StartCurrentLevel(currentLevel);
    }

    /// <summary>
    /// Starts the specified level by initializing enemies and configuring the level parameters.
    /// </summary>
    /// <param name="level">The number of the level to start.
    /// This value should correspond to an index within the game levels array.</param>
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


    /// <summary>
    /// Spawns enemies for the current level based on the provided level data.
    /// Each enemy type and count is defined in the level data. Enemies are added to the spawn queue, shuffled,
    /// and spawned at a random position, either from the left or right side.
    /// </summary>
    /// <param name="levelData">The data object containing enemy types and counts for the current level.</param>
    /// <returns>An IEnumerator allowing the spawning process to be executed over time using a coroutine.</returns>
    IEnumerator SpawnEnemies(LevelDataSO levelData)
    {
        // create a new list of enemies that will be spawned from the data
        List<EnemyConfigSO> enemiesToSpawn = new List<EnemyConfigSO>();
        
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
        foreach (var enemyData in enemiesToSpawn)
        {
            bool spawnFromLeft = Random.value >= 0.5f;
            var enemyToSpawn = enemySpawnManager.SpawnEnemy(spawnFromLeft, enemyPrefab, enemyData);
            enemyList.Add(enemyToSpawn);
            enemyToSpawn.OnEnemyDestroyed += HandleEnemyDestroyed;
            yield return new WaitForSeconds(spawnDelay);
        }

    }


    /// <summary>
    /// Handles the logic for when an enemy is destroyed, including player rewards and level progression.
    /// </summary>
    /// <param name="enemy">The enemy instance that was destroyed.</param>
    /// <param name="diedByPlayer">Indicates whether the player destroyed the enemy.</param>
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
            // go to the pick upgrade menu
            OnRoundEnd();
            // after the player picker the upgrade
            // begin the next level
                        
            //StartCoroutine(BeginNextLevel());
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


    void OnRoundEnd()
    {
        upgradeSelector.PresentUpgradeOptions(() =>
        {
            StartCoroutine(BeginNextLevel());
        });
    }

}
