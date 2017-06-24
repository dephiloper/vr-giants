using UnityEngine;

public class SpawnTowerBehaviour : MonoBehaviour {
    public GameObject BrickBoyTowerPrefab;
    public GameObject MageTowerPrefab;
    public GameObject ArcherTowerPrefab;
    public GameObject SelectionTowerPrefab;
    public GameObject LaserPrefab;
    public Transform HeadTransform;
    public Transform CameraRigTransform;
    public LayerMask PlaceMask;
    public float Threshold = 0.5f;

    private SteamVR_TrackedObject trackedObj;
    private GameObject laser;
    private GameObject selectionTower;
    private Transform laserTransform;
    private RaycastHit? lastHit;
    private bool placeMode = false;
    private bool showSelectionTower = false;
    private GameObject currentTowerPrefab;

    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start () {
        laser = Instantiate(LaserPrefab);
        laserTransform = laser.transform;
        selectionTower = Instantiate(SelectionTowerPrefab);
    }
    
    void Update()
    {
        laser.SetActive(false);
        selectionTower.SetActive(showSelectionTower);

        if (controller.GetHairTriggerDown())
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

            if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
            {
                // oben
                if (controller.GetAxis().y > Threshold)
                {
                    currentTowerPrefab = BrickBoyTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.red);
                }
                // unten
                else if (controller.GetAxis().y < -Threshold)
                {
                    currentTowerPrefab = null;
                    showSelectionTower = false;
                }
                // rechts
                else if (controller.GetAxis().x > Threshold)
                {
                    currentTowerPrefab = MageTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.magenta);
                }
                // links
                else if (controller.GetAxis().x < -Threshold)
                {
                    currentTowerPrefab = ArcherTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.green);
                }
            }
        }

        if (controller.GetHairTriggerUp()) { 
            if (lastHit.HasValue) { 
                int hitMask = BitPositionToMask(lastHit.Value.transform.gameObject.layer);
                if (placeMode && showSelectionTower && PlaceMask.value == hitMask)
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

        if (PlaceMask.value == hitMask)
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
