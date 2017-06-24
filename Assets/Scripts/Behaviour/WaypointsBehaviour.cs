using UnityEngine;

public class WaypointsBehaviour : MonoBehaviour {

    public static Transform[] Points;

    private void Awake()
    {
        Points = new Transform[transform.childCount];
        for(var i = 0; i<transform.childCount; i++)
        {
            Points[i] = transform.GetChild(i);
        }
    }
}
