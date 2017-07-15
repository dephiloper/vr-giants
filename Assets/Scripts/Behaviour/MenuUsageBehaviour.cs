using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUsageBehaviour : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input ((int)trackedObj.index); }
    }

    private void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject> ();
    }

    private void OnTriggerEnter(Collider other)
    {
        var colliderTag = other.gameObject.tag;
        
        if (TagUtility.IsButton(colliderTag)) 
        {
            if (Controller.GetHairTrigger())
            {
                if (TagUtility.IsTutorialButton(colliderTag) && IsGameScene())
                {
                    if (MenuManagerBehaviour.Menu) {
                        Destroy(MenuManagerBehaviour.Menu);
                    }
                    transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Tutorial;
                }
                else if (TagUtility.IsRestartButton(colliderTag))
                {
                    SteamVR_LoadLevel.Begin("Game");
                }
                else if (TagUtility.IsQuitButton(colliderTag))
                {
                    Application.Quit();
                }
            }
        }
    }

    private bool IsGameScene()
    {
        var activeScene = SceneManager.GetActiveScene();
        return     activeScene.name.Equals("Game");
    }
}
