using UnityEngine;

/// <summary>
/// Represents the behaviour of a grenade chest. It spawns a <see cref="GrenadePrefab"/> everytime the 
/// <see cref="TimeDelta"/> has passed by. The grenade chest grabs at the beginning the positions of its children and 
/// uses them as spawn slots. If all spawn slots are used it waits till a slot is free again.
/// </summary>
public class GrenadeChestBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefab which gets spawned.
    /// </summary>
    public GameObject GrenadePrefab;

    /// <summary>
    /// Time between the spawnes.
    /// </summary>
    public int TimeDelta = 3000;

    private Transform[] slots;
    private GameObject[] grenades;
    private Timer timer;
    private int lastTime;

    private void Awake() {
        grenades = new GameObject[transform.childCount];
        slots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            slots[i] = transform.GetChild(i);
        }
    }

    private void Start() {
        timer = new Timer(TimeDelta);
    }

    private void FixedUpdate() {
        if (timer.IsTimeUp()) {
            for (var i = 0; i < grenades.Length; i++) {
                if (grenades[i] == null) {
                    grenades[i] = Instantiate(GrenadePrefab, slots[i].transform.position,
                        GrenadePrefab.transform.rotation);
                    break;
                }
            }

            timer.Reset();
        }
    }
}