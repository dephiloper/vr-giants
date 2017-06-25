using UnityEngine;

public class GiantState : ControllerState
{
    public override void Setup(){
        Debug.Log("GiantState - Setup()");
    }

    public override ControllerState Process(BaseControllerProviderBehaviour leftControllerProvider, BaseControllerProviderBehaviour rightControllerProvider){
        
        var newState = HandleControllerInput(leftControllerProvider);
        if (newState != null) {
            return newState;
        }
        
        newState = HandleControllerInput(rightControllerProvider);
        if (newState != null) {
            return newState;
        }
        
        return this;
    }

    private ControllerState HandleControllerInput(BaseControllerProviderBehaviour controllerProvider){
        if (controllerProvider.Controller.GetHairTriggerDown()) {
            {
                return GetComponent<EditState>();
            }
        }

        switch (ControllerUtility.TouchpadDpadPressDown(controllerProvider.Controller)) {
            case ControllerUtility.Dpad.Up:
            {
                return GetComponent<GiantMoveState>();
            }
        }
        
        // menu and tutorial button are missing
        
        return null;
    }

    public override void Dismantle(){
        Debug.Log("GiantState - Dismantle()");
    }
}