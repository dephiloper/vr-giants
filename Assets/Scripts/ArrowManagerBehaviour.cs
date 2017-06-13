using System;
using UnityEngine;

public class ArrowManagerBehaviour : MonoBehaviour {

    public static bool IsArrowAttached;
    public static ArrowManagerBehaviour Instance { get; private set; }

    public GameObject ArrowPrefab;
    public float ArrowOffset = 0.342f;
    public Vector3 ArrowStartPosition = new Vector3(3.275f, 0f, 0f);
    public float StringStrengthModifier = 10f;
    public SteamVR_TrackedObject TrackedObj { get; private set; }
    public GameObject ArrowController { get; private set; }

    private GameObject arrow;
    private Vector3 stringPositionOffset = new Vector3(1.4f, 0f, 0f);

    private SteamVR_Controller.Device controller
    {
        get { return SteamVR_Controller.Input((int)TrackedObj.index); }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        ArrowController = transform.gameObject;
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    void Start () {
    }

    void Update () {
        AttachArrow();
        PullString();
    }

    private void AttachArrow()
    {
        if (arrow == null)
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
        if (IsArrowAttached)
        {
            var bowBehaviour = BowManagerBehaviour.Instance;
            if (bowBehaviour == null) return;

            var distance = Vector3.Distance(bowBehaviour.String.transform.position, TrackedObj.transform.position);
            distance*= StringStrengthModifier;
            bowBehaviour.String.transform.localPosition = new Vector3(distance, 0f, 0f);
            if (distance > 6) // magic number
                distance = 6;

            if (controller.GetHairTriggerUp())
            {
                Fire();
            }
        }
    }

    // auf joints umbauen
    private void Fire()
    {
        var bowBehaviour = BowManagerBehaviour.Instance;
        if (bowBehaviour == null) return;

        arrow.transform.parent = null;
        var rigidbody = arrow.GetComponent<Rigidbody>();
        rigidbody.velocity = arrow.transform.forward * 30f; // magic number
        rigidbody.useGravity = true;
        bowBehaviour.String.transform.localPosition = stringPositionOffset;
        arrow = null;
        IsArrowAttached = false;
    }

    void OnDisable()
    {
        Destroy(arrow);
    }
}
