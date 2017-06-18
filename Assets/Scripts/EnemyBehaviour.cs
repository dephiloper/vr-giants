using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Speed = 10;

    private HealthBehaviour healthBehaviour;
    private Transform target;
    private int targetIndex;
    private bool endReached;

    private void Start()
    {
        targetIndex = 0;
        target = Waypoints.Points[targetIndex];
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    private void SeekTarget()
    {
        if (!healthBehaviour)
        {
            healthBehaviour = GetComponent<HealthBehaviour>();
        }

        if (!endReached)
        {
            var difference = target.position - transform.position;
            transform.Translate(difference.normalized * Speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                endReached = targetIndex > Waypoints.Points.Length - 1;
                if (!endReached)
                {
                    target = Waypoints.Points[targetIndex];
                    targetIndex++;
                }
            }
        }
        else
        {
            DealDamage();

            if (healthBehaviour)
            {
                healthBehaviour.Health = 0;
            }
        }
    }

    private void DealDamage()
    {
        var endPoint = Waypoints.Points[Waypoints.Points.Length - 1];
        
        if (healthBehaviour)
        {
            var endPointHealth = endPoint.GetComponentInChildren<HealthBehaviour>();
            endPointHealth.ReceiveDamage(healthBehaviour.Health * healthBehaviour.DamageTransferCoefficient);
        }
    }

    private void Update()
    {
        SeekTarget();
    }
}