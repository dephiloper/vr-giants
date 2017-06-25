using UnityEngine;

public abstract class BaseControllerProviderBehaviour : MonoBehaviour
{
    public SteamVR_TrackedObject TrackedObj;

    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int) TrackedObj.index); }
    }

    private void Awake(){
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }
}