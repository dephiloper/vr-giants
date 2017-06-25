using UnityEngine;

public class TowerState : ControllerState
{
    public override void Setup(){
        Debug.Log("TowerState - Setup()");
    }

    public override ControllerState Process(BaseControllerProviderBehaviour leftControllerProvider, BaseControllerProviderBehaviour rightControllerProvider){
    
        if (rightControllerProvider.Controller.GetHairTriggerDown()) {
            return GetComponent<GiantState>();
        }
        
        switch (ControllerUtility.TouchpadDpadPressDown(rightControllerProvider.Controller)) {
            case ControllerUtility.Dpad.Up:
                return GetComponent<MageAttackState>();
            case ControllerUtility.Dpad.Down:
                return GetComponent<TowerMoveState>();
            case ControllerUtility.Dpad.Left: 
                return GetComponent<BrickBoyAttackState>();
            case ControllerUtility.Dpad.Right:
                return GetComponent<ArcherAttackState>();
        }

        return this;
    }

    public override void Dismantle(){
        Debug.Log("TowerState - Dismantle()");
    }
}