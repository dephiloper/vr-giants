public class ControllerUtility
{
    private const float detectionThreshold = 0.5f;
    
    public enum Dpad
    {
        Up, Down, Left, Right, None
    }

    public static Dpad TouchpadDpadDetection(SteamVR_Controller.Device controller){
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (controller.GetAxis().x < - detectionThreshold) { 
                return Dpad.Left;
            }
            if (controller.GetAxis().x > detectionThreshold) { 
                return Dpad.Right;
            }
            if (controller.GetAxis().y > detectionThreshold) { 
                return Dpad.Up;
            }
            if (controller.GetAxis().y < - detectionThreshold) { 
                return Dpad.Down;
            }
        }

        return Dpad.None;
    }
}