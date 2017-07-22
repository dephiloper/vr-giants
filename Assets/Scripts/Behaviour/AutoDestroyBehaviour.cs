using UnityEngine;

/// <summary>
/// Represents a behaviour which outomaticly destroys the GameObject after <see cref="time"/> seconds.
/// </summary>
public class AutoDestroyBehaviour : MonoBehaviour {
    /// <summary>
    /// Time in seconds till the GameObject gets gestroyed.
    /// </summary>
    public float time;

    private void Start() {
        Destroy(gameObject, time);
    }
}