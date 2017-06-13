using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

    public static SpawnerBehaviour Instance { get; private set; }

    public GameObject NormalEnemyPrefab;
    public GameObject FastEnemyPrefab;
    public GameObject ResistanceEnemyPrefab;

    public int TimeDelta = 100;

    private int spawnedUnits;
    private Timer unitTimer;
    private Timer waveTimer;
    private Timer philippIstKackeTimer;
    private List<Wave> waves;
    private int currentWave;

    void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        unitTimer = new Timer(TimeDelta, false);
        waveTimer = new Timer(20000, false);
        waves = new List<Wave>
        {
            new Wave(NormalEnemyPrefab, 10),
            new Wave(FastEnemyPrefab, 5),
            new Wave(ResistanceEnemyPrefab, 3),
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
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
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
        Transform[] childs = new Transform[transform.childCount];
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i] = transform.GetChild(i);
        }

        return childs;
    }
}
