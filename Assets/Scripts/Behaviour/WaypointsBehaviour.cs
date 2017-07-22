using UnityEngine;

/// <summary>
/// Represents a behaviour which gets all its children and saves them in a easily accessible array.
/// </summary>
public class WaypointsBehaviour : MonoBehaviour {
    /// <summary>
    /// All existing waypoints on the path. 
    /// </summary>
    public static Transform[] Points;

    private void Awake() {
        Points = new Transform[transform.childCount];
        for (var i = 0; i < transform.childCount; i++) {
            Points[i] = transform.GetChild(i);
        }
    }
}