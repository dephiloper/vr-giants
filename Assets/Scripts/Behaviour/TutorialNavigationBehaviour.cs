using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TutorialNavigationBehaviour : MonoBehaviour {
	
	public GameObject TutorialPlanePrefab;
	public GameObject EyeCameraInstance;
	
	private SteamVR_TrackedObject trackedObj;
	private static GameObject tutorialPlane;
	
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

	void Start () {
		if (!tutorialPlane) {
			tutorialPlane = Instantiate(TutorialPlanePrefab, EyeCameraInstance.transform.position + (transform.forward * 2), 
				EyeCameraInstance.transform.rotation * TutorialPlanePrefab.transform.rotation);
			
			tutorialPlane.transform.parent = transform.parent;
		}
	}
	
	void Update () {
		if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
		{
			if (Controller.GetAxis().x > 0.5f)
			{
				TutorialBehaviour.Instance.NextTutorialPage();
			}else if (Controller.GetAxis().x < -0.5f) {
				TutorialBehaviour.Instance.PreviousTutorialPage();
			}else if (Controller.GetAxis().y < -0.5f) {
				TutorialBehaviour.Instance.ExitTutorial();
				transform.parent.GetComponent<MovementChangeBehaviour>().MovementState = State.Giant;
			}
		}
	}
}
