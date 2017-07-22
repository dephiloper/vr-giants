using UnityEngine;

/// <summary>
/// Represents a behaviour which lets the given gameobject fly away.
/// </summary>
public class TowerFlyBehaviour : MonoBehaviour {
    private float velocity = 0.02f;
    private const float AccelerationMultiplier = 2f;
    private const float CeilingHeight = 60;

    private void FixedUpdate() {
        if (transform.position.y < CeilingHeight) {
            transform.position += (Vector3.up * velocity);
        }
        else {
            Destroy(gameObject);
        }
    }
}