using UnityEngine;

public class TowerAttackBehaviour : MonoBehaviour {

    public GameObject ProjectilePrefab;
    public float AttackRange = 10f;
    public float AttackDamage = 0.1f;
    public float Radius = 10f;
    public int TimeDelta = 1;


    private GameObject projectile;
    private int lastTime;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        int time = (int)Time.fixedTime;
        if ((time % TimeDelta == 0) && time != lastTime)
        {
            DealDamage();
            lastTime = time;

        }
    }
    void DealDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag.Equals("Enemy"))
            {
                Debug.Log("launch projectile");
                var spawnPos = transform.position;
                spawnPos.y = 9;
                projectile = Instantiate(ProjectilePrefab, spawnPos, Quaternion.Euler(new Vector3(0, 90, 0)));
                projectile.GetComponent<ProjectileBehaviour>().Target = hitColliders[i].transform;
            }
            i++;
        }
    }
}
