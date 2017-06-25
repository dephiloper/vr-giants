using UnityEngine;

public class GiantMoveState : ControllerState
{
    public GameObject TeleportReticlePrefab;
    public GameObject LaserPrefab;
    public Transform HeadTransform;
    public Transform CameraRigTransform;
    public LayerMask TeleportMask;
    public LayerMask StandOnMask;

    private readonly GameObject[] laser = new GameObject[2];
    private readonly GameObject[] reticle = new GameObject[2];
    private readonly RaycastHit[] lastHit = new RaycastHit[2];

    public override void Setup(){
        Debug.Log("GiantMoveState - Setup()");

        laser[0] = Instantiate(LaserPrefab);
        laser[1] = Instantiate(LaserPrefab);
        reticle[0] = Instantiate(TeleportReticlePrefab);
        reticle[1] = Instantiate(TeleportReticlePrefab);
    }

    public override ControllerState Process(BaseControllerProviderBehaviour leftControllerProvider, BaseControllerProviderBehaviour rightControllerProvider){
        var newControllerState = HandleInput(leftControllerProvider, 0);
        if (!newControllerState) {
            return newControllerState;
        }

        /*newControllerState = HandleInput(rightControllerProvider, 1);
        if (!newControllerState) {
            return newControllerState;
        }*/

        return this;
    }

    private ControllerState HandleInput(BaseControllerProviderBehaviour controllerProvider, int index){
        if (ControllerUtility.TouchpadDpadPress(controllerProvider.Controller) == ControllerUtility.Dpad.Up) {
            RaycastHit hit;
            if (Physics.Raycast(controllerProvider.TrackedObj.transform.position,
                controllerProvider.TrackedObj.transform.forward, out hit, float.PositiveInfinity)) {

                MoveUtility.ShowLaser(laser[index], reticle[index], hit,
                    controllerProvider.TrackedObj.transform.position, TeleportMask, Color.blue, Color.red);

                lastHit[index] = hit;
            }
            else {
                laser[index].SetActive(false);
                reticle[index].SetActive(false);
            }
        }
        else {
            laser[index].SetActive(false);
            reticle[index].SetActive(false);
            var difference = CameraRigTransform.position - HeadTransform.position;
            difference.y = CameraRigTransform.position.y;
            if (MoveUtility.CanTeleport(CameraRigTransform, lastHit[index], difference, TeleportMask)) {
                CameraRigTransform.position = lastHit[index].point + difference;
            }
        }
       
        return this;
    }



    public override void Dismantle(){
        Debug.Log("GiantMoveState - Dismantle()");
    }
}