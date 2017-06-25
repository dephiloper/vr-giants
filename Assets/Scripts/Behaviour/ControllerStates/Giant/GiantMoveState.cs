using UnityEngine;

public class GiantMoveState : ControllerState
{
    
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
    
    public override void Setup(){
        Debug.Log("GiantMoveState - Setup()");
        
        laser = Instantiate(LaserPrefab);
        laserTransform = laser.transform;
        reticle = Instantiate (TeleportReticlePrefab);
        teleportReticleTransform = reticle.transform;
    }

    public override ControllerState Process(SteamVR_Controller.Device leftController,
        SteamVR_Controller.Device rightController){
        
        if (!leftController.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            return new GiantState();
        }
        
        
        
        return this;
    }

    public override void Dismantle(){
        Debug.Log("GiantMoveState - Dismantle()");
    }

    private void ShittyMonoBehaviourUpdate(){
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