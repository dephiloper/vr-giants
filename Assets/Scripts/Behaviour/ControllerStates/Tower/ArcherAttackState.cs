using UnityEngine;

public class ArcherAttackState : ControllerState
{
    public override void Setup(){
        Debug.Log("ArcherAttackState - Setup()");
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
        Debug.Log("ArcherAttackState - Dismantle()");
    }
}