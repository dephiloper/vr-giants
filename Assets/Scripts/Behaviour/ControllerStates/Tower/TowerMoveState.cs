using UnityEngine;

public class TowerMoveState : ControllerState
{
    public override void Setup(){
        Debug.Log("TowerMoveState - Setup()");
    }

    public override ControllerState Process(SteamVR_Controller.Device leftController,
        SteamVR_Controller.Device rightController){
        if (rightController.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (rightController.GetAxis().y < -0.5f) {
                // down
                return new TowerState();
            }
        }
        return this;
    }

    public override void Dismantle(){
        Debug.Log("TowerMoveState - Dismantle()");
    }
}