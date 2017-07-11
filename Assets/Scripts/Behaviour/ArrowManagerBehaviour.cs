using System;
using UnityEngine;

public class ArrowManagerBehaviour : MonoBehaviour {

    public static bool IsArrowAttached;
    public static ArrowManagerBehaviour Instance { get; private set; }
    public SteamVR_TrackedObject TrackedObj { get; private set; }
    public GameObject ArrowPrefab;
    public Vector3 ArrowControllerPosition { get; private set; }
    public Vector3 ArrowStartPosition = new Vector3(3.275f, 0f, 0f);
    public float ArrowOffset = 1.342f;
    public float StringStrengthModifier = 10f;
    public int ArrowSpawnDelay = 1000;

    private const float MaxDistance = 6;
    private GameObject arrow;
    private readonly Vector3 stringPositionOffset = new Vector3(1.4f, 0f, 0f);
    private const float ArrowVelocityMultiplier = 6;
    private Timer arrowSpawnerTimer;
    private Timer hapticPulseTimer;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)TrackedObj.index); }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void FixedUpdate () {
        if (!arrow)
            SpawnArrow();
        
        PullString();
        
        ArrowControllerPosition = transform.position;
    }

    private void SpawnArrow()
    {
        if (arrowSpawnerTimer == null || arrowSpawnerTimer.IsTimeUp())
        {
            arrow = Instantiate(ArrowPrefab);
            arrow.transform.parent = TrackedObj.transform;
            arrow.transform.localPosition = new Vector3(0, 0, ArrowOffset);
            arrow.transform.rotation = TrackedObj.transform.rotation;
        }
    }

    public void AttachArrowToBow()
    {
        var bowBehaviour = BowManagerBehaviour.Instance;
        if (bowBehaviour == null) return;

        arrow.transform.parent = bowBehaviour.String.transform;
        arrow.transform.localPosition = ArrowStartPosition;
        arrow.transform.rotation = bowBehaviour.Bow.transform.rotation;
        IsArrowAttached = true;
    }

    private void PullString()
    {
        if (!IsArrowAttached) return;
        
        var bowBehaviour = BowManagerBehaviour.Instance;
        if (bowBehaviour == null) return;

        var distance = Vector3.Distance(BowManagerBehaviour.Instance.TrackedObj.transform.position, TrackedObj.transform.position);
        distance*= StringStrengthModifier;
        distance = distance <= MaxDistance ? distance : MaxDistance;
        bowBehaviour.String.transform.localPosition = new Vector3(distance, 0f, 0f);

        // Implement later in a better and cooler way
        /*if (hapticPulseTimer == null || hapticPulseTimer.IsTimeUp())
        {
            ApplyHapticFeedback(distance);
            if (hapticPulseTimer == null)
            {
                hapticPulseTimer = new Timer(100, false);    
            } else 
            {
                hapticPulseTimer.Reset();
            }
        }*/
        
        if (Controller.GetHairTriggerUp())
        {
            Fire(distance);
        }
    }

    private void ApplyHapticFeedback(float distance)
    {
        var pulseStrength = (int)distance * 200;
        Controller.TriggerHapticPulse((ushort)pulseStrength);
    }

    // auf joints umbauen
    private void Fire(float distance)
    {
        var bowBehaviour = BowManagerBehaviour.Instance;
        if (bowBehaviour == null) return;

        arrow.transform.parent = null;
        var rigidbody = arrow.GetComponent<Rigidbody>();
        rigidbody.velocity = arrow.transform.forward * distance * ArrowVelocityMultiplier;
        rigidbody.useGravity = true;
        bowBehaviour.String.transform.localPosition = stringPositionOffset;
        arrow = null;
        IsArrowAttached = false;
        
        if (arrowSpawnerTimer == null)
        {
            arrowSpawnerTimer = new Timer(ArrowSpawnDelay, false);
        }
        else
        {
            arrowSpawnerTimer.Reset();
        }
    }


    private void OnDisable()
    {
        Destroy(arrow);
    }
}
