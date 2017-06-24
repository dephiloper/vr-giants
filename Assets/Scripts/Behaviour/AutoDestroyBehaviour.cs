using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyBehaviour : MonoBehaviour {

    public float time;

	void Start () {
        Destroy(gameObject, time);
    }
	
}
