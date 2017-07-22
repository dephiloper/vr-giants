using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a behaviour which shows the game statistics in the win or lose scene.
/// </summary>
public class StatisticsShowecaseBehaviour : MonoBehaviour {
    /// <summary>
    /// GameObject which shows the time.
    /// </summary>
    public GameObject TimeTextbox;

    /// <summary>
    /// GameObject which shows the damage.
    /// </summary>
    public GameObject DamageTextbox;

    /// <summary>
    /// SteamVR CameraRig instance.
    /// </summary>
    public GameObject CameraRig;

    private Text timeTextBoxComponent;
    private Text damageTextBoxComponent;

    private void Start() {
        timeTextBoxComponent = TimeTextbox.GetComponent<Text>();
        damageTextBoxComponent = DamageTextbox.GetComponent<Text>();
    }

    private void Update() {
        var gameScoreBehaviour = CameraRig.GetComponent<GameScoreBehaviour>();
        if (gameScoreBehaviour) {
            timeTextBoxComponent.text = string.Format("{0}s", Math.Round(gameScoreBehaviour.SecondsTaken, 2));
            damageTextBoxComponent.text = string.Format("{0} ({1} DPS)", gameScoreBehaviour.DealedDamage,
                Math.Round(gameScoreBehaviour.DealedDamage / gameScoreBehaviour.SecondsTaken, 4));
        }
    }
}