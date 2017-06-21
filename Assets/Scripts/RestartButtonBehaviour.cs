using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonBehaviour : MonoBehaviour {
	public void OnButtonPressed(){
		SteamVR_LoadLevel.Begin("Game");
	}
}
