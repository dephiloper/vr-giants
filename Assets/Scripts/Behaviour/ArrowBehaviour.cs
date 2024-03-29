﻿using UnityEngine;

/// <summary>
/// Represents a behaviour which corresponds the arrow in the game. 
/// </summary>
public class ArrowBehaviour : MonoBehaviour {
    /// <summary>
    /// CenterOfMass the arrow uses.
    /// </summary>
    public GameObject CenterOfMass;

    /// <summary>
    /// Damage the arrow should make on a hit.
    /// </summary>
    public float AttackDamage = 5;

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = ArrowManagerBehaviour.Instance.TrackedObj;
    }

    private void OnTriggerEnter(Collider other) {
        AttachArrow(other);

        if (TagUtility.IsShootableEntity(other.gameObject.tag)) {
            var healthBehaviour = other.GetComponent<HealthBehaviour>();
            if (healthBehaviour) {
                healthBehaviour.ReceiveDamage(Role.Archer, AttackDamage);
                transform.parent = healthBehaviour.transform;
            }

            var boxCollider = GetComponent<BoxCollider>();
            boxCollider.enabled = false;
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.isKinematic = true;
            rigidBody.velocity = Vector3.zero;

            Destroy(gameObject, 3f);
        }
    }

    private void OnTriggerStay(Collider other) {
        AttachArrow(other);
    }

    private void AttachArrow(Component other) {
        if (Controller.GetHairTrigger()) {
            if (TagUtility.IsAttachable(other.gameObject.tag)) {
                ArrowManagerBehaviour.Instance.AttachArrowToBow();
            }
        }
    }
}