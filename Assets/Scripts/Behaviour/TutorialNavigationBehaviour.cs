using UnityEngine;

/// <summary>
/// Behaviour which handles the enabling of the tutorial planes.
/// </summary>
public class TutorialNavigationBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefabs which gets instantiated and holds the tutorial planes.
    /// </summary>
    public GameObject TutorialSpacePrefab;

    /// <summary>
    /// SteamVR eye camera instance.
    /// </summary>
    public GameObject EyeCameraInstance;

    private SteamVR_TrackedObject trackedObj;
    private static GameObject tutorialSpace;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start() {
        if (!tutorialSpace) {
            tutorialSpace = Instantiate(TutorialSpacePrefab, transform.parent);
        }
    }

    private void Update() {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
            if (Controller.GetAxis().x > 0.5f) {
                TutorialBehaviour.Instance.NextTutorialPage();
            }
            else if (Controller.GetAxis().x < -0.5f) {
                TutorialBehaviour.Instance.PreviousTutorialPage();
            }
            else if (Controller.GetAxis().y < -0.5f) {
                TutorialBehaviour.Instance.ExitTutorial();
                transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Giant;
            }
        }
    }

    private void OnEnable() {
        Start();
    }
}