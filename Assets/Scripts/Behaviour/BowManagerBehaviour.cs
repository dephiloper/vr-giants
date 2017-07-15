using UnityEngine;

public class BowManagerBehaviour : MonoBehaviour {

    public static BowManagerBehaviour Instance { get; private set; }

    public SteamVR_TrackedObject TrackedObj { get; private set; }
    public GameObject BowPrefab;
    public GameObject Bow { get; private set; }
    public GameObject String { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SpawnBow()
    {
        if (Bow != null) return;
        
        Bow = Instantiate(BowPrefab);
        String = Bow.transform.Find("Bow/main/string").gameObject;
        Bow.transform.parent = TrackedObj.transform;
        Bow.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnDisable()
    {
        Destroy(Bow);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        SpawnBow();
    }
}
