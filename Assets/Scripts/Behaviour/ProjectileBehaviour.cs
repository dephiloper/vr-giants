using UnityEngine;

/// <summary>
/// Represents a behaviour which controlls the movement of a projectile
/// </summary>
public class ProjectileBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefabs which get instantiated if the projectile hits its target.
    /// </summary>
    public GameObject HitPrefab;

    /// <summary>
    /// Gets and sets the target of the projectile.
    /// </summary>
    public Transform Target { get; set; }

    /// <summary>
    /// Speed of the projectile.
    /// </summary>
    public float Speed = 10f;

    /// <summary>
    /// Amount of damage the projectile makes if it hits the target.
    /// </summary>
    public float Damage = 1f;

    private bool targetFound;

    private void Update() {
        if (Target != null && !targetFound) {
            targetFound = true;
        }

        if (targetFound) {
            if (Target != null) {
                SeekTarget();
            }
            else {
                Destroy(gameObject);
            }
        }
    }

    private void SeekTarget() {
        var difference = Target.position - transform.position;
        transform.LookAt(Target);
        transform.Translate(difference.normalized * Speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, Target.position) <= 0.1f) {
            Target.GetComponent<HealthBehaviour>().ReceiveDamage(Role.None, Damage);
            var hitAnimation = Instantiate(HitPrefab);
            hitAnimation.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}