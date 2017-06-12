using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	private GameObject CollidingObject;
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

	// Update is called once per frame
	void Update () {
		if (controller.GetHairTriggerDown ()) {
			if (CollidingObject) {
				GrabObject();
			}
		}

		if (controller.GetHairTriggerUp ()) {
			if (ObjectInHand) {
				ReleaseObject();
			} else if (GetComponent<FixedJoint>())
            {
                DestroyFixedJoint();
            }
		}
	}

	private void SetCollidingObject(Collider coli) {
		// erster teil ist ein not null check
		// zweiter teil checkt ob das object nicht greifbar ist (kein rigidbody ist)
		if (CollidingObject || !coli.GetComponent<Rigidbody> ()) {
			return;
		}

		CollidingObject = coli.gameObject;
	}

	public void OnTriggerEnter(Collider other) {
		SetCollidingObject(other);
	}

	public void OnTriggerStay(Collider other) {
		SetCollidingObject (other);
	}

	public void OnTriggerExit(Collider other) {
		if (!CollidingObject) {
			return;
		}

		CollidingObject = null;
	}

	private void GrabObject() {
		ObjectInHand = CollidingObject;
		CollidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = ObjectInHand.GetComponent<Rigidbody>();
	}

	private FixedJoint AddFixedJoint() {
		FixedJoint fx = gameObject.AddComponent<FixedJoint> ();
		fx.breakForce = 20000; // welcher wert auch sonst
		fx.breakTorque = 20000;
		return fx;
	}

	private void ReleaseObject() {
		if (GetComponent<FixedJoint>())
        {
            DestroyFixedJoint();
            ObjectInHand.GetComponent<Rigidbody>().AddForce(controller.velocity, ForceMode.Impulse);
            ObjectInHand.GetComponent<Rigidbody>().velocity = controller.velocity;
            ObjectInHand.GetComponent<Rigidbody>().angularVelocity = controller.angularVelocity;
        }

        ObjectInHand = null;
	}

    private void DestroyFixedJoint()
    {
        GetComponent<FixedJoint>().connectedBody = null;
        Destroy(GetComponent<FixedJoint>());
    }

    private void OnDisable()
    {
        ReleaseObject();
    }
}
