using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Represents a behaviour which tracks the current score and loads the win and lose scene on a <see cref="GameState"/>
/// change.
/// </summary>
public class GameScoreBehaviour : MonoBehaviour {
    private GameState currentGameState = GameState.Playing;

    /// <summary>
    /// Gets the singleton instance of the <see cref="GameScoreBehaviour"/>.
    /// </summary>
    public static GameScoreBehaviour Instance { get; private set; }

    /// <summary>
    /// Gets or sets the sum of damage the player made.
    /// </summary>
    public float DealedDamage { get; set; }

    /// <summary>
    /// Gets or sets the time at which the round started.
    /// </summary>
    public DateTime StartTime { private get; set; }

    /// <summary>
    /// Gets or sets the <see cref="GameState"/> the game is currently in. If it switches to <see cref="GameState.Won"/>
    /// or <see cref="GameState.Lost"/> it also saves the current <see cref="DealedDamage"/> and <see cref="StartTime"/>
    /// values and loads the right scene.
    /// </summary>
    public GameState CurrentGameState {
        get { return currentGameState; }
        set {
            if (currentGameState != GameState.Playing) return;

            currentGameState = value;
            SecondsTaken = (DateTime.Now - StartTime).TotalSeconds;
            PlayerPrefs.SetFloat("DealedDamage", DealedDamage);
            PlayerPrefs.SetFloat("SecondsTaken", (float) SecondsTaken);
            File.AppendAllText("./highscores.txt", string.Format("Name: <Name>, Damage: {0}, Seconds: {1}, DPS: {2}{3}",
                DealedDamage, Math.Round(SecondsTaken, 2), Math.Round(DealedDamage / SecondsTaken, 4),
                Environment.NewLine));
            Debug.Log("Seconds " + SecondsTaken);
            PlayerPrefs.Save();

            switch (currentGameState) {
                case GameState.Won:
                    SceneManager.LoadScene("Won");
                    break;
                case GameState.Lost:
                    SceneManager.LoadScene("Lost");
                    break;
            }
        }
    }

    /// <summary>
    /// Gets the amounts of seconds it took the player win or lose.
    /// </summary>
    public double SecondsTaken { private set; get; }

    private void Start() {
        if (Instance == null) {
            Instance = this;
            var dealedDamage = PlayerPrefs.GetFloat("DealedDamage");
            var secondsTaken = PlayerPrefs.GetFloat("SecondsTaken");
            if (secondsTaken > 0) {
                DealedDamage = dealedDamage;
                SecondsTaken = secondsTaken;
            }
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }
}

/// <summary>
/// Represents the state in which the game currently is.
/// </summary>
public enum GameState {
    Won,
    Lost,
    Playing
}