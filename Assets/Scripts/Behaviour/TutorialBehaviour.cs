using UnityEngine;
using UnityEngine.Assertions.Must;

public class TutorialBehaviour : MonoBehaviour
{
	public static TutorialBehaviour Instance { get; private set; }
	
	
	public Material[] Materials;

	private SteamVR_Controller trackedObj;
	private Renderer renderer;
	private int currentMaterialIndex = 0;


	void Start (){
		if (!Instance) {
			Instance = this;
		}
		
		// change Spawn to Eye Position
		renderer = GetComponent<Renderer>();
		renderer.material = Materials[currentMaterialIndex];
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
			renderer.material = Materials[currentMaterialIndex];
		}
	}

	public void PreviousTutorialPage(){
		if (currentMaterialIndex > 0) {
			renderer.material = Materials[--currentMaterialIndex];
		}
	}

	public void ExitTutorial(){
		Destroy(gameObject);
	}
}
