﻿using UnityEngine;

public class MovementChangeBehaviour : MonoBehaviour {

    private State movementState;
    public State MovementState
    {
        get
        {
            return movementState;
        }
        set
        {
            switch (value)
            {
                case State.Giant:
                    ChangeState(true, false);
                    Debug.Log("State Giant");
                    break;
                case State.Tower:
                    ChangeState(false, true);
                    Debug.Log("State Tower");
                    break;
            }

            movementState = value;
        }
    }

    private void ChangeState(bool isGiant, bool isTower)
    {
        var giantMoveBehaviours = GetComponentsInChildren<GiantMoveBehaviour>();
        var towerMoveBehaviours = GetComponentsInChildren<OnTowerMoveBehaviour>();
        var grabBehaviours = GetComponentsInChildren<ControllerGrabObject>();
        var spawnTowerBehaviours = GetComponentsInChildren<SpawnTowerBehaviour>();

        foreach (var behaviour in giantMoveBehaviours)
        {
            behaviour.enabled = isGiant;
        }
        foreach (var behaviour in towerMoveBehaviours)
        {
            behaviour.enabled = isTower;
            if (isTower)
            {
                behaviour.lastGiantPos = transform.position;
            }
        }
        foreach (var behaviour in grabBehaviours)
        {
            behaviour.enabled = isTower;
        }
        foreach (var behaviour in spawnTowerBehaviours)
        {
            behaviour.enabled = isGiant;
        }
    }

    private void Start()
    {
        movementState = State.Giant;
    }


}

public enum State {
    Giant,
    Tower
}