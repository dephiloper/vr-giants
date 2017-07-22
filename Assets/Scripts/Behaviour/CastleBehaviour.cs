using UnityEngine;

/// <summary>
/// Represents a behaviour which changes the <see cref="GameState"/> to <see cref="GameState.Lost"/>.
/// </summary>
public class CastleBehaviour : MonoBehaviour {
    /// <summary>
    /// SteamVR CameraRig instance.
    /// </summary>
    public GameObject CameraRig;

    private void OnDestroy() {
        if (!CameraRig)
            return;

        var gameScoreBehaviour = CameraRig.GetComponent<GameScoreBehaviour>();
        if (gameScoreBehaviour) {
            gameScoreBehaviour.CurrentGameState = GameState.Lost;
        }
    }
}