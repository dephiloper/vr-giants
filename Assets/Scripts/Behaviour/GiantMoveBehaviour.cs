using UnityEngine;

/// <summary>
/// Represents a behaviour which allowes the player to move around in the giant move state.
/// </summary>
public class GiantMoveBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefabs which gets instantiated to mark the end of the laser.
    /// </summary>
    public GameObject TeleportReticlePrefab;

    /// <summary>
    /// Prefabs which gets instantiated to show where the player is aiming at.
    /// </summary>
    public GameObject LaserPrefab;

    /// <summary>
    /// SteamVR head transform instance.
    /// </summary>
    public Transform HeadTransform;

    /// <summary>
    /// SteamVR camera rig instance.
    /// </summary>
    public Transform CameraRigTransform;

    /// <summary>
    /// <see cref="LayerMask"/> of the area where the player teleport to.
    /// </summary>
    public LayerMask TeleportMask;

    /// <summary>
    /// <see cref="LayerMask"/> of the area where the player can move on the tower.
    /// </summary>
    public LayerMask StandOnMask;

    private SteamVR_TrackedObject trackedObj;
    private GameObject laser;
    private GameObject reticle;
    private Transform laserTransform;
    private Transform teleportReticleTransform;
    private RaycastHit? lastHit;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start() {
        laser = Instantiate(LaserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(TeleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
        Debug.Log("Giant mode enabled.");
    }

    private void ShowLaser(RaycastHit hit) {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f);
        laserTransform.LookAt(hit.point);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y,
            hit.distance);
        reticle.SetActive(true);
        teleportReticleTransform.position = hit.point;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);

        if ((TeleportMask.value & hitMask) != 0) {
            laser.GetComponent<Renderer>().material.color = Color.blue;
        }
        else if ((StandOnMask.value & hitMask) != 0) {
            laser.GetComponent<Renderer>().material.color = Color.green;
        }
        else {
            laser.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void Teleport(RaycastHit hit) {
        var difference = CameraRigTransform.position - HeadTransform.position;
        difference.y = CameraRigTransform.position.y;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);
        if ((TeleportMask.value & hitMask) != 0) {
            difference.y -= hit.point.y;
            CameraRigTransform.position = hit.point + difference;
        }
        else if ((StandOnMask.value & hitMask) != 0) {
            CameraRigTransform.GetComponent<MovementChangeBehaviour>().MovementState = State.Tower;
            CameraRigTransform.position = hit.point;
            ChangeTowerRole(hit);
        }
    }

    private void ChangeTowerRole(RaycastHit hit) {
        var roleChangeBehaviour = CameraRigTransform.GetComponent<RoleChangeBehaviour>();
        if (roleChangeBehaviour) {
            roleChangeBehaviour.TowerRole = TagUtility.TagToTowerRole(hit.transform.tag);
        }
    }

    private void Update() {
        laser.SetActive(false);
        reticle.SetActive(false);
        if (!Controller.GetHairTrigger()) {
            if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
                if (Controller.GetAxis().y > 0) {
                    RaycastHit hit;

                    if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit,
                        float.PositiveInfinity)) {
                        ShowLaser(hit);
                        lastHit = hit;
                    }
                }
            }

            if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
                if (Controller.GetAxis().y > 0) {
                    if (lastHit.HasValue) {
                        Teleport(lastHit.Value);
                    }
                }
            }
        }
    }
}