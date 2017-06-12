using UnityEngine;

public class GiantMoveBehaviour : MonoBehaviour {

    public GameObject TeleportReticlePrefab;
    public GameObject LaserPrefab;
    public Transform HeadTransform;
    public Transform CameraRigTransform;
    public LayerMask TeleportMask;
    public LayerMask StandOnMask;

    private SteamVR_TrackedObject trackedObj;
    private GameObject laser;
	private GameObject reticle;
    private Transform laserTransform;
    private Transform teleportReticleTransform;
    private RaycastHit? lastHit;

    private SteamVR_Controller.Device controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
    }

    void Start() {
		laser = Instantiate(LaserPrefab);
        laserTransform = laser.transform;
		reticle = Instantiate (TeleportReticlePrefab);
		teleportReticleTransform = reticle.transform;
        Debug.Log("Giant mode enabled.");
    }

    private int BitPositionToMask(int bitPos)
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
        if (TeleportMask.value == hitMask)
        {
            laser.GetComponent<Renderer>().material.color = Color.blue;
        } else if (StandOnMask.value == hitMask)
        {
            laser.GetComponent<Renderer>().material.color = Color.green;
        } else
        {
            laser.GetComponent<Renderer>().material.color = Color.red;
        }
    }

	private void Teleport(RaycastHit hit) 
	{
		Vector3 difference = CameraRigTransform.position - HeadTransform.position;
		difference.y = CameraRigTransform.position.y;

        int hitMask = BitPositionToMask(hit.transform.gameObject.layer);
        if (TeleportMask.value == hitMask)
        {
            CameraRigTransform.position = hit.point + difference;
        }
        else if (StandOnMask.value == hitMask)
        {
            CameraRigTransform.GetComponent<MovementChangeBehaviour>().MovementState = State.Tower;
            CameraRigTransform.position = hit.point;
            ChangeTowerRole(hit);
        }
    }

    private void ChangeTowerRole(RaycastHit hit)
    {
        switch (hit.transform.tag)
        {
            case "ArcherTower":
                CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole = Role.Archer;
                break;
            case "BrickBoyTower":
                CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole = Role.BrickBoy;
                break;
            case "MageTower":
                CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole = Role.Mage;
                break;
            default:
                CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole = Role.None;
                break;
        }
    }

    void Update(){
        
        laser.SetActive(false);
        reticle.SetActive(false);
        if (!controller.GetHairTrigger()) { 
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
                }
            }

            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
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
}
