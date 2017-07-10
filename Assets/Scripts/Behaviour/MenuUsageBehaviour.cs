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
                var restartButtonBehaviour = other.GetComponent<RestartButtonBehaviour>();
                var tutorialButtonBehaviour = other.GetComponent<TutorialButtonBehaviour>();
                if (quitButtonBehaviour){
                    quitButtonBehaviour.OnButtonPressed();
                } else if (restartButtonBehaviour) {
                    restartButtonBehaviour.OnButtonPressed();
                } else if (tutorialButtonBehaviour) {
                    if (MenuManagerBehaviour.Menu)
                        Destroy(MenuManagerBehaviour.Menu);
                    
                    transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Tutorial;
                }
            }
        }
    }
}
