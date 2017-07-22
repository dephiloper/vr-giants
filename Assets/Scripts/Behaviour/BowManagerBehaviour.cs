using UnityEngine;

/// <summary>
/// Represents a behaviour which handles the communication between the <see cref="ArrowManagerBehaviour"/> and the
/// <see cref="ArrowBehaviour"/>.
/// </summary>
public class BowManagerBehaviour : MonoBehaviour {
    /// <summary>
    /// Gets the singleton instance of the <see cref="BowManagerBehaviour"/>.
    /// </summary>
    public static BowManagerBehaviour Instance { get; private set; }

    /// <summary>
    /// Gets the <see cref="SteamVR_TrackedObject"/> which belongs to the current controller.
    /// </summary>
    public SteamVR_TrackedObject TrackedObj { get; private set; }

    /// <summary>
    /// Prefab which represents the Bow.
    /// </summary>
    public GameObject BowPrefab;

    /// <summary>
    /// Gets the instance of the BowPrefab.
    /// </summary>
    public GameObject Bow { get; private set; }

    /// <summary>
    /// Gets the instance of the string inside the BowPrefab.
    /// </summary>
    public GameObject String { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

    private void Start() {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SpawnBow() {
        if (Bow != null) return;

        Bow = Instantiate(BowPrefab);
        String = Bow.transform.Find("Bow/main/string").gameObject;
        Bow.transform.parent = TrackedObj.transform;
        Bow.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void OnDisable() {
        Destroy(Bow);
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Update() {
        SpawnBow();
    }
}