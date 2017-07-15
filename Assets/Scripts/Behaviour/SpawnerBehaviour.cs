using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

    public static SpawnerBehaviour Instance { get; private set; }

    public GameObject[] NormalEnemyPrefabs;
    public GameObject[] MidEnemyPrefabs;
    
    public GameObject BossEnemyPrefab;
    public GameObject CameraRig;

    public int UnitTimeDelta = 100;
    public int WaveTimeDelta = 20000;
    
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
        waves = new List<Wave>
        {
            new Wave(6, NormalEnemyPrefabs, MidEnemyPrefabs),
            new Wave(12, NormalEnemyPrefabs, MidEnemyPrefabs),
            new Wave(15, NormalEnemyPrefabs, MidEnemyPrefabs),
            new Wave(1, BossEnemyPrefab)
        };
    }

    public void StartSpawning()
    {
        if (waveTimer == null && unitTimer == null)
        {
            unitTimer = new Timer(UnitTimeDelta, false);
            waveTimer = new Timer(WaveTimeDelta, false);
            GameScoreBehaviour.Instance.StartTime = DateTime.Now;
        }
    }

    private void Update()
    {
        if (waveTimer != null && waveTimer.IsTimeUp()) { 
            if (unitTimer != null && unitTimer.IsTimeUp())
            {
                SpawnUnit(waves[currentWave].NextEnemy(), waves[currentWave].Units);
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
        } else if (transform.childCount == 0)
        {
            if (currentWave < waves.Count - 1)
            {
                spawnedUnits = 0;
                currentWave++;
                waveTimer.Reset();

            }
            else
            {
                var gameScoreBehaviour = CameraRig.GetComponent<GameScoreBehaviour>();
                if (gameScoreBehaviour)
                {
                    gameScoreBehaviour.CurrentGameState = GameState.Won;
                }
            }
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
