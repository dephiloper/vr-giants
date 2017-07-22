using UnityEngine;

/// <summary>
/// Represents the behaviour of a explosive.
/// </summary>
public class ExplosionBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefab which gets spawned after the explosion collision happened.
    /// </summary>
    public GameObject ExplosionPrefab;

    /// <summary>
    /// Amount of damage the explosion makes.
    /// </summary>
    public float Damage = 20;

    /// <summary>
    /// Range of the explosion.
    /// </summary>
    public float Radius = 20;

    /// <summary>
    /// Speed penality the enemy receives after it got hit by a explosion.
    /// </summary>
    public float SpeedReductionPercentage = 0;

    /// <summary>
    /// Time how long the speed penality is active in ms after it got hit by a explosion.
    /// </summary>
    public int SpeedReductionTime = 5000;

    /// <summary>
    /// Reduces the target index of the enemy by two if it gets hit by a explosion with this property.
    /// </summary>
    public bool IsConfusing = false;

    /// <summary>
    /// Role of the source of this explsion damage. This is needed to manage the vulnerability of the enemies.
    /// </summary>
    public Role DamageProducerRole;

    private void OnCollisionEnter(Collision other) {
        if (TagUtility.IsExplodableEntity(other.gameObject.tag)) {
            var explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(explosion, 2.75f);
            ExplosionDamage(transform.position, Radius);
            Destroy(gameObject);
        }
    }

    private void ExplosionDamage(Vector3 center, float radius) {
        var hitColliders = Physics.OverlapSphere(center, radius);
        var i = 0;
        while (i < hitColliders.Length) {
            if (TagUtility.IsExplodableEntity(hitColliders[i].tag)) {
                var enemyHealth = hitColliders[i].gameObject.GetComponent<HealthBehaviour>();
                var enemyDebuff = hitColliders[i].gameObject.GetComponent<DebuffBehaviour>();
                if (enemyHealth) {
                    //var distance = (int) Vector3.Distance(hitColliders[i].gameObject.transform.position,
                    //    transform.position);
                    //var damageDiff = Damage - distance;
                    //damageDiff = damageDiff <= 0 ? 0 : damageDiff;

                    enemyHealth.ReceiveDamage(DamageProducerRole, Damage);
                }
                if (enemyDebuff) {
                    if (IsConfusing) {
                        enemyDebuff.MoveBack();
                    }

                    enemyDebuff.ReduceSpeed(SpeedReductionPercentage, SpeedReductionTime);
                }
            }
            i++;
        }
    }
}