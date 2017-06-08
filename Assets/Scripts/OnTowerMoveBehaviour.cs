﻿using UnityEngine;

public class OnTowerMoveBehaviour : MonoBehaviour {

    public GameObject teleportReticlePrefab;
    public GameObject laserPrefab;
    public Transform headTransform;
    public Transform cameraRigTransform;
    public LayerMask standOnMask;
    public Vector3 lastGiantPos;

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

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start()
    {
        laser = Instantiate(laserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate(teleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
        Debug.Log("OnTower mode enabled.");
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
        if (standOnMask.value == hitMask)
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
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = cameraRigTransform.position.y;

        int hitMask = BitPositionToMask(hit.transform.gameObject.layer);
        if (standOnMask.value == hitMask)
        {
            cameraRigTransform.position = hit.point;
        }
    }

    void Update()
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
                cameraRigTransform.position = lastGiantPos;
                cameraRigTransform.GetComponent<MovementChangeBehaviour>().MovementState = State.Giant;
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
