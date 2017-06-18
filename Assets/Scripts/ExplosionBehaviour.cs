using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour {

    public GameObject ExplosionPrefab;

    public void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.tag.Equals("GameController")) {
            var objectInHand = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(objectInHand, 2.75f);
            ExplosionDamage(transform.position, 10);
            Destroy(gameObject);
        }
    }

    private void ExplosionDamage(Vector3 center, float radius)
    {
        var hitColliders = Physics.OverlapSphere(center, radius);
        var i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag.Equals("Enemy"))
            {
                var distance = (int)Vector3.Distance(hitColliders[i].gameObject.transform.position, transform.position);
                var healthDiff = 10 - distance;
                var enemyHealth = hitColliders[i].gameObject.GetComponent<HealthBehaviour>();
                enemyHealth.ReceiveDamage(healthDiff);
            }
            i++;
        }
    }
}