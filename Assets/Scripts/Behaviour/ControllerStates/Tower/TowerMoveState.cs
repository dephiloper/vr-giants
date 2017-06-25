using UnityEngine;

public class TowerMoveState : ControllerState
{
    public override void Setup(){
        Debug.Log("TowerMoveState - Setup()");
    }

    public override ControllerState Process(BaseControllerProviderBehaviour leftControllerProvider, BaseControllerProviderBehaviour rightControllerProvider){
        if (rightControllerProvider.Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (rightControllerProvider.Controller.GetAxis().y < -0.5f) {
                // down
                return GetComponent<TowerState>();
            }
        }
        return this;
    }

    public override void Dismantle(){
        Debug.Log("TowerMoveState - Dismantle()");
    }
}