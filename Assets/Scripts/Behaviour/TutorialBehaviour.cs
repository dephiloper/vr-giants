using System;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class TutorialBehaviour : MonoBehaviour
{
	public static TutorialBehaviour Instance { get; private set; }
	
	
	public Material[] Materials;

	private SteamVR_Controller trackedObj;
	private new Renderer[] renderer;
	private int currentMaterialIndex = 0;


	void Start (){
		if (!Instance) {
			Instance = this;
		}
		
		// change Spawn to Eye Position
		renderer = GetComponentsInChildren<Renderer>();

		ChangeMaterial();
	}

	private void ChangeMaterial()
	{
		foreach (var r in renderer)
		{
			r.material = Materials[currentMaterialIndex];
		}
	}

	private void OnDestroy(){
		if (Instance) {
			Instance = null;
		}
	}

	void Update () {
		
	}

	public void NextTutorialPage(){
		if (currentMaterialIndex < Materials.Length-1) {
			currentMaterialIndex++;
			ChangeMaterial();
		}
	}

	public void PreviousTutorialPage(){
		if (currentMaterialIndex > 0) {
			currentMaterialIndex--;
			ChangeMaterial();
		}
	}

	public void ExitTutorial(){
		Destroy(gameObject);
	}

	public void Hide()
	{
		foreach (var r in renderer)
		{
			if (r.enabled)
				r.enabled = false;
		}
	}
}
