using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] Points;

    private void Awake()
    {
        Points = new Transform[transform.childCount];
        for(int i = 0; i<transform.childCount; i++)
        {
            Points[i] = transform.GetChild(i);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
