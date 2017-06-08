using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour {

    public GameObject enemyPrefab;
    public int timeDelta;
    public int units;

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
        if ((time % timeDelta == 0) && time != lastTime && spawnedUnits < units)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            lastTime = time;
            spawnedUnits++;
        }
    }
}
