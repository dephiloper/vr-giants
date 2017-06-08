using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private bool isAttachable; // weird workaround

    private void Awake()
    {
        trackedObj = ArrowManagerBehaviour.Instance.TrackedObj;
    }

    void Update() { 
        if (!isAttachable) { 
            isAttachable = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAttachable) { 
            AttachArrow();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAttachable)
        {
            AttachArrow();
        }
    }

    private void AttachArrow()
    {
        if (Controller.GetHairTriggerDown()) {
            ArrowManagerBehaviour.Instance.AttachArrowToBow();
        }
    }
}
