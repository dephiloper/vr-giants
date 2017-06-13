using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public float DamageTransferCoefficient = 0.01f;
    public float Speed = 10;
    public float Health = 10;
    public float Resistance = 0.1f;
    public GameObject KilledPrefab;

    private Transform target;
    private int targetIndex;
    private float maxHealth;

    void Start () {
        maxHealth = Health;
        targetIndex = 0;
        target = Waypoints.Points[targetIndex];
        AjustHealthColor();
    }

    private bool endReached;

    private void SeekTarget()
    {
        if (!endReached) { 
            Vector3 difference = target.position - transform.position;
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
        } else
        {
            DealDamage();
            Vanish();
        }
    }

    private void Vanish()
    {
        GameObject death = Instantiate(KilledPrefab, transform.position, Quaternion.identity);
        Destroy(death, 1.5f);
        Destroy(gameObject);
    }

    public void ReceiveDamage(float damage)
    {
        Health -= damage * (1-Resistance);
        AjustHealthColor();
    }

    private void DealDamage()
    {
        var endPoint = Waypoints.Points[Waypoints.Points.Length-1];
        endPoint.GetComponent<EndPointBehaviour>().ReceiveDamage(Health * DamageTransferCoefficient);
    }

    private void AjustHealthColor()
    {
        var healthColorRenderer = gameObject.GetComponent<Renderer>();
        if (!healthColorRenderer)
        {
            healthColorRenderer = gameObject.GetComponentInChildren<Renderer>();
        }
            var healthDiff = maxHealth - Health;
            var redDiff = (Color.red.r - Color.green.r) / maxHealth * healthDiff;
            var greenDiff = (Color.red.g - Color.green.g) / maxHealth * healthDiff;
            var blueDiff = (Color.red.b - Color.green.b) / maxHealth * healthDiff;
            healthColorRenderer.material.color = new Color(Color.green.r + redDiff, Color.green.g + greenDiff, Color.green.b + blueDiff);
    }

    void Update()
    {
        SeekTarget();
        if (Health <= 0)
        {
            Vanish();
        }
    }
}
