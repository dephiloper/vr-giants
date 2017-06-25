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
        //Debug.Log("i will press this shitty button. i dare you!");
        if (TagUtility.IsButton(other.gameObject.tag)) {
            if (Controller.GetHairTrigger()) {
                var quitButtonBehaviour = other.GetComponent<QuitButtonBehaviour>();
                var restartButtonBehaviour = other.GetComponent<RestartButtonBehaviour>();
                var tutorialButtonBehaviour = other.GetComponent<TutorialButtonBehaviour>();
                if (quitButtonBehaviour) {
                    quitButtonBehaviour.OnButtonPressed();
                } else if (restartButtonBehaviour) {
                    restartButtonBehaviour.OnButtonPressed();
                } else if (tutorialButtonBehaviour) {
                    transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Tutorial;
                    // ^ i feel dirty now
                    Debug.Log("i pressed this dirty button");
                }
            }
        }
    }
}
