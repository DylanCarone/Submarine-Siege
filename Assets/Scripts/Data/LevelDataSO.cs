using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Level Data")]
public class LevelDataSO : ScriptableObject
{
    public int EnemyCount {
        get
        {
            int total = 0;
            if (enemyTypes != null)
            {
                foreach (var enemy in enemyTypes)
                {
                    if (enemy != null)
                    {
                        total += enemy.count;
                    }
                }
            }

            return total;
        }
    }

    public EnemyTypeCount[] enemyTypes;
    
}

[Serializable]
public class EnemyTypeCount
{
    public EnemyBase enemyToSpawn;
    public int count;
}