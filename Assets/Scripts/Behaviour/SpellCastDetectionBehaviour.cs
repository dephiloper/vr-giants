using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a behaviour which allowes a player to cast spells through drawing.
/// </summary>
public class SpellCastDetectionBehaviour : MonoBehaviour {
    /// <summary>
    /// SteamVR camera instance which is used to place the <see cref="LockedCam"/> on the start of the gesture 
    /// detection.
    /// </summary>
    public Camera Camera;

    /// <summary>
    /// Camera instance which is used to record the gesture input.
    /// </summary>
    public GameObject LockedCam;

    /// <summary>
    /// Prefab which gets instantiated if the player draws a fire spell.
    /// </summary>
    public GameObject FireSpellPrefab;

    /// <summary>
    /// Prefab which gets instantiated if the player draws a frost spell.
    /// </summary>
    public GameObject FrostSpellPrefab;

    /// <summary>
    /// Prefab which gets instantiated if the player draws a confusion spell.
    /// </summary>
    public GameObject ConfusionSpellPrefab;

    /// <summary>
    /// Prefab which gets instantiated if the player fails to draw a spell.
    /// </summary>
    public GameObject CastFailedPrefab;

    /// <summary>
    /// Prefab which gets instantiated while the player draws the spell.
    /// </summary>
    public GameObject SpellTraceElementPrefab;

    private SteamVR_TrackedObject trackedObj;
    private List<Vector2> points;
    private readonly List<GameObject> spellTrace = new List<GameObject>();
    private int ralphIstKackeCounter = 0;

    private GameObject fireSpell;

    private GameObject frostSpell;
    private GameObject lightningSpell;
    private GameObject castFailed;

    private GameObject currentSpell;
    private bool isTracking;
    private bool spellCasted;
    private const int SpellForceMultipilier = 2000;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void FixedUpdate() {
        TrackMovement();

        if (currentSpell == null) {
            if (Controller.GetHairTrigger() && !isTracking) {
                isTracking = true;
                points = new List<Vector2>(1024);
                LockedCam.transform.position = Camera.transform.position;
                LockedCam.transform.rotation = Camera.transform.rotation;
            }
            else if (!Controller.GetHairTrigger() && isTracking) {
                isTracking = false;
                RemoveSpellTrace();
                var detectionResult = GestureDetectionUtility.Detect(points);
                SpawnSpells(detectionResult);
            }
        }
        else {
            if (Controller.GetHairTrigger() && !isTracking && !spellCasted) {
                currentSpell.transform.parent = null;
                var spellRigidbody = currentSpell.AddComponent<Rigidbody>();
                spellRigidbody.useGravity = false;
                spellRigidbody.AddForce(transform.forward * SpellForceMultipilier);
                spellCasted = true;
            }
            else if (!Controller.GetHairTrigger() && spellCasted && !isTracking) {
                currentSpell = null;
                spellCasted = false;
            }
        }
    }

    private void RemoveSpellTrace() {
        foreach (var spellTraceElement in spellTrace) {
            Destroy(spellTraceElement);
        }
    }

    private void OnDisable() {
        DestroySpells();
        RemoveSpellTrace();
    }

    private void DestroySpells() {
        Destroy(currentSpell);
    }

    private void SpawnSpells(GestureDetectionUtility.Result detectionResult) {
        switch (detectionResult) {
            case GestureDetectionUtility.Result.Circle:
                CastSpell(FireSpellPrefab);
                break;
            case GestureDetectionUtility.Result.Triangle:
                CastSpell(FrostSpellPrefab);
                break;
            case GestureDetectionUtility.Result.Square:
                CastSpell(ConfusionSpellPrefab);
                break;
            case GestureDetectionUtility.Result.NotDetectable:
                if (CastSpell(CastFailedPrefab)) {
                    Destroy(currentSpell, 0.5f);
                }
                break;
            default:
                if (CastSpell(CastFailedPrefab)) {
                    Destroy(currentSpell, 0.5f);
                }
                Debug.LogError("Unhandled detection Result defaulting to fail.");
                break;
        }
    }

    private bool CastSpell(GameObject spellPrefab) {
        if (currentSpell) {
            return false;
        }

        currentSpell = Instantiate(spellPrefab, transform);
        return true;
    }

    private void TrackMovement() {
        if (!isTracking) return;

        SpawnSpellTrace();

        var cameraPoint = LockedCam.GetComponent<Camera>().WorldToScreenPoint(transform.position);
        var roundedX = (float) Math.Round(cameraPoint.x, 1);
        var roundedY = (float) Math.Round(cameraPoint.y, 1);
        points.Add(new Vector2(roundedX, roundedY));
    }

    private void SpawnSpellTrace() {
        if (ralphIstKackeCounter >= 10) {
            var spellTraceElement = Instantiate(SpellTraceElementPrefab, transform);
            spellTrace.Add(spellTraceElement);
            spellTrace.Last().transform.parent = null;
            ralphIstKackeCounter = 0;
        }
        ralphIstKackeCounter++;
    }
}