using UnityEngine;

public class GrenadeChestBehaviour : MonoBehaviour {

    public GameObject GrenadePrefab;
    public int TimeDelta = 3000;

    private Transform[] slots;
    private GameObject[] grenades;
    private Timer timer;
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

    void Start () {
        timer = new Timer(TimeDelta);
    }
	
	void FixedUpdate () {
        if (timer.IsTimeUp())
        {
            for (int i = 0; i < grenades.Length; i++)
            {
                if (grenades[i] == null)
                {
                    grenades[i] = Instantiate(GrenadePrefab, slots[i].transform.position, Quaternion.identity);
                    break;
                }
            }
        }

    }
}
