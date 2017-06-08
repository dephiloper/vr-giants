using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount];
        for(int i = 0; i<transform.childCount; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
