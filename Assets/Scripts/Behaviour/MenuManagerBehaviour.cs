using UnityEngine;
using Valve.VR;

/// <summary>
/// Represents a behaviour which handles the spawning of the game menu.
/// </summary>
public class MenuManagerBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefab which gets instantiated as game menu.
    /// </summary>
    public GameObject MenuPrefab;

    /// <summary>
    /// Instance of the currently active menu.
    /// </summary>
    public static GameObject Menu;

    private SteamVR_TrackedObject trackedObj;
    private readonly Vector3 menuPosition = new Vector3(0, 0.05f, 0.05f);

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update() {
        if (Controller.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu)) {
            ToggleDisplayMenu();
        }
    }

    private void ToggleDisplayMenu() {
        // extract code into change behaviour
        var movementChangeBehaviour = transform.parent.GetComponent<MovementChangeBehaviour>();
        if (movementChangeBehaviour.MovementState == State.Tower
            || movementChangeBehaviour.MovementState == State.Tutorial) return;

        if (!Menu) {
            Menu = Instantiate(MenuPrefab, transform);
            Menu.transform.localPosition += menuPosition;
            Menu.transform.parent = transform;
            Menu.transform.localRotation = Quaternion.Euler(60, 0, 0);

            // Change State to Menu
            transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Menu;
        }
        else {
            Destroy(Menu);
            transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Giant;
        }
    }
}