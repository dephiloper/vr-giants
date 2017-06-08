using System;
using UnityEngine;

public class ArrowManagerBehaviour : MonoBehaviour {

    public GameObject arrowPrefab;
    public GameObject stringAttachPoint;
    public float arrowOffset = 0.342f;
    public Vector3 arrowStartPosition = new Vector3(3.275f, 0f, 0f);
    public Vector3 stringPositionOffset = new Vector3(0f, 0f, -0.1416228f);
    public GameObject bow;
    public float stringStrengthModifier = 7;
    public SteamVR_TrackedObject TrackedObj { get; private set; }

    public static ArrowManagerBehaviour Instance { get; private set; }

    private Vector3 stringStartPosition;
    private GameObject arrow;
    private bool isArrowAttached;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)TrackedObj.index); }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
        stringStartPosition = stringAttachPoint.transform.localPosition;
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
            arrow = Instantiate(arrowPrefab);
            arrow.transform.parent = TrackedObj.transform;
            arrow.transform.localPosition = new Vector3(0, 0, arrowOffset);
            arrow.transform.rotation = TrackedObj.transform.rotation;
        }
    }

    public void AttachArrowToBow()
    {
        arrow.transform.parent = stringAttachPoint.transform;
        arrow.transform.localPosition = arrowStartPosition;
        arrow.transform.rotation = bow.transform.rotation;
        isArrowAttached = true;
    }

    private void PullString()
    {
        if (isArrowAttached)
        {
            // stringAttachPoint ist merkwürdig. richtige variable??
            var distance = Vector3.Distance(stringAttachPoint.transform.position, TrackedObj.transform.position) * stringStrengthModifier;
            stringAttachPoint.transform.localPosition = stringPositionOffset + new Vector3(distance, 0, 0);
            Debug.Log(distance);

            if (Controller.GetHairTriggerUp())
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
        stringAttachPoint.transform.localPosition = stringStartPosition;
        arrow = null;
        isArrowAttached = false;
    }
}
