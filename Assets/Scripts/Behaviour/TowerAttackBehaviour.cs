﻿using System;
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
        var closest = -1;
        var lastDistance = float.MaxValue;
        for (var i = 0; i < enemiesTransform.Length; i++)
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
