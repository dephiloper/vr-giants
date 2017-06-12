using System;
using UnityEngine;

public class ArrowManagerBehaviour : MonoBehaviour {

    public static bool IsArrowAttached;
    public static ArrowManagerBehaviour Instance { get; private set; }

    public GameObject ArrowPrefab;
    public float ArrowOffset = 0.342f;
    public Vector3 ArrowStartPosition = new Vector3(3.275f, 0f, 0f);
    public Vector3 StringPositionOffset = new Vector3(.4f, 0f, 0f);
    public float StringStrengthModifier = 5f;
    public SteamVR_TrackedObject TrackedObj { get; private set; }
    public GameObject ArrowController { get; private set; }

    private Vector3 stringStartPosition;
    private GameObject arrow;
    private GameObject bowString;
    private GameObject bow;

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
        stringStartPosition = StringPositionOffset;
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
        bow = BowManagerBehaviour.Instance.Bow;
        bowString = BowManagerBehaviour.Instance.String;
        arrow.transform.parent = bowString.transform;
        arrow.transform.localPosition = ArrowStartPosition;
        //Debug.Log("arrow rot: " + arrow.transform.rotation);
        //Debug.Log("bow rot: " + bow.transform.rotation);
        arrow.transform.rotation = bow.transform.rotation;
        IsArrowAttached = true;
    }

    private void PullString()
    {
        if (IsArrowAttached)
        {
            var distance = Vector3.Distance(bowString.transform.position, TrackedObj.transform.position);
            distance*= StringStrengthModifier;
            bowString.transform.localPosition = stringStartPosition + new Vector3(distance, 0f, 0f);
            if (distance > 6)
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
        arrow.transform.parent = null;
        var rigidbody = arrow.GetComponent<Rigidbody>();
        rigidbody.velocity = arrow.transform.forward * 30f; // magic number
        rigidbody.useGravity = true;
        bowString.transform.localPosition = stringStartPosition;
        arrow = null;
        IsArrowAttached = false;
    }

    void OnDisable()
    {
        Destroy(arrow);
    }
}
