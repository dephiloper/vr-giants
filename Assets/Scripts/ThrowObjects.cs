using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjects : MonoBehaviour {

    public GameObject throwPrefab;
    private GameObject objectInHand;
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start () {
        
    }

    void Update () {
        if (Controller.GetHairTriggerDown())
        {
            if (!objectInHand)
            { 
                objectInHand = Instantiate(throwPrefab, trackedObj.transform.position, Quaternion.identity);
                var joint = AddFixedJoint();
                joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
            }
        }

        if (Controller.GetHairTriggerUp())
        {
            if (GetComponent<FixedJoint>())
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());
                objectInHand.GetComponent<Rigidbody>().AddForce(Controller.velocity, ForceMode.Impulse);
                objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
                objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
            }

            objectInHand = null;
        }
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000; // welcher wert auch sonst
        fx.breakTorque = 20000;
        return fx;
    }
}
