using UnityEngine;

public class ThrowObjectsBehaviour : MonoBehaviour {

    public GameObject ThrowPrefab;
    private GameObject objectInHand;
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update () {
        if (Controller.GetHairTriggerDown())
        {
            if (!objectInHand)
            { 
                objectInHand = Instantiate(ThrowPrefab, trackedObj.transform.position, Quaternion.identity);
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
        var fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
}
