using UnityEngine;

/// <summary>
/// Represents the behaviour of the tutorial.
/// </summary>
public class TutorialBehaviour : MonoBehaviour {
    /// <summary>
    /// Gets the singleton instance of the <see cref="TutorialBehaviour"/>.
    /// </summary>
    public static TutorialBehaviour Instance { get; private set; }

    /// <summary>
    /// Materials of the tutorial pages.
    /// </summary>
    public Material[] Materials;

    private new Renderer[] renderer;
    private int currentMaterialIndex = 0;

    private void Start() {
        if (!Instance) {
            Instance = this;
        }

        renderer = GetComponentsInChildren<Renderer>();
        ChangeMaterial();
    }

    private void ChangeMaterial() {
        foreach (var r in renderer) {
            r.material = Materials[currentMaterialIndex];
        }
    }

    private void OnDestroy() {
        if (Instance) {
            Instance = null;
        }
    }

    public void NextTutorialPage() {
        if (currentMaterialIndex >= Materials.Length - 1) return;

        currentMaterialIndex++;
        ChangeMaterial();
    }

    public void PreviousTutorialPage() {
        if (currentMaterialIndex <= 0) return;

        currentMaterialIndex--;
        ChangeMaterial();
    }

    public void ExitTutorial() {
        Destroy(gameObject);
        SpawnerBehaviour.Instance.StartSpawning();
    }

    public void Hide() {
        foreach (var r in renderer) {
            if (r.enabled)
                r.enabled = false;
        }
    }
}