using UnityEngine;

public class EndPointBehaviour : MonoBehaviour {

    public float Health = 10;

	void Start () {
		
	}
	
	void Update () {
		
	}

    public void ReceiveDamage(float damage)
    {
        Health -= damage;
    }
}
