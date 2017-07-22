using UnityEngine;

/// <summary>
/// Represents a behaviour which lets the given gameobject pulse.
/// </summary>
public class PulseBehaviour : MonoBehaviour {
    private void Start() {
        var r = GetComponent<MeshRenderer>();
        r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0.3f);
    }
}