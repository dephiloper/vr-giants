using UnityEngine;

public class ThrowObjects : MonoBehaviour {

    public GameObject ThrowPrefab;
    private GameObject ObjectInHand;
    private SteamVR_TrackedObject TrackedObj;

    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)TrackedObj.index);
        }
    }

    void Awake()
    {
        TrackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Start () {
        
    }

    void Update () {
        if (controller.GetHairTriggerDown())
        {
            if (!ObjectInHand)
            { 
                ObjectInHand = Instantiate(ThrowPrefab, TrackedObj.transform.position, Quaternion.identity);
                var joint = AddFixedJoint();
                joint.connectedBody = ObjectInHand.GetComponent<Rigidbody>();
            }
        }

        if (controller.GetHairTriggerUp())
        {
            if (GetComponent<FixedJoint>())
            {
                GetComponent<FixedJoint>().connectedBody = null;
                Destroy(GetComponent<FixedJoint>());
                ObjectInHand.GetComponent<Rigidbody>().AddForce(controller.velocity, ForceMode.Impulse);
                ObjectInHand.GetComponent<Rigidbody>().velocity = controller.velocity;
                ObjectInHand.GetComponent<Rigidbody>().angularVelocity = controller.angularVelocity;
            }

            ObjectInHand = null;
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
