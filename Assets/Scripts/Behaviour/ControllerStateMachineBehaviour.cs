using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerStateMachineBehaviour : MonoBehaviour
{
    public static BaseControllerProviderBehaviour LeftControllerProvider { get; private set; }
    public static BaseControllerProviderBehaviour RightControllerProvider { get; private set; }

    private ControllerState currentControllerState;

    void Start(){
        currentControllerState = GetComponent<GiantState>();
        currentControllerState.Setup();
    }

    void Update(){
        if (LeftControllerProvider == null || RightControllerProvider == null) {
            RecognizeController();
            return;
        }

        var newControllerMode = currentControllerState.Process(LeftControllerProvider, RightControllerProvider);
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
            LeftControllerProvider = leftControllerProvider;
            RightControllerProvider = rightControllerProvider;
        }
    }
}