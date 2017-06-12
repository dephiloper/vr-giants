using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

    public static SpawnerBehaviour Instance { get; private set; }
    public GameObject EnemyPrefab;
    public int TimeDelta = 1000;
    public int Units = 1;

    private int spawnedUnits;
    private Timer timer;
    
    void Start () {
        if (Instance == null)
        {
            Instance = this;
        }
        timer = new Timer(TimeDelta, false);
	}

    private void Update()
    {
        if (timer.IsTimeUp())
        {
            SpawnUnit();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void SpawnUnit()
    {
        if (spawnedUnits < Units)
        {
            GameObject enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = transform;
            spawnedUnits++;
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
