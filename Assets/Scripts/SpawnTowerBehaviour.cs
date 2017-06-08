using UnityEngine;

public class SpawnTowerBehaviour : MonoBehaviour {
    public GameObject brickBoyTowerPrefab;
    public GameObject mageTowerPrefab;
    public GameObject archerTowerPrefab;
    public GameObject selectionTowerPrefab;
    public GameObject laserPrefab;
    public Transform headTransform;
    public Transform cameraRigTransform;
    public LayerMask placeMask;
    public float threshold = 0.5f;

    private SteamVR_TrackedObject trackedObj;
    private GameObject laser;
    private GameObject selectionTower;
    private Transform laserTransform;
    private RaycastHit? lastHit;
    private bool placeMode = false;
    private bool showSelectionTower = false;
    private GameObject currentTowerPrefab;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start () {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        selectionTower = Instantiate(selectionTowerPrefab);
    }
    
    void Update()
    {
        laser.SetActive(false);
        selectionTower.SetActive(showSelectionTower);

        if (Controller.GetHairTriggerDown())
        {
            placeMode = true;
        }

        if (placeMode)
        {
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, float.PositiveInfinity))
            {
                ShowLaser(hit);
                lastHit = hit;
            }

            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                // oben
                if (Controller.GetAxis().y > threshold)
                {
                    currentTowerPrefab = brickBoyTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.red);
                }
                // unten
                else if (Controller.GetAxis().y < -threshold)
                {
                    currentTowerPrefab = null;
                    showSelectionTower = false;
                }
                // rechts
                else if (Controller.GetAxis().x > threshold)
                {
                    currentTowerPrefab = mageTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.magenta);
                }
                // links
                else if (Controller.GetAxis().x < -threshold)
                {
                    currentTowerPrefab = archerTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.green);
                }
            }
        }

        if (Controller.GetHairTriggerUp()) { 
            if (lastHit.HasValue) { 
                int hitMask = BitPositionToMask(lastHit.Value.transform.gameObject.layer);
                if (placeMode && showSelectionTower && placeMask.value == hitMask)
                {
                    InstantiateTower();
                }
            }

            showSelectionTower = false;
            placeMode = false;
        }
    }

    private void ChangeChildColor(Color color)
    {
        foreach (var renderer in selectionTower.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = color;
        }
    }

    private void InstantiateTower()
    {
        var tower = Instantiate(currentTowerPrefab);
        tower.transform.position = selectionTower.transform.position;
        
    }

    private void ShowLaser(RaycastHit hit)
	{
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f);
        laserTransform.LookAt(hit.point);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
        selectionTower.transform.position = hit.point;
        selectionTower.transform.position += 4 * Vector3.up;
       
        int hitMask = BitPositionToMask(hit.transform.gameObject.layer);

        if (placeMask.value == hitMask)
        {
            laser.GetComponent<Renderer>().material.color = Color.yellow;
        } else
        {
            laser.GetComponent<Renderer>().material.color = Color.red;
            showSelectionTower = false;
        }
    }

    private int BitPositionToMask(int bitPos)
    {
        return (1 << bitPos);
    }
}
