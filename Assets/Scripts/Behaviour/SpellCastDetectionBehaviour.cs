using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Valve.VR;

public class SpellCastDetectionBehaviour : MonoBehaviour
{
	public Camera Camera;
	public GameObject LockedCam;
	public GameObject FireSpellPrefab;
	public GameObject FrostSpellPrefab;
	public GameObject LightningSpellPrefab;
	public GameObject CastFailedPrefab;
	
	private SteamVR_TrackedObject trackedObj;
	private List<Vector2> points;
	private GameObject fireSpell;
	private GameObject frostSpell;
	private GameObject lightningSpell;
	private GameObject castFailed;
	private bool isTracking;
	
	private SteamVR_Controller.Device Controller {
		get { return SteamVR_Controller.Input ((int)trackedObj.index); }
	}
	
	private void Awake(){
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	private void FixedUpdate(){
		TrackMovement();
		
		if (Controller.GetPressDown(EVRButtonId.k_EButton_Grip)) {
			isTracking = true;
			points = new List<Vector2>(1024);
			LockedCam.transform.position = Camera.transform.position;
			LockedCam.transform.rotation = Camera.transform.rotation;
		}
		
		if (Controller.GetPressUp(EVRButtonId.k_EButton_Grip)) {
			isTracking = false;
			DestroySpells();
			var detectionResult = GestureDetectionUtility.Detect(points);
			Debug.Log(detectionResult);
			SpawnSpells(detectionResult);
		}
	}

	private void DestroySpells(){
		Destroy(fireSpell);
		Destroy(frostSpell);
		Destroy(lightningSpell);
		Destroy(castFailed);
	}

	private void SpawnSpells(GestureDetectionUtility.Result detectionResult){
		switch (detectionResult) {
			case GestureDetectionUtility.Result.Circle:
				if (!fireSpell) {
					fireSpell = Instantiate(FireSpellPrefab, transform);
				}
				break;
			case GestureDetectionUtility.Result.Triangle:
				if (!frostSpell) {
					frostSpell = Instantiate(FrostSpellPrefab, transform);
				}
				break;
			case GestureDetectionUtility.Result.Square:
				if (!lightningSpell) {
					lightningSpell = Instantiate(LightningSpellPrefab, transform);
				}
				break;
			case GestureDetectionUtility.Result.NotDetectable:
				if (!castFailed) {
					castFailed = Instantiate(CastFailedPrefab, transform);
					Destroy(castFailed, 0.5f);
				}
				break;
			default:
				if (!castFailed) {
					castFailed = Instantiate(CastFailedPrefab);
					Destroy(castFailed, 0.5f);
				}
				Debug.LogError("Unhandled detection Result defaulting to fail.");
				break;
		}
	}

	private void TrackMovement(){
		if (!isTracking) return;
		
		var cameraPoint = LockedCam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
		var roundedX = (float) Math.Round(cameraPoint.x, 1);
		var roundedY = (float) Math.Round(cameraPoint.y, 1);
		points.Add(new Vector2(roundedX, roundedY));
	}
}
