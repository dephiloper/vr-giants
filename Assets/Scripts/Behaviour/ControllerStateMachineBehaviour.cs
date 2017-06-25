using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStateMachineBehaviour : MonoBehaviour
{
    public static SteamVR_Controller.Device LeftController { get; private set; }
    public static SteamVR_Controller.Device RightController { get; private set; }

    private ControllerState currentControllerState;

    void Start(){
        currentControllerState = new GiantState();
        currentControllerState.Setup();
    }

    void Update(){
        if (LeftController == null || RightController == null) {
            RecognizeController();
            return;
        }

        var newControllerMode = currentControllerState.Process(LeftController, RightController);
        if (newControllerMode != currentControllerState) {
            currentControllerState.Dismantle();
            currentControllerState = newControllerMode;
            currentControllerState.Setup();
        }
    }

    // is there a better way??
    private void RecognizeController(){
        var leftControllerProvider = GetComponentInChildren<LeftControllerProviderBehaviour>();
        var rightControllerProvider = GetComponentInChildren<RightControllerProviderBehaviour>();
        if (leftControllerProvider && rightControllerProvider) {
            LeftController = leftControllerProvider.Controller;
            RightController = rightControllerProvider.Controller;
        }
    }
}