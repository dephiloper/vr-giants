using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour {

    public GameObject VanishPrefab;
    public float Health = 10;
    public float Resistance = 0.1f;
    public float VanishingTime = 1.5f;

    private float maxHealth;

    public void ReceiveDamage(float damage)
    {
        Health -= damage * (1 - Resistance);
        AdjustHealthColor();
    }

    public void ReceiveDamage()
    {
        Health = 0;
    }

    private void Start () {
        maxHealth = Health;
        AdjustHealthColor();
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Vanish();
        }
    }

    private void AdjustHealthColor()
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

    private void Vanish()
    {
        var vanish = Instantiate(VanishPrefab, transform.position, Quaternion.identity);
        Destroy(vanish, VanishingTime);
        Destroy(gameObject);
    }
}
