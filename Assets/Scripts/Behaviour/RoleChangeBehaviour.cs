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
        var grabBehaviours = GetComponentsInChildren<BrickGrabBehaviour>();
        var arrowManagerBehaviour = GetComponentInChildren<ArrowManagerBehaviour>();
        var bowManagerBehaviour = GetComponentInChildren<BowManagerBehaviour>();
        var spellCastDetectionBehaviour = GetComponentsInChildren<SpellCastDetectionBehaviour>();
        
        foreach (var behaviour in grabBehaviours)
        {
            behaviour.enabled = (value == Role.BrickBoy);
        }
        
        foreach (var behaviour in spellCastDetectionBehaviour)
        {
            behaviour.enabled = (value == Role.Mage);
        }

        if (bowManagerBehaviour)
        { 
            bowManagerBehaviour.enabled = (value == Role.Archer);
        }
        if (arrowManagerBehaviour)
        { 
            arrowManagerBehaviour.enabled = (value == Role.Archer);
        }
    }

    private void Start () {
        towerRole = Role.None;
	}
	
}

public enum Role
{
    Archer,
    Mage,
    BrickBoy,
    None
}
