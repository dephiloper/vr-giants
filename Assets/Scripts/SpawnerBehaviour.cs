using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

    public GameObject EnemyPrefab;
    public int TimeDelta = 1;
    public int Units = 1;

    private int spawnedUnits;
    private int lastTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
        int time = (int)Time.fixedTime;
        if ((time % TimeDelta == 0) && time != lastTime && spawnedUnits < Units)
        {
            GameObject enemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            lastTime = time;
            spawnedUnits++;
        }
    }
}
