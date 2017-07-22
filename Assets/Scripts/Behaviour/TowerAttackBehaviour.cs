using UnityEngine;

/// <summary>
/// Represents the behaviour of a auto attacking tower.
/// </summary>
public class TowerAttackBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefab of the projectile which the tower spawnes to attack.
    /// </summary>
    public GameObject ProjectilePrefab;

    /// <summary>
    /// Amount of damage the projectile makes if it hits an enemy
    /// </summary>
    public float AttackDamage = 0.1f;

    /// <summary>
    /// Max range of the tower.
    /// </summary>
    public float Radius = 10f;

    /// <summary>
    /// Time in ms between the attacks of a tower.
    /// </summary>
    public int TimeDelta = 1000;

    /// <summary>
    /// Height offset at which the projectile gets spawned.
    /// </summary>
    public float ProjectileHeightOffset = 4.5f;

    private GameObject projectile;
    private GameObject currentTarget;
    private Timer timer;

    private void Start() {
        timer = new Timer(TimeDelta);
    }

    private void FixedUpdate() {
        if (timer.IsTimeUp()) {
            DealDamage();
            timer.Reset();
        }
    }

    private void DealDamage() {
        if (!currentTarget) {
            currentTarget = FindClosestEnemy();
        }

        if (currentTarget) {
            var distance = Vector2.Distance(
                new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.z),
                new Vector2(transform.position.x, transform.position.z));
            if (distance < Radius) {
                var spawnPos = transform.position;
                spawnPos.y = ProjectileHeightOffset;
                projectile = Instantiate(ProjectilePrefab, spawnPos, Quaternion.identity);
                var projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
                projectileBehaviour.Target = currentTarget.transform;
                projectileBehaviour.Damage = AttackDamage;
            }
            else {
                currentTarget = null;
            }
        }
    }

    private GameObject FindClosestEnemy() {
        var spawnerBehaviour = SpawnerBehaviour.Instance;

        if (!spawnerBehaviour || spawnerBehaviour.getChildren().Length == 0)
            return null;

        var closest = -1;
        var lastDistance = float.MaxValue;
        for (var i = 0; i < spawnerBehaviour.getChildren().Length; i++) {
            var dist = Vector3.Distance(spawnerBehaviour.getChildren()[i].position, transform.position);
            if (dist < lastDistance) {
                closest = i;
                lastDistance = dist;
            }
        }
        return closest != -1 ? spawnerBehaviour.getChildren()[closest].gameObject : null;
    }
}