using System;
using UnityEngine;

/// <summary>
/// Represents the behaviour of an enemy in the game.
/// </summary>
public class EnemyBehaviour : MonoBehaviour {
    /// <summary>
    /// Movement speed of the enemy.
    /// </summary>
    public float Speed = 10f;

    /// <summary>
    /// Coefficient how much of the live of the enemy gets transformed to damage if it hits the base.
    /// </summary>
    public float DamageTransferCoefficient = 0.1f;

    /// <summary>
    /// Current index of the target of the enemy.
    /// </summary>
    public int TargetIndex;

    /// <summary>
    /// Vulnerability against brick boy tower player attacks.
    /// </summary>
    public float BrickBoyVulnerability;

    /// <summary>
    /// Vulnerability against mage tower player attacks.
    /// </summary>
    public float MageVulnerability;

    /// <summary>
    /// Vulnerability against archer tower player attacks.
    /// </summary>
    public float ArcherVulnerability;

    private HealthBehaviour healthBehaviour;
    private Transform target;
    private bool endReached;
    private float deltaTimeSum;
    private float targetStartY;

    private void Start() {
        TargetIndex = 0;
        target = WaypointsBehaviour.Points[TargetIndex];
        targetStartY = transform.position.y;
    }

    private void SeekTarget() {
        if (!healthBehaviour) {
            healthBehaviour = GetComponent<HealthBehaviour>();
        }

        if (!endReached) {
            var difference = target.position - transform.position;
            transform.Translate(difference.normalized * Speed * Time.deltaTime, Space.World);

            var distance = Vector2.Distance(
                new Vector2(transform.position.x, transform.position.z),
                new Vector2(target.position.x, target.position.z));

            var targetPostition = new Vector3(target.position.x, transform.position.y, target.position.z);
            transform.LookAt(targetPostition);

            if (distance <= 1f) {
                endReached = TargetIndex > WaypointsBehaviour.Points.Length - 1;
                if (!endReached) {
                    target = WaypointsBehaviour.Points[TargetIndex];
                    TargetIndex++;
                }
            }
        }
        else {
            DealDamage();

            if (healthBehaviour) {
                healthBehaviour.Health = 0;
            }
        }
    }

    private void DealDamage() {
        var endPoint = WaypointsBehaviour.Points[WaypointsBehaviour.Points.Length - 1];

        if (healthBehaviour) {
            var endPointHealth = endPoint.GetComponentInChildren<HealthBehaviour>();
            if (endPointHealth)
                endPointHealth.ReceiveDamage(Role.None, healthBehaviour.Health * DamageTransferCoefficient);
        }
    }

    private void Update() {
        SinusWaveMovement();
        SeekTarget();
    }

    private void SinusWaveMovement() {
        deltaTimeSum += Time.deltaTime * Speed * 0.5f;
        deltaTimeSum = deltaTimeSum > 360 ? 0 : deltaTimeSum;

        var movementInterval = (float) Math.Sin(deltaTimeSum) + 1f;
        transform.position = new Vector3(transform.position.x, targetStartY + movementInterval, transform.position.z);
    }
}