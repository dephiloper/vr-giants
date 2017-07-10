using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffBehaviour : MonoBehaviour
{	
	private Timer slowTimer = null;
	private float speedRestoreMultiplier;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (slowTimer != null)
		{
			if (slowTimer.IsTimeUp())
			{
				var enemyBehaviour = GetComponent<EnemyBehaviour>();
				if (enemyBehaviour)
				{
					enemyBehaviour.Speed /= speedRestoreMultiplier;
					slowTimer = null;
				}				
			}
		}
	}

	public void MoveBack()
	{
		var enemyBehaviour = GetComponent<EnemyBehaviour>();
		if (enemyBehaviour)
		{
			enemyBehaviour.TargetIndex = 0;
		}
	}


	public void ReduceSpeed(float speedReduction, int slowTime)
	{
		if (slowTimer != null)
		{
			slowTimer.Reset();
		}
		else
		{
			var enemyBehaviour = GetComponent<EnemyBehaviour>();
			if (enemyBehaviour)
			{
				enemyBehaviour.Speed *= 1-speedReduction;
				speedRestoreMultiplier = 1 - speedReduction;
				slowTimer = new Timer(slowTime);
			}			
		}
	}
}
