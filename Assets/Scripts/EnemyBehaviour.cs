using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public int health = 10;
    public float speed = 10;
    public GameObject killedPrefab;

    private Transform target;
    private int targetIndex;

    // Use this for initialization
    void Start () {
        targetIndex = 0;
        target = Waypoints.points[targetIndex];
    }

    private bool endReached;

    private void SeekTarget()
    {
        if (!endReached) { 
            Vector3 difference = target.position - transform.position;
            transform.Translate(difference.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                endReached = targetIndex > Waypoints.points.Length - 1;
                if (!endReached)
                { 
                    target = Waypoints.points[targetIndex];
                    targetIndex++;
                }
            }
        }
    }

    private void Die()
    {
        GameObject death = Instantiate(killedPrefab, transform.position, Quaternion.identity);
        Destroy(death, 1.5f);
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        SeekTarget();
        if (health <= 0)
        {
            Die();
        }
    }
}
