using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    public Transform Target { private get; set; }
    public float Speed = 10f;
    public float Damage = 1f;
    public GameObject hitPrefab;

    private bool targetFound = false;

    void Start () {
    }

    void Update () {
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
        Vector3 difference = Target.position - transform.position;
        transform.Translate(difference.normalized * Speed * Time.deltaTime, Space.World);
        transform.LookAt(Target);
        Debug.Log(transform.rotation);

        if (Vector3.Distance(transform.position, Target.position) <= 0.1f)
        {
            Target.GetComponent<EnemyBehaviour>().ReceiveDamage(Damage);
            GameObject hit = Instantiate(hitPrefab, transform);
            Destroy(hit, 1.5f);
            Destroy(gameObject);
        }
    }
}
