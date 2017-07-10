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

	private void Start () {
		if (!tutorialPlane) {
			//Instantiate(TutorialPlanePrefab, transform.position + transform.forward*10, transform.rotation);
			tutorialPlane = Instantiate(TutorialPlanePrefab, EyeCameraInstance.transform.position + (EyeCameraInstance.transform.forward * 5), 
				EyeCameraInstance.transform.rotation * TutorialPlanePrefab.transform.rotation);
			
			//tutorialPlane.transform.parent = transform.parent;
		}
	}

	private void Update ()
	{
		if (!Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) return;
		
		
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

	private void OnEnable()
	{
		Start();
	}
}
