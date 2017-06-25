using UnityEngine;

public abstract class BaseControllerProviderBehaviour : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    public SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
}