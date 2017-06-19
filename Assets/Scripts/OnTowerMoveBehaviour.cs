using UnityEngine;

public class OnTowerMoveBehaviour : MonoBehaviour {

    public GameObject TeleportReticlePrefab;
    public GameObject LaserPrefab;
    public Transform HeadTransform;
    public Transform CameraRigTransform;
    public LayerMask StandOnMask;
    public Vector3 LastGiantPos;

    private SteamVR_TrackedObject trackedObj;
    private GameObject laser;
    private GameObject reticle;
    private Transform laserTransform;
    private Transform teleportReticleTransform;
    private RaycastHit? lastHit;
    
    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        laser = Instantiate(LaserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(TeleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
        Debug.Log("OnTower mode enabled.");
    }

    private static int BitPositionToMask(int bitPos)
    {
        return (1 << bitPos);
    }

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f);
        laserTransform.LookAt(hit.point);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
        reticle.SetActive(true);
        teleportReticleTransform.position = hit.point;

        int hitMask = BitPositionToMask(hit.transform.gameObject.layer);
        if (StandOnMask.value == hitMask)
        {
            laser.GetComponent<Renderer>().material.color = Color.blue;
        }
        else
        {
            laser.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void Teleport(RaycastHit hit)
    {
        Vector3 difference = CameraRigTransform.position - HeadTransform.position;
        //difference.y = CameraRigTransform.position.y;
        difference.y = 0;
        
        int hitMask = BitPositionToMask(hit.transform.gameObject.layer);
        if (StandOnMask.value == hitMask)
        {
            CameraRigTransform.position = hit.point;
            CameraRigTransform.position += difference;
        }
    }

    void Update()
    {
        laser.SetActive(false);
        reticle.SetActive(false);

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (controller.GetAxis().y > 0)
            {
                RaycastHit hit;

                if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, float.PositiveInfinity))
                {
                    ShowLaser(hit);
                    lastHit = hit;
                }
            } else
            {
                // teleport back
                CameraRigTransform.position = LastGiantPos;
                CameraRigTransform.GetComponent<MovementChangeBehaviour>().MovementState = State.Giant;
                CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole = Role.None;
            }
        }

        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (controller.GetAxis().y > 0)
            {
                if (lastHit.HasValue)
                {
                    Teleport(lastHit.Value);
                }
            }
        }
    }
}
