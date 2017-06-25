using System;
using UnityEngine;

public partial class TowerState : ControllerState
{
    public override void Setup(){
        Debug.Log("TowerState - Setup()");
    }

    public override ControllerState Process(SteamVR_Controller.Device leftController, SteamVR_Controller.Device rightController){
    
        if (rightController.GetHairTriggerDown()) {
            return new GiantState();
        }
        
        switch (ControllerUtility.TouchpadDpadDetection(rightController)) {
            case ControllerUtility.Dpad.Up:
                return new MageAttackState();
            case ControllerUtility.Dpad.Down:
                return new TowerMoveState();
            case ControllerUtility.Dpad.Left: 
                return new BrickBoyAttackState();
            case ControllerUtility.Dpad.Right:
                return new ArcherAttackState();
        }

        return this;
    }

    public override void Dismantle(){
        Debug.Log("TowerState - Dismantle()");
    }
}