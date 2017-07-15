using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScoreBehaviour : MonoBehaviour
{

	private GameState currentGameState = GameState.Playing;
	
	public static GameScoreBehaviour Instance { get; private set; }
	
	public float DealedDamage { get; set; }
	public DateTime StartTime { private get; set; }
	
	public GameState CurrentGameState
	{
		get { return currentGameState; }
		set
		{
			if (currentGameState != GameState.Playing) return;
			
			currentGameState = value;
			SecondsTaken = (DateTime.Now - StartTime).TotalSeconds;
			PlayerPrefs.SetFloat("DealedDamage", DealedDamage);
			PlayerPrefs.SetFloat("SecondsTaken", (float)SecondsTaken);
			Debug.Log("Seconds " + SecondsTaken);
			PlayerPrefs.Save();

			switch (currentGameState)
			{
				case GameState.Won:
					SceneManager.LoadScene("Won");
					break;
				case GameState.Lost:
					SceneManager.LoadScene("Lost");
					break;
			}
		}
	}

	public double SecondsTaken
	{
		private set;
		get;
	}
	
	private void Start () {
		if (Instance == null)
		{
			Instance = this;
			var dealedDamage = PlayerPrefs.GetFloat("DealedDamage");
			var secondsTaken = PlayerPrefs.GetFloat("SecondsTaken");
			if (secondsTaken > 0)
			{
				DealedDamage = dealedDamage;
				SecondsTaken = secondsTaken;
			}
			PlayerPrefs.DeleteAll();
		}
	}

	private void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}
}

public enum GameState
{
	Won, Lost, Playing
}
