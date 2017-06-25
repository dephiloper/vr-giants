using UnityEngine;

public class EditState : ControllerState
{
    public override void Setup(){
        Debug.Log("EditState - Setup()");
    }

    public override ControllerState Process(BaseControllerProviderBehaviour leftControllerProvider,
        BaseControllerProviderBehaviour rightControllerProvider){
        if (leftControllerProvider.Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (leftControllerProvider.Controller.GetAxis().y < -0.5f) {
                return GetComponent<GiantState>();
            }
        }
        return this;
    }

    public override void Dismantle(){
        Debug.Log("EditState - Dismantle()");
    }
}