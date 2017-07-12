using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialVisibleBehaviour : MonoBehaviour
{
	private void Update () {
		if (TutorialBehaviour.Instance)
			TutorialBehaviour.Instance.Hide();
		
		RaycastHit hit;

		if (Physics.Raycast(transform.position, transform.forward, out hit, float.PositiveInfinity))
		{
			if (TagUtility.IsTutorialPlane(hit.transform.tag))
			{
				hit.transform.GetComponent<MeshRenderer>().enabled = true;
			}
		}	
	}
}
