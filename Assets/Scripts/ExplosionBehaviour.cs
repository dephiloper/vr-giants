using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour {

    public GameObject explosionPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.tag.Equals("GameController")) {
            GameObject objectInHand = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(objectInHand, 2.75f);
            ExplosionDamage(transform.position, 10);
            Destroy(gameObject);
        }
    }

    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag.Equals("Enemy"))
            {
                var distance = (int)Vector3.Distance(hitColliders[i].gameObject.transform.position, transform.position);
                var healthDiff = 10 - distance;
                hitColliders[i].gameObject.GetComponent<EnemyBehaviour>().health -= healthDiff;
            }
            i++;
        }
    }
}