using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseBehaviour : MonoBehaviour {

	private void Start()
	{
		var r = GetComponent<MeshRenderer>();
		r.material.color = new Color(r.material.color.r, r.material.color.g, r.material.color.b, 0.3f);
		Debug.Log("Opacity changed");
	}
}