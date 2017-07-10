using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour {

    public GameObject ExplosionPrefab;
    public float Damage = 20;
    public float Radius = 20;
    public float SpeedReductionPercentage = 0;
    public int SpeedReductionTime = 5000;
    public bool IsConfusing = false;
    
    private void OnCollisionEnter(Collision other)
    {
        if (TagUtility.IsExplodableEntity(other.gameObject.tag)) {
            var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2.75f);
            ExplosionDamage(transform.position, Radius);
            Destroy(gameObject);
        }
    }

    private void ExplosionDamage(Vector3 center, float radius)
    {
        var hitColliders = Physics.OverlapSphere(center, radius);
        var i = 0;
        while (i < hitColliders.Length)
        {
            if (TagUtility.IsExplodableEntity(hitColliders[i].tag))
            {
                var enemyHealth = hitColliders[i].gameObject.GetComponent<HealthBehaviour>();
                var enemyDebuff = hitColliders[i].gameObject.GetComponent<DebuffBehaviour>();
                if (enemyHealth)
                {
                    var distance = (int) Vector3.Distance(hitColliders[i].gameObject.transform.position,
                        transform.position);
                    var healthDiff = Damage - distance;
                    healthDiff = healthDiff <= 0 ? 0 : healthDiff;
                    
                    enemyHealth.ReceiveDamage(healthDiff);
                }
                if (enemyDebuff)
                {
                    if (IsConfusing)
                    {
                        enemyDebuff.MoveBack();
                    }

                    enemyDebuff.ReduceSpeed(SpeedReductionPercentage, SpeedReductionTime);
                }
            }
            i++;
        }
    }
}