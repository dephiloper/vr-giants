using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

    public static SpawnerBehaviour Instance { get; private set; }

    public GameObject WarriorEnemyPrefab;
    public GameObject MageEnemyPrefab;
    public GameObject ArcherEnemyPrefab;
    public GameObject BossEnemyPrefab;

    public int TimeDelta = 100;

    private int spawnedUnits;
    private Timer unitTimer;
    private Timer waveTimer;
    private List<Wave> waves;
    private int currentWave;

    private void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        unitTimer = new Timer(TimeDelta, false);
        waveTimer = new Timer(20000, false);
        waves = new List<Wave>
        {
            new Wave(WarriorEnemyPrefab, 10),
            new Wave(MageEnemyPrefab, 10),
            new Wave(ArcherEnemyPrefab, 10),
            new Wave(BossEnemyPrefab, 1)
        };
    }

    private void Update()
    {   if (waveTimer.IsTimeUp()) { 
            if (unitTimer.IsTimeUp())
            {
                SpawnUnit(waves[currentWave].Enemy, waves[currentWave].Units);
                unitTimer.Reset();
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void SpawnUnit(GameObject enemyPrefab, int units)
    {
        if (spawnedUnits < units)
        {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = transform;
            spawnedUnits++;
        } else if (currentWave < waves.Count - 1 && transform.childCount == 0)
        {
            spawnedUnits = 0;
            currentWave++;
            waveTimer.Reset();
        }
    }

    public Transform[] getChildren()
    {
        var childs = new Transform[transform.childCount];
        for (var i = 0; i < childs.Length; i++)
        {
            childs[i] = transform.GetChild(i);
        }

        return childs;
    }
}
