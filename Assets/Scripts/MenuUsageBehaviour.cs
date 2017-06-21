using UnityEngine;

public class MenuUsageBehaviour : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input ((int)trackedObj.index); }
    }

    private void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject> ();
    }

    private void Start()
    {
        Debug.Log("Menu is open.");         
    }

    private void OnDestroy()
    {
        Debug.Log("Menu is closed.");         
    }

    private void OnTriggerEnter(Collider other){
        if (TagUtility.IsButton(other.gameObject.tag)) {
            if (Controller.GetHairTrigger()) {
                var quitButtonBehaviour = other.GetComponent<QuitButtonBehaviour>();
                if (quitButtonBehaviour) {
                    quitButtonBehaviour.OnButtonPressed();
                } else {
                    var restartButtonBehaviour = other.GetComponent<RestartButtonBehaviour>();
                    if (restartButtonBehaviour) {
                        restartButtonBehaviour.OnButtonPressed();
                    }
                }
            }
        }
    }
}
