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

    void Start () {
		
	}
	
	void Update () {
		
	}
}
