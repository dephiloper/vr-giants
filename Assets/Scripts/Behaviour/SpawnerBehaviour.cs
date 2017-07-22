using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a behaviour which controlls the spawning of enemy waves.
/// </summary>
public class SpawnerBehaviour : MonoBehaviour {
    /// <summary>
    /// Gets the singleton instance of the <see cref="SpawnerBehaviour"/>.
    /// </summary>
    public static SpawnerBehaviour Instance { get; private set; }

    /// <summary>
    /// Prefabs which get instantiated for normal enemies.
    /// </summary>
    public GameObject[] NormalEnemyPrefabs;

    /// <summary>
    /// Prefabs which get instantiated for medium enemies.
    /// </summary>
    public GameObject[] MidEnemyPrefabs;

    /// <summary>
    /// Prefab which gets instantiated for boss enemies.
    /// </summary>
    public GameObject BossEnemyPrefab;

    /// <summary>
    /// SteamVR CameraRig instance.
    /// </summary>
    public GameObject CameraRig;

    /// <summary>
    /// Time in ms between the spawn of single enemies.
    /// </summary>
    public int UnitTimeDelta = 100;

    /// <summary>
    /// Time in ms between the spawn of different waves.
    /// </summary>
    public int WaveTimeDelta = 20000;

    private int spawnedUnits;
    private Timer unitTimer;
    private Timer waveTimer;
    private List<Wave> waves;
    private int currentWave;

    private void Start() {
        if (Instance == null) {
            Instance = this;
        }
        waves = new List<Wave> {
            new Wave(6, NormalEnemyPrefabs, MidEnemyPrefabs),
            new Wave(12, NormalEnemyPrefabs, MidEnemyPrefabs),
            new Wave(15, NormalEnemyPrefabs, MidEnemyPrefabs),
            new Wave(1, BossEnemyPrefab)
        };
    }

    /// <summary>
    /// Starts the wave spawning.
    /// </summary>
    public void StartSpawning() {
        if (waveTimer == null && unitTimer == null) {
            unitTimer = new Timer(UnitTimeDelta, false);
            waveTimer = new Timer(WaveTimeDelta, false);
            GameScoreBehaviour.Instance.StartTime = DateTime.Now;
        }
    }

    private void Update() {
        if (waveTimer != null && waveTimer.IsTimeUp()) {
            if (unitTimer != null && unitTimer.IsTimeUp()) {
                SpawnUnit(waves[currentWave].NextEnemy(), waves[currentWave].Units);
                unitTimer.Reset();
            }
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void SpawnUnit(GameObject enemyPrefab, int units) {
        if (spawnedUnits < units) {
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = transform;
            spawnedUnits++;
        }
        else if (transform.childCount == 0) {
            if (currentWave < waves.Count - 1) {
                spawnedUnits = 0;
                currentWave++;
                waveTimer.Reset();
            }
            else {
                var gameScoreBehaviour = CameraRig.GetComponent<GameScoreBehaviour>();
                if (gameScoreBehaviour) {
                    gameScoreBehaviour.CurrentGameState = GameState.Won;
                }
            }
        }
    }

    /// <summary>
    /// Gets all children and returns them as a array.
    /// </summary>
    /// <returns>Children of this GameObject as a array</returns>
    public Transform[] getChildren() {
        var childs = new Transform[transform.childCount];
        for (var i = 0; i < childs.Length; i++) {
            childs[i] = transform.GetChild(i);
        }

        return childs;
    }
}