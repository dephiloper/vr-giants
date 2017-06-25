using UnityEngine;

public class GiantState : ControllerState
{
    public override void Setup(){
        Debug.Log("GiantState - Setup()");
    }

    public override ControllerState Process(SteamVR_Controller.Device leftController,
        SteamVR_Controller.Device rightController){
        
        var newState = HandleControllerInput(leftController);
        if (newState != null) {
            return newState;
        }
        
        newState = HandleControllerInput(rightController);
        if (newState != null) {
            return newState;
        }
        
        return this;
    }

    private ControllerState HandleControllerInput(SteamVR_Controller.Device controller){
        if (controller.GetHairTriggerDown()) {
            {
                return new EditState();
            }
        }

        switch (ControllerUtility.TouchpadDpadDetection(controller)) {
            case ControllerUtility.Dpad.Up:
            {
                return new GiantMoveState();
            }
        }
        
        // menu and tutorial button are missing
        
        return null;
    }

    public override void Dismantle(){
        Debug.Log("GiantState - Dismantle()");
    }
}