using UnityEngine;

public class EditState : ControllerState
{
    public override void Setup(){
        Debug.Log("EditState - Setup()");
    }

    public override ControllerState Process(SteamVR_Controller.Device leftController,
        SteamVR_Controller.Device rightController){
        if (leftController.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (leftController.GetAxis().y < -0.5f) {
                return new GiantState();
            }
        }
        return this;
    }

    public override void Dismantle(){
        Debug.Log("EditState - Dismantle()");
    }
}