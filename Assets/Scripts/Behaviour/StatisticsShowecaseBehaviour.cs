using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatisticsShowecaseBehaviour : MonoBehaviour
{

	public GameObject TimeTextbox;
	public GameObject DamageTextbox;
	public GameObject CameraRig;

	private Text timeTextBoxComponent;
	private Text damageTextBoxComponent;

	private void Start ()
	{
		timeTextBoxComponent = TimeTextbox.GetComponent<Text>();
		damageTextBoxComponent = DamageTextbox.GetComponent<Text>();
	}

	private void Update ()
	{
		var gameScoreBehaviour = CameraRig.GetComponent<GameScoreBehaviour>();
		if (gameScoreBehaviour)
		{
			timeTextBoxComponent.text = string.Format("{0}s", Math.Round(gameScoreBehaviour.SecondsTaken, 2));
			damageTextBoxComponent.text = string.Format("{0} ({1} DPS)",gameScoreBehaviour.DealedDamage, 
				Math.Round(gameScoreBehaviour.DealedDamage / gameScoreBehaviour.SecondsTaken, 4));
		}
	}
}
