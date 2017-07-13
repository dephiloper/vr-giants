using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TowerAttackBehaviour : MonoBehaviour {

    public GameObject ProjectilePrefab;
    public float AttackRange = 10f;
    public float AttackDamage = 0.1f;
    public float Radius = 10f;
    public int TimeDelta = 1000;
    public float ProjectileHeightOffset = 4.5f;

    private GameObject projectile;
    private GameObject currentTarget;
    private Timer timer;

    private void Start () {
        timer = new Timer(TimeDelta);
    }

    private void FixedUpdate()
    {
        if (timer.IsTimeUp())
        {
            DealDamage();
            timer.Reset();
        }
    }

    public void DealDamage()
    {
        if (!currentTarget) {
            currentTarget = FindClosestEnemy();
        }
        
        if (currentTarget)
        {
            var distance = Vector2.Distance(new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.z), 
                new Vector2(transform.position.x, transform.position.z));
            if (distance < Radius) {
                var spawnPos = transform.position;
                spawnPos.y = ProjectileHeightOffset;
                projectile = Instantiate(ProjectilePrefab, spawnPos, Quaternion.identity);
                var projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
                projectileBehaviour.Target = currentTarget.transform;
                projectileBehaviour.Damage = AttackDamage;
            }
            else
            {
                currentTarget = null;
            }
        }
        
    }

    private GameObject FindClosestEnemy()
    {
        var spawnerBehaviour = SpawnerBehaviour.Instance;
        
        if (!spawnerBehaviour || spawnerBehaviour.getChildren().Length == 0)
            return null;
        
        var closest = -1;
        var lastDistance = float.MaxValue;
        for (var i = 0; i < spawnerBehaviour.getChildren().Length; i++)
        {
            var dist = Vector3.Distance(spawnerBehaviour.getChildren()[i].position, transform.position);
            if (dist < lastDistance)
            {
                closest = i;
                lastDistance = dist;
            }
        }
        return closest != -1 ? spawnerBehaviour.getChildren()[closest].gameObject : null;
    }
}
