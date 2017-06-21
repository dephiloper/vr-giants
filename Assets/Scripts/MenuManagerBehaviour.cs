using UnityEngine;
using Valve.VR;

public class MenuManagerBehaviour : MonoBehaviour
{
    public GameObject MenuPrefab;

    private SteamVR_TrackedObject trackedObj;
    private readonly Vector3 menuPosition = new Vector3(0, 0.05f, 0.05f);
    private static GameObject menu;
    private State lastState;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake(){
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update(){
        if (Controller.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu)) {
            ToggleDisplayMenu();
            var menuUsageBehaviours = GetComponentsInChildren<MenuUsageBehaviour>();
            foreach (var behaviour in menuUsageBehaviours)
            {
                behaviour.enabled = menu != null;
            }
        }
    }

    private void ToggleDisplayMenu(){
        var movementChangeBehaviour = transform.parent.GetComponent<MovementChangeBehaviour>();
        if (movementChangeBehaviour.MovementState == State.Tower) return;
        
        if (!menu) {
            menu = Instantiate(MenuPrefab, transform);
            menu.transform.localPosition += menuPosition;
            menu.transform.parent = transform;
            menu.transform.localRotation = Quaternion.Euler(60, 0, 0);

            lastState = movementChangeBehaviour.MovementState;
            movementChangeBehaviour.MovementState = State.Menu;
        }
        else {
            Destroy(menu);
            movementChangeBehaviour.MovementState = lastState;
        }
    }
}