using UnityEngine;

/// <summary>
/// Represents a behaviour which handles the communication between the <see cref="BowManagerBehaviour"/> and the
/// <see cref="ArrowBehaviour"/>.
/// </summary>
public class ArrowManagerBehaviour : MonoBehaviour {
    /// <summary>
    /// Shows if the currently active arrow is attached to the bow.
    /// </summary>
    public static bool IsArrowAttached;

    /// <summary>
    /// Gets the singleton instance of the <see cref="ArrowManagerBehaviour"/>. 
    /// </summary>
    public static ArrowManagerBehaviour Instance { get; private set; }

    /// <summary>
    /// Gets the <see cref="SteamVR_TrackedObject"/> which belongs to the current controller.
    /// </summary>
    public SteamVR_TrackedObject TrackedObj { get; private set; }

    /// <summary>
    /// Prefab of the arrow which should get spawned
    /// </summary>
    public GameObject ArrowPrefab;

    /// <summary>
    /// Current position of the controller with the attached arrow.
    /// </summary>
    public Vector3 ArrowControllerPosition { get; private set; }

    /// <summary>
    /// Position where the arrow is getting instantiated.
    /// </summary>
    public Vector3 ArrowStartPosition = new Vector3(3.275f, 0f, 0f);

    /// <summary>
    /// Offset to get arrow perfectly attached to the string of the bow.
    /// </summary>
    public float ArrowOffset = 1.342f;

    /// <summary>
    /// Modifier how hard it is to pull the string of the arrow back.
    /// </summary>
    public float StringStrengthModifier = 10f;

    /// <summary>
    /// Time till a new arrow is spawned in ms after the old one has been shoot.
    /// </summary>
    public int ArrowSpawnDelay = 1000;

    private const float MaxDistance = 6;
    private GameObject arrow;
    private readonly Vector3 stringPositionOffset = new Vector3(1.4f, 0f, 0f);
    private const float ArrowVelocityMultiplier = 6;
    private Timer arrowSpawnerTimer;
    private Timer hapticPulseTimer;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) TrackedObj.index); }
    }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void FixedUpdate() {
        if (!arrow)
            SpawnArrow();

        PullString();

        ArrowControllerPosition = transform.position;
    }

    private void SpawnArrow() {
        if (arrowSpawnerTimer == null || arrowSpawnerTimer.IsTimeUp()) {
            arrow = Instantiate(ArrowPrefab);
            arrow.transform.parent = TrackedObj.transform;
            arrow.transform.localPosition = new Vector3(0, 0, ArrowOffset);
            arrow.transform.rotation = TrackedObj.transform.rotation;
        }
    }

    /// <summary>
    /// Attaches the current arrow and the bow to each other.
    /// </summary>
    public void AttachArrowToBow() {
        var bowBehaviour = BowManagerBehaviour.Instance;
        if (bowBehaviour == null) return;

        arrow.transform.parent = bowBehaviour.String.transform;
        arrow.transform.localPosition = ArrowStartPosition;
        arrow.transform.rotation = bowBehaviour.Bow.transform.rotation;
        IsArrowAttached = true;
    }

    private void PullString() {
        if (!IsArrowAttached) return;

        var bowBehaviour = BowManagerBehaviour.Instance;
        if (bowBehaviour == null) return;

        var distance = Vector3.Distance(BowManagerBehaviour.Instance.TrackedObj.transform.position,
            TrackedObj.transform.position);
        distance *= StringStrengthModifier;
        distance = distance <= MaxDistance ? distance : MaxDistance;
        bowBehaviour.String.transform.localPosition = new Vector3(distance, 0f, 0f);

        if (Controller.GetHairTriggerUp()) {
            Fire(distance);
        }
    }

    private void ApplyHapticFeedback(float distance) {
        var pulseStrength = (int) distance * 200;
        Controller.TriggerHapticPulse((ushort) pulseStrength);
    }

    private void Fire(float distance) {
        var bowBehaviour = BowManagerBehaviour.Instance;
        if (bowBehaviour == null) return;

        arrow.transform.parent = null;
        var rigidbody = arrow.GetComponent<Rigidbody>();
        rigidbody.velocity = arrow.transform.forward * distance * ArrowVelocityMultiplier;
        rigidbody.useGravity = true;
        bowBehaviour.String.transform.localPosition = stringPositionOffset;
        arrow = null;
        IsArrowAttached = false;

        if (arrowSpawnerTimer == null) {
            arrowSpawnerTimer = new Timer(ArrowSpawnDelay, false);
        }
        else {
            arrowSpawnerTimer.Reset();
        }
    }

    private void OnDisable() {
        Destroy(arrow);
    }
}