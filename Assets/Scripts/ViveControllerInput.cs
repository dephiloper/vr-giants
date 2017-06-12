using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViveControllerInput : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	
	private SteamVR_Controller.Device controller
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

	// Update is called once per frame
	void Update () {
		// 1
		if (controller.GetAxis() != Vector2.zero) {
			//Debug.Log(gameObject.name + Controller.GetAxis());
		}

		// 2
		if (controller.GetHairTriggerDown()) {
            //Debug.Log(gameObject.name + " Trigger Press");
        }

        // 3
        if (controller.GetHairTriggerUp()) {
            //Debug.Log(gameObject.name + " Trigger Release");
        }

        // 4
        if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip)) {
            //Debug.Log(gameObject.name + " Grip Press");
        }

        // 5
        if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            //Debug.Log(gameObject.name + " Grip Release");
        }
    }
}
