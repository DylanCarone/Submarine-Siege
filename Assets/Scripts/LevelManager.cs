using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    /*[SerializeField] private EnemySpawner spawner;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private int baseEnemies = 5;
    [SerializeField] private float growthRate = 1.2f;
    [SerializeField] private float spawnDelay = 0.5f;
    [SerializeField] private float nextLevelDelay = 1f;
    
    private int currentLevel = 1;
    private List<EnemySub> activeEnemies = new List<EnemySub>();

    public int Score { get; private set; }

    private void Start()
    {
        StartLevel(currentLevel);
        
    }

    void StartLevel(int level)
    {
        currentLevel = level;
        levelText.text = $"LVL {currentLevel}";
        scoreText.text = Score.ToString();
        spawner.GetShootChance(currentLevel);
        int enemiesToSpawn = Mathf.RoundToInt(baseEnemies * Mathf.Pow(growthRate, currentLevel - 1));
        StartCoroutine(SpawnEnemies(enemiesToSpawn));

    }
    


    IEnumerator SpawnEnemies(int count)
    {
        activeEnemies.Clear();
        for (int i = 0; i < count; i++)
        {
            bool spawnFromLeft = Random.value > 0.5f;

            
            EnemySub enemy = spawner.SpawnEnemy(spawnFromLeft);
            activeEnemies.Add(enemy);
            enemy.OnEnemyDestroyed += HandleEnemyDestroyed;
            
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void HandleEnemyDestroyed(EnemySub enemy, bool playerKilled)
    {
        activeEnemies.Remove(enemy);
        
        if(playerKilled)
            Score += enemy.Score;
        
        scoreText.text = Score.ToString();
        if (activeEnemies.Count == 0)
        {
            StartCoroutine(NextLevelAfterDelay());
        }
    }
    IEnumerator NextLevelAfterDelay()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        StartLevel(currentLevel + 1);
    }*/
    
    
    
    
}
