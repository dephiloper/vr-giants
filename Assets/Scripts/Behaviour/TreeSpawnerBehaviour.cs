using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents a behaviour which spawnes trees and stones in the game world.
/// </summary>
public class TreeSpawnerBehaviour : MonoBehaviour {
    /// <summary>
    /// <see cref="GameObject"/> which is used to spawn the objects on.
    /// </summary>
    public GameObject Forrest;

    /// <summary>
    /// A list of trees which can be spawned from the behaviour.
    /// </summary>
    public List<GameObject> SpawnableTrees;

    /// <summary>
    /// A list of stones which can be spawned from the behaviour.
    /// </summary>
    public List<GameObject> SpawnableStones;

    /// <summary>
    /// <see cref="GameObject"/> which gets added as parent for the spawned entities.
    /// </summary>
    public GameObject SpawnedObjectParent;

    /// <summary>
    /// The maximum amount of trees which should get spawned.
    /// </summary>
    public int MaxTreeCount;

    /// <summary>
    /// The maximum amount of stones which should get spawned.
    /// </summary>
    public int MaxStoneCount;

    private void Start() {
        PlaceRandomTrees();
    }

    private void SpawnEntities(float width, float depth, List<GameObject> spawnableEntities, int entityCounter) {
        for (var i = 0; i < entityCounter; i++) {
            var x = Random.Range(-width / 2, width / 2);
            var z = Random.Range(-depth / 2, depth / 2);
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(x, 100, z), Vector3.down, out hit, float.PositiveInfinity)) {
                if (TagUtility.IsSpawnableArea(hit.transform.gameObject.tag)) {
                    var tree = Instantiate(spawnableEntities[Random.Range(0, spawnableEntities.Count)]);
                    tree.transform.parent = SpawnedObjectParent.transform;
                    tree.transform.position = new Vector3(x, hit.point.y, z);
                }
            }
        }
    }

    private void PlaceRandomTrees() {
        var width = Forrest.GetComponent<MeshRenderer>().bounds.size.x;
        var depth = Forrest.GetComponent<MeshRenderer>().bounds.size.z;
        SpawnEntities(width, depth, SpawnableTrees, MaxTreeCount);
        SpawnEntities(width, depth, SpawnableStones, MaxStoneCount);
    }
}