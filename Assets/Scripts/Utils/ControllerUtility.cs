public class ControllerUtility
{
    private const float DetectionThreshold = 0.5f;
    
    public enum Dpad
    {
        Up, Down, Left, Right, None
    }

    public static Dpad TouchpadDpadPressDown(SteamVR_Controller.Device controller){
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            return DetectDpadDirection(controller);
        }

        return Dpad.None;
    }
    
    public static Dpad TouchpadDpadPressUp(SteamVR_Controller.Device controller){
        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad)) {
            return DetectDpadDirection(controller);
        }

        return Dpad.None;
    }
    
    public static Dpad TouchpadDpadPress(SteamVR_Controller.Device controller){
        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad)) {
            return DetectDpadDirection(controller);
        }

        return Dpad.None;
    }
    
    private static Dpad DetectDpadDirection(SteamVR_Controller.Device controller){
        if (controller.GetAxis().x < -DetectionThreshold) {
            return Dpad.Left;
        }
        if (controller.GetAxis().x > DetectionThreshold) {
            return Dpad.Right;
        }
        if (controller.GetAxis().y > DetectionThreshold) {
            return Dpad.Up;
        }
        if (controller.GetAxis().y < -DetectionThreshold) {
            return Dpad.Down;
        }
        return Dpad.None;
    }

}