using System.Collections.Generic;
using UnityEngine;

public abstract class ControllerState : MonoBehaviour
{
    private static readonly List<ControllerState> modes;

    private ControllerState controllerState;

    public ControllerState(){ }

    public abstract void Setup();

    public abstract ControllerState Process(SteamVR_Controller.Device leftController,
        SteamVR_Controller.Device rightController);

    public abstract void Dismantle();
}