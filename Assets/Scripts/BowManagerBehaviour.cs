using UnityEngine;

public class BowManagerBehaviour : MonoBehaviour {

    public static BowManagerBehaviour Instance { get; private set; }

    public GameObject BowPrefab;
    public SteamVR_TrackedObject TrackedObj { get; private set; }
    public GameObject Bow { get; private set; }
    public GameObject String { get; private set; }

    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int)TrackedObj.index); }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
       
    }

    private void AttachBow()
    {
        if (Bow == null)
        {
            Bow = Instantiate(BowPrefab);
            String = Bow.transform.Find("Bow/main/string").gameObject;
            Bow.transform.parent = TrackedObj.transform;
            Bow.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    void OnDisable()
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

    void Update()
    {
        AttachBow();
    }
}
