using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Speed = 10f;
    public float DamageTransferCoefficient = 0.1f;
    
    private HealthBehaviour healthBehaviour;
    private Transform target;
    private int targetIndex;
    private bool endReached;

    private void Start()
    {
        targetIndex = 0;
        target = WaypointsBehaviour.Points[targetIndex];
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
                endReached = targetIndex > WaypointsBehaviour.Points.Length - 1;
                if (!endReached)
                {
                    target = WaypointsBehaviour.Points[targetIndex];
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
        var endPoint = WaypointsBehaviour.Points[WaypointsBehaviour.Points.Length - 1];
        
        if (healthBehaviour)
        {
            var endPointHealth = endPoint.GetComponentInChildren<HealthBehaviour>();
            if (endPointHealth)
                endPointHealth.ReceiveDamage(healthBehaviour.Health * DamageTransferCoefficient);
        }
    }

    private void Update()
    {
        SeekTarget();
    }
}