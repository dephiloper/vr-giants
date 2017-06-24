using UnityEngine;

public class MovementChangeBehaviour : MonoBehaviour {


    public State MovementState
    {
        get
        {
            return movementState;
        }
        set
        {
            ChangeState(value);
            movementState = value;
        }
    }
    private State movementState;

    private void ChangeState(State value)
    {
        var giantMoveBehaviours = GetComponentsInChildren<GiantMoveBehaviour>();
        var towerMoveBehaviours = GetComponentsInChildren<OnTowerMoveBehaviour>();
        var spawnTowerBehaviours = GetComponentsInChildren<SpawnTowerBehaviour>();
        var menuUsageBehaviours = GetComponentsInChildren<MenuUsageBehaviour>();
        var tutorialNavigationBehaviours = GetComponentsInChildren<TutorialNavigationBehaviour>();
       
        foreach (var behaviour in giantMoveBehaviours)
        {
            behaviour.enabled = value == State.Giant;
        }
        foreach (var behaviour in towerMoveBehaviours)
        {
            behaviour.enabled = value == State.Tower;
            if (value == State.Tower)
            {
                behaviour.LastGiantPos = transform.position;
            }
        }
        foreach (var behaviour in spawnTowerBehaviours)
        {
            behaviour.enabled = value == State.Giant;
        }
        foreach (var behaviour in menuUsageBehaviours)
        {
            behaviour.enabled = value == State.Menu;
        }
        foreach (var behaviour in tutorialNavigationBehaviours)
        {
            behaviour.enabled = value == State.Tutorial;
        }
    }

    private void Start()
    {
        MovementState = State.Tutorial;
    }
}

public enum State {
    Giant,
    Tower,
    Menu,
    Tutorial
}
