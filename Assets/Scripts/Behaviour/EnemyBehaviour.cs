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
    private float deltaTimeSum;
    private float targetStartY;

    private void Start()
    {
        targetIndex = 0;
        target = WaypointsBehaviour.Points[targetIndex];
        targetStartY = transform.position.y;
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
            
            var distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                new Vector2(target.position.x, target.position.z));    
            
            var targetPostition = new Vector3( target.position.x, 
                this.transform.position.y, 
                target.position.z ) ;
            this.transform.LookAt( targetPostition ) ;
            
            if (distance <= 1f)
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
        SinusWaveMovement();
        SeekTarget();
    }

    private void SinusWaveMovement()
    {
        deltaTimeSum += Time.deltaTime*2;
        deltaTimeSum = deltaTimeSum > 360 ? 0 : deltaTimeSum;
        var movementInterval = 2 * (float) (Math.Sin(deltaTimeSum));
        transform.position = new Vector3(transform.position.x, targetStartY + movementInterval, transform.position.z);
    }
}