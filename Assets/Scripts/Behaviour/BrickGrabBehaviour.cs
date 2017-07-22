using UnityEngine;

/// <summary>
/// Represents a behaviour which allowes the player to grab any GameObject with a <see cref="Rigidbody"/>.
///  </summary>
public class BrickGrabBehaviour : MonoBehaviour {
    private GameObject collidingObject;
    private GameObject objectInHand;
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update() {
        if (Controller.GetHairTriggerDown()) {
            if (collidingObject) {
                GrabObject();
            }
        }

        if (Controller.GetHairTriggerUp()) {
            if (objectInHand) {
                ReleaseObject();
            }
            else if (GetComponent<FixedJoint>()) {
                DestroyFixedJoint();
            }
        }
    }

    private void SetCollidingObject(Collider collider) {
        if (collidingObject || !collider.GetComponent<Rigidbody>()) {
            return;
        }

        collidingObject = collider.gameObject;
    }

    public void OnTriggerEnter(Collider other) {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other) {
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other) {
        if (!collidingObject) {
            return;
        }

        collidingObject = null;
    }

    private void GrabObject() {
        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    private FixedJoint AddFixedJoint() {
        var fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject() {
        if (GetComponent<FixedJoint>()) {
            DestroyFixedJoint();
            objectInHand.GetComponent<Rigidbody>().AddForce(Controller.velocity, ForceMode.Impulse);
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }

    private void DestroyFixedJoint() {
        GetComponent<FixedJoint>().connectedBody = null;
        Destroy(GetComponent<FixedJoint>());
    }

    private void OnDisable() {
        ReleaseObject();
    }
}