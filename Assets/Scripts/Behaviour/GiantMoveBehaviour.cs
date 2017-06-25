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

    private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}

    private void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
    }

    private void Start() {
		laser = Instantiate(LaserPrefab);
        laserTransform = laser.transform;
		reticle = Instantiate (TeleportReticlePrefab);
		teleportReticleTransform = reticle.transform;
        Debug.Log("Giant mode enabled.");
    }

    private void ShowLaser(RaycastHit hit)
	{
        laser.SetActive(true);
	    laser.transform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f);
	    laser.transform.LookAt(hit.point);
	    laser.transform.localScale = new Vector3(laser.transform.localScale.x, laser.transform.localScale.y,
            hit.distance);
        reticle.SetActive(true);
        teleportReticleTransform.position = hit.point;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);
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
		var difference = CameraRigTransform.position - HeadTransform.position;
		difference.y = CameraRigTransform.position.y;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);
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
        var roleChangeBehaviour = CameraRigTransform.GetComponent<RoleChangeBehaviour>();
        if (roleChangeBehaviour)
        {
            roleChangeBehaviour.TowerRole = TagUtility.TagToTowerRole(hit.transform.tag);    
        }
    }

    private void Update()
    {
        laser.SetActive(false);
        reticle.SetActive(false);
        if (!Controller.GetHairTrigger()) { 
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
                }
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
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
}
