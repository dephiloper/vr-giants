using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents the behaviour which allowes the player to place tower.
/// </summary>
public class SpawnTowerBehaviour : MonoBehaviour {
    /// <summary>
    /// Prefab which gets instantiated if the player places a brick boy tower.
    /// </summary>
    public GameObject BrickBoyTowerPrefab;

    /// <summary>
    /// Prefab which gets instantiated if the player places a mage tower.
    /// </summary>
    public GameObject MageTowerPrefab;

    /// <summary>
    /// Prefab which gets instantiated if the player places a archer tower.
    /// </summary>
    public GameObject ArcherTowerPrefab;

    /// <summary>
    /// Prefab which gets instantiated as a showecase of the new tower position.
    /// </summary>
    public GameObject SelectionTowerPrefab;

    /// <summary>
    /// Prefab which gets instantiated to show the place where he is aiming at.
    /// </summary>
    public GameObject LaserPrefab;

    /// <summary>
    /// <see cref="LayerMask"/> of areas where the player can place tower.
    /// </summary>
    public LayerMask PlaceMask;

    /// <summary>
    /// Threshold at which positions the input on the touchpad gets registered.
    /// </summary>
    public float Threshold = 0.5f;

    /// <summary>
    /// Area where the player can place tower.
    /// </summary>
    public GameObject PlaceArea;

    /// <summary>
    /// Material of the placearea when it is not highlighted.
    /// </summary>
    public Material NormalPlaceArea;

    /// <summary>
    /// Material of the placearea when it is highlighted.
    /// </summary>
    public Material HighlightedPlaceArea;

    private static readonly List<GameObject> MageTowerList = new List<GameObject>();
    private static readonly List<GameObject> ArcherTowerList = new List<GameObject>();
    private static readonly List<GameObject> BrickBoyTowerList = new List<GameObject>();
    private readonly Vector3 towerGroundOffset = Vector3.up * 3.2f;

    private SteamVR_TrackedObject trackedObj;
    private GameObject laser;
    private GameObject selectionTower;
    private Transform laserTransform;
    private RaycastHit? lastHit;
    private bool placeMode = false;
    private bool showSelectionTower = false;
    private GameObject currentTowerPrefab;

    private SteamVR_Controller.Device Controller {
        get { return SteamVR_Controller.Input((int) trackedObj.index); }
    }

    private void Awake() {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void Start() {
        laser = Instantiate(LaserPrefab);
        laserTransform = laser.transform;
    }

    private void Update() {
        laser.SetActive(false);
        if (selectionTower != null) {
            selectionTower.SetActive(showSelectionTower);
        }

        if (Controller.GetHairTriggerDown()) {
            if (selectionTower == null) {
                selectionTower = Instantiate(SelectionTowerPrefab);
            }
            placeMode = true;
        }

        if (placeMode) {
            RaycastHit hit;
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, float.PositiveInfinity)) {
                ShowLaser(hit);
                HighlightPlaceAreas(true);
                lastHit = hit;
            }

            if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad)) {
                // oben
                if (Controller.GetAxis().y > Threshold) {
                    currentTowerPrefab = BrickBoyTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.red);
                }
                // unten
                else if (Controller.GetAxis().y < -Threshold) {
                    currentTowerPrefab = null;
                    showSelectionTower = false;
                }
                // rechts
                else if (Controller.GetAxis().x > Threshold) {
                    currentTowerPrefab = MageTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.magenta);
                }
                // links
                else if (Controller.GetAxis().x < -Threshold) {
                    currentTowerPrefab = ArcherTowerPrefab;
                    showSelectionTower = true;
                    ChangeChildColor(Color.green);
                }
            }
        }

        if (Controller.GetHairTriggerUp()) {
            if (lastHit.HasValue) {
                var hitMask = LayerMaskUtility.BitPositionToMask(lastHit.Value.transform.gameObject.layer);
                if (placeMode && showSelectionTower && PlaceMask.value == hitMask) {
                    InstantiateTower();
                }
            }

            showSelectionTower = false;
            placeMode = false;
            HighlightPlaceAreas(false);
        }
    }

    private void HighlightPlaceAreas(bool isVisible) {
        PlaceArea.GetComponent<Renderer>().material = isVisible ? HighlightedPlaceArea : NormalPlaceArea;
    }

    private void ChangeChildColor(Color color) {
        foreach (var r in selectionTower.GetComponentsInChildren<Renderer>()) {
            r.material.color = color;
        }
    }

    private void InstantiateTower() {
        var tower = Instantiate(currentTowerPrefab);
        tower.transform.position = selectionTower.transform.position;

        if (currentTowerPrefab.name.Equals(MageTowerPrefab.name)) {
            AddNewTowerToList(MageTowerList, tower);
        }
        else if (currentTowerPrefab.name.Equals(BrickBoyTowerPrefab.name)) {
            AddNewTowerToList(BrickBoyTowerList, tower);
        }
        else if (currentTowerPrefab.name.Equals(ArcherTowerPrefab.name)) {
            AddNewTowerToList(ArcherTowerList, tower);
        }
    }

    private void AddNewTowerToList(List<GameObject> list, GameObject newTower) {
        if (list.Count > 1) {
            Debug.Log("Tower added");
            var oldTower = list[0];
            list.RemoveAt(0);
            Destroy(oldTower);
        }

        list.Add(newTower);
    }

    private void ShowLaser(RaycastHit hit) {
        laser.SetActive(true);
        laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hit.point, .5f);
        laserTransform.LookAt(hit.point);
        laserTransform.localScale = new Vector3(laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
        selectionTower.transform.position = hit.point;
        selectionTower.transform.position += towerGroundOffset;

        var hitMask = LayerMaskUtility.BitPositionToMask(hit.transform.gameObject.layer);

        if (PlaceMask.value == hitMask) {
            laser.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else {
            laser.GetComponent<Renderer>().material.color = Color.red;
            showSelectionTower = false;
        }
    }
}