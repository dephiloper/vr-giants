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
    
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    private void Awake()
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

    private void ShowLaser(RaycastHit hit)
    {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f);
        laserTransform.LookAt(hit.point);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
        reticle.SetActive(true);
        teleportReticleTransform.position = hit.point;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);
        laser.GetComponent<Renderer>().material.color = 
            (StandOnMask.value & hitMask) != 0 ? Color.blue : Color.red;
    }

    private void Teleport(RaycastHit hit)
    {
        var difference = CameraRigTransform.position - HeadTransform.position;
        //difference.y = CameraRigTransform.position.y;
        difference.y = 0;
        
        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);
        
        Debug.Log(string.Format("StandOnMask: {0}, HitMask: {1}", StandOnMask.value, hitMask));
        if ((StandOnMask.value & hitMask) != 0) {
            var currentRole = CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole;

            if (TagUtility.CompareRoleWithTowerTag(currentRole, hit.transform.tag)) {
                Debug.Log("wqirejwqrjh");
                CameraRigTransform.position = hit.point;
                CameraRigTransform.position += difference;
            }
        }
    }

    private void Update()
    {
        laser.SetActive(false);
        reticle.SetActive(false);

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (Controller.GetAxis().y > 0)
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

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (Controller.GetAxis().y > 0)
            {
                if (lastHit.HasValue)
                {
                    Teleport(lastHit.Value);
                }
            }
        }
    }
}
