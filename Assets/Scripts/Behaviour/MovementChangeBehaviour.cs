using UnityEngine;

/// <summary>
/// Behaviour which handles the changing of states.
/// </summary>
public class MovementChangeBehaviour : MonoBehaviour {
    /// <summary>
    /// Gets and sets the current state. If the movement state gets changed it activates and deactivates the behaviours
    /// in the controller.
    /// </summary>
    public State MovementState {
        get { return movementState; }
        set {
            ChangeState(value);
            movementState = value;
        }
    }

    private State movementState;

    private void ChangeState(State value) {
        var giantMoveBehaviours = GetComponentsInChildren<GiantMoveBehaviour>();
        var towerMoveBehaviours = GetComponentsInChildren<OnTowerMoveBehaviour>();
        var spawnTowerBehaviours = GetComponentsInChildren<SpawnTowerBehaviour>();
        var menuUsageBehaviours = GetComponentsInChildren<MenuUsageBehaviour>();
        var tutorialNavigationBehaviours = GetComponentsInChildren<TutorialNavigationBehaviour>();

        foreach (var behaviour in giantMoveBehaviours) {
            behaviour.enabled = value == State.Giant;
        }
        foreach (var behaviour in towerMoveBehaviours) {
            behaviour.enabled = value == State.Tower;
            if (value == State.Tower) {
                behaviour.LastGiantPos = transform.position;
            }
        }
        foreach (var behaviour in spawnTowerBehaviours) {
            behaviour.enabled = value == State.Giant;
        }
        foreach (var behaviour in menuUsageBehaviours) {
            behaviour.enabled = value == State.Menu;
        }
        foreach (var behaviour in tutorialNavigationBehaviours) {
            behaviour.enabled = value == State.Tutorial;
        }
    }

    private void Start() {
        MovementState = State.Tutorial;
    }
}

/// <summary>
/// Represents the states which are possible in the game.
/// </summary>
public enum State {
    Giant,
    Tower,
    Menu,
    Tutorial
}