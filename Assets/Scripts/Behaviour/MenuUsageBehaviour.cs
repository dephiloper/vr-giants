using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a behaviour which gives the player a way to interact with the game menu.
/// </summary>
public class MenuUsageBehaviour : MonoBehaviour {
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void OnTriggerEnter(Collider other) {
        var colliderTag = other.gameObject.tag;

        if (TagUtility.IsButton(colliderTag)) {
            if (Controller.GetHairTrigger()) {
                if (TagUtility.IsTutorialButton(colliderTag) && IsGameScene()) {
                    if (MenuManagerBehaviour.Menu) {
                        Destroy(MenuManagerBehaviour.Menu);
                    }
                    transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Tutorial;
                }
                else if (TagUtility.IsRestartButton(colliderTag)) {
                    PlayerPrefs.DeleteAll();
                    SteamVR_LoadLevel.Begin("Game");
                }
                else if (TagUtility.IsQuitButton(colliderTag)) {
                    PlayerPrefs.DeleteAll();
                    Application.Quit();
                }
            }
        }
    }

    private static bool IsGameScene() {
        var activeScene = SceneManager.GetActiveScene();
        return activeScene.name.Equals("Game");
    }
}