﻿using UnityEngine;

/// <summary>
/// Represents a behaviour which handles the movement on top of a tower.
/// </summary>
public class OnTowerMoveBehaviour : MonoBehaviour {
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
    /// <see cref="LayerMask"/> of the area where the player can move on the tower.
    /// </summary>
    public LayerMask StandOnMask;

    /// <summary>
    /// Last position while in giant mode.
    /// </summary>
    public Vector3 LastGiantPos;

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
        Debug.Log("OnTower mode enabled.");
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
        laser.GetComponent<Renderer>().material.color =
            (StandOnMask.value & hitMask) != 0 ? Color.blue : Color.red;
    }

    private void Teleport(RaycastHit hit) {
        var difference = CameraRigTransform.position - HeadTransform.position;
        //difference.y = CameraRigTransform.position.y;
        difference.y = 0;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);

        Debug.Log(string.Format("StandOnMask: {0}, HitMask: {1}", StandOnMask.value, hitMask));
        if ((StandOnMask.value & hitMask) != 0) {
            var currentRole = CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole;

            if (TagUtility.CompareRoleWithTowerTag(currentRole, hit.transform.tag)) {
                CameraRigTransform.position = hit.point;
                CameraRigTransform.position += difference;
            }
        }
    }

    private void Update() {
        laser.SetActive(false);
        reticle.SetActive(false);

        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (Controller.GetAxis().y > 0) {
                RaycastHit hit;

                if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit,
                    float.PositiveInfinity)) {
                    ShowLaser(hit);
                    lastHit = hit;
                }
            }
            else {
                // teleport back
                CameraRigTransform.position = LastGiantPos;
                CameraRigTransform.GetComponent<MovementChangeBehaviour>().MovementState = State.Giant;
                CameraRigTransform.GetComponent<RoleChangeBehaviour>().TowerRole = Role.None;
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