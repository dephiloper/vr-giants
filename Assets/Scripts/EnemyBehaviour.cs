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

    // Use this for initialization
    void Start () {
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
    }

    private void DealDamage()
    {
        var endPoint = Waypoints.Points[Waypoints.Points.Length-1];
        endPoint.GetComponent<EndPointBehaviour>().ReceiveDamage(Health * DamageTransferCoefficient);
    }

    private void AjustHealthColor()
    {
        var healthColorRenderer = gameObject.GetComponent<Renderer>();

        if (Health > 6)
        {
            healthColorRenderer.material.color = Color.green;
        } else if (Health > 3)
        {
            healthColorRenderer.material.color = Color.yellow;
        } else if (Health > 0)
        {
            healthColorRenderer.material.color = Color.red;
        }
    }

    void Update()
    {
        SeekTarget();
        AjustHealthColor();
        if (Health <= 0)
        {
            Vanish();
        }
    }
}
