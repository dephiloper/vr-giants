using UnityEngine;

/// <summary>
/// Represents a behaviour which gives an entity a live pool.
/// </summary>
public class HealthBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefab which gets instantiated if the entity has 0 or less hp.
    /// </summary>
    public GameObject VanishPrefab;

    /// <summary>
    /// Amount of health the entity has.
    /// </summary>
    public float Health = 10;

    /// <summary>
    /// Time in s the <see cref="VanishPrefab"/> stays till it gets destroyed.
    /// </summary>
    public float VanishingTime = 1.5f;

    private const string HealthMaterialName = "polyhealth";
    private float maxHealth;

    /// <summary>
    /// Reduces the health of the entity and respects there vulnerabilities.
    /// </summary>
    /// <param name="role">Role of the entity who did the damage.</param>
    /// <param name="damage">Amount of damage the entity should receive.</param>
    public void ReceiveDamage(Role role, float damage) {
        var calculatedDamage = damage;
        var enemyBehaviour = GetComponent<EnemyBehaviour>();
        switch (role) {
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

        if (role != Role.None) {
            GameScoreBehaviour.Instance.DealedDamage += calculatedDamage;
        }
    }

    private void Start() {
        maxHealth = Health;
        AdjustHealthColor();
    }

    private void Update() {
        if (Health <= 0) {
            Vanish();
        }
    }

    private void AdjustHealthColor() {
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

    private Material FindHealthMaterial(MeshRenderer[] renderers) {
        foreach (var r in renderers) {
            foreach (var m in r.materials) {
                if (m.name.StartsWith(HealthMaterialName)) return m;
            }
        }

        return null;
    }

    private void Vanish() {
        var vanish = Instantiate(VanishPrefab, transform.position, Quaternion.identity);
        Destroy(vanish, VanishingTime);
        Destroy(gameObject);
    }
}