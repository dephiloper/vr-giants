using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour {

    public GameObject VanishPrefab;
    public float Health = 10;
    public float VanishingTime = 1.5f;
    
    private const string HealthMaterialName = "polyhealth";
    private float maxHealth;

    public void ReceiveDamage(Role role, float damage)
    {
        var calculatedDamage = damage;
        var enemyBehaviour = GetComponent<EnemyBehaviour>();
        switch (role)
        {
            case Role.Archer:
                calculatedDamage = enemyBehaviour.ArcherVulnerability * damage;
                break;
            case Role.Mage:
                calculatedDamage = enemyBehaviour.MageVulnerability * damage;
                break;
            case Role.BrickBoy:
                calculatedDamage = enemyBehaviour.BrickBoyVulnerability * damage;
                break;
        }

        Health -= calculatedDamage;
        AdjustHealthColor();
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
        var healthColorRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        var healthDiff = maxHealth - Health;
        var green = new Color(0.3f, 0.69f, 0.49f);
        var redDiff = (Color.red.r - green.r) / maxHealth * healthDiff;
        var greenDiff = (Color.red.g - green.g) / maxHealth * healthDiff;
        var blueDiff = (Color.red.b - green.b) / maxHealth * healthDiff;
        var healthMaterial = FindHealthMaterial(healthColorRenderers);
        
        if (healthMaterial)
            healthMaterial.color = new Color(green.r + redDiff, green.g + greenDiff, green.b + blueDiff);
    }

    private Material FindHealthMaterial(MeshRenderer[] renderers)
    {
        foreach (var r in renderers)
        {
            foreach (var m in r.materials)
            {
                if (m.name.StartsWith(HealthMaterialName)) return m;
            }
        }

        return null;
    }

    private void Vanish()
    {
        var vanish = Instantiate(VanishPrefab, transform.position, Quaternion.identity);
        Destroy(vanish, VanishingTime);
        Destroy(gameObject);
    }
}
