using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour {

    public GameObject VanishPrefab;
    public float Health = 10;
    public float Resistance = 0.1f;
    public float DamageTransferCoefficient = 0.01f;

    private float maxHealth;

    public void ReceiveDamage(float damage)
    {
        Health -= damage * (1 - Resistance);
        AjustHealthColor();
    }

    private void Start () {
        maxHealth = Health;
        AjustHealthColor();
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Vanish();
        }
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

    private void Vanish()
    {
        var death = Instantiate(VanishPrefab, transform.position, Quaternion.identity);
        Destroy(death, 1.5f);
        Destroy(gameObject);
    }
}
