using System;
using UnityEngine;

public class RoleChangeBehaviour : MonoBehaviour {

    public Role TowerRole
    {
        get
        {
            return towerRole;
        }
        set
        {
            ChangeRole(value);
            towerRole = value;
        }
    }
    private Role towerRole;


    private void ChangeRole(Role value)
    {
        var grabBehaviours = GetComponentsInChildren<ControllerGrabObject>();
        var arrowManagerBehaviour = GetComponentInChildren<ArrowManagerBehaviour>();
        var bowManagerBehaviour = GetComponentInChildren<BowManagerBehaviour>();

        foreach (var behaviour in grabBehaviours)
        {
            behaviour.enabled = (value == Role.BrickBoy);
        }

        bowManagerBehaviour.enabled = arrowManagerBehaviour.enabled = (value == Role.Archer);
    }

    // Use this for initialization
    void Start () {
        towerRole = Role.None;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum Role
{
    Archer,
    Mage,
    BrickBoy,
    None
}
