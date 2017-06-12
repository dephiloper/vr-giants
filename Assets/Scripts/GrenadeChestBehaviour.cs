using UnityEngine;

public class GrenadeChestBehaviour : MonoBehaviour {

    public GameObject GrenadePrefab;
    public int TimeDelta = 3;

    private Transform[] slots;
    private GameObject[] grenades;
    private int lastTime;

    private void Awake()
    {
        grenades = new GameObject[transform.childCount];
        slots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            slots[i] = transform.GetChild(i);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        int time = (int)Time.fixedTime;

        if ((time % TimeDelta == 0) && time != lastTime)
        {
            for (int i = 0; i < grenades.Length; i++)
            {
                if (grenades[i] == null)
                {
                    grenades[i] = Instantiate(GrenadePrefab, slots[i].transform.position, Quaternion.identity);
                    lastTime = time;
                    break;
                }
            }
        }

    }
}
