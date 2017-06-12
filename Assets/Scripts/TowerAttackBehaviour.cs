using System;
using UnityEngine;

public class TowerAttackBehaviour : MonoBehaviour {

    public GameObject ProjectilePrefab;
    public float AttackRange = 10f;
    public float AttackDamage = 0.1f;
    public float Radius = 10f;
    public int TimeDelta = 1000;
    public float ProjectileHeightOffset = 9;

    private GameObject projectile;
    private GameObject currentTarget;
    private Timer timer;

    void Start () {
        timer = new Timer(TimeDelta);
    }

    void FixedUpdate()
    {
        if (timer.IsTimeUp())
        {
            DealDamage();
        }
    }

    public void DealDamage()
    {
        if (!currentTarget) {
            currentTarget = FindClosestEnemy();
        }

        if (currentTarget)
        {
            if (Vector3.Distance(currentTarget.transform.position, transform.position) < Radius) {
                var spawnPos = transform.position;
                spawnPos.y = ProjectileHeightOffset;
                projectile = Instantiate(ProjectilePrefab, spawnPos, Quaternion.identity);
                projectile.GetComponent<ProjectileBehaviour>().Target = currentTarget.transform;
            }
            else
            {
                currentTarget = null;
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        var enemiesTransform = SpawnerBehaviour.Instance.getChildren();
        int closest = -1;
        float lastDistance = float.MaxValue;
        for (int i = 0; i < enemiesTransform.Length; i++)
        {
            var dist = Vector3.Distance(enemiesTransform[i].position, transform.position);
            if (dist < lastDistance)
            {
                closest = i;
                lastDistance = dist;
            }
        }

        return closest != -1 ? enemiesTransform[closest].gameObject : null;
    }
}
