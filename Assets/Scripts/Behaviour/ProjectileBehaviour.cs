using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    
    public GameObject HitPrefab;
    public Transform Target { private get; set; }
    public float Speed = 10f;
    public float Damage = 1f;

    private bool targetFound;

    private void Update () {
		if (Target != null && !targetFound)
        {
            targetFound = true;
        }

        if (targetFound)
        {
            if (Target != null) { 
                SeekTarget();
            } else
            {
                Destroy(gameObject);
            }
        }
	}

    private void SeekTarget()
    {
        var difference = Target.position - transform.position;
        transform.LookAt(Target);
        transform.Translate(difference.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, Target.position) <= 0.1f)
        {
            Target.GetComponent<HealthBehaviour>().ReceiveDamage(Role.None, Damage);
            var hitAnimation = Instantiate(HitPrefab);
            hitAnimation.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
