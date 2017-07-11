using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float Speed = 10f;
    public float DamageTransferCoefficient = 0.1f;
    public int TargetIndex;
    public float BrickBoyVulnerability;
    public float MageVulnerability;
    public float ArcherVulnerability;
    
    private HealthBehaviour healthBehaviour;
    private Transform target;
    private bool endReached;
    private float deltaTimeSum;
    private float targetStartY;
    

    private void Start()
    {
        TargetIndex = 0;
        target = WaypointsBehaviour.Points[TargetIndex];
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
                endReached = TargetIndex > WaypointsBehaviour.Points.Length - 1;
                if (!endReached)
                {
                    target = WaypointsBehaviour.Points[TargetIndex];
                    TargetIndex++;
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
                endPointHealth.ReceiveDamage(Role.None, healthBehaviour.Health * DamageTransferCoefficient);
        }
    }

    private void Update()
    {
        SinusWaveMovement();
        SeekTarget();
    }

    private void SinusWaveMovement()
    {
        
        deltaTimeSum += Time.deltaTime * Speed * 0.5f;
        deltaTimeSum = deltaTimeSum > 360 ? 0 : deltaTimeSum;
        
        var movementInterval = (float) Math.Sin(deltaTimeSum) + 1f;
        transform.position = new Vector3(transform.position.x, targetStartY + movementInterval, transform.position.z);
    }
}