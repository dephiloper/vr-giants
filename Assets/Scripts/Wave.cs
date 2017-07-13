using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public int Units { get; set; }

    private const float EnemyProportion = 0.7f;
    private readonly GameObject[] normalEnemies;
    private readonly GameObject[] midEnemies;
    private readonly GameObject bossEnemy;
    
    public Wave(int units, GameObject[] normalEnemies, GameObject[] midEnemies)
    {
        Units = units;
        this.normalEnemies = normalEnemies;
        this.midEnemies = midEnemies;
    }

    public Wave(int units, GameObject bossEnemy)
    {
        Units = units;
        this.bossEnemy = bossEnemy;
    }


    public GameObject NextEnemy()
    {
        if (bossEnemy != null)
        {
            return bossEnemy;
        }

        if (Random.value <= EnemyProportion)
        {
            return normalEnemies[Random.Range(0, normalEnemies.Length)];
        }
        
        return midEnemies[Random.Range(0, midEnemies.Length)];
    }
    
}
