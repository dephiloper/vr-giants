using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnerBehaviour : MonoBehaviour
{
    public GameObject Forrest;
    public List<GameObject> SpawnableTrees;
    public List<GameObject> SpawnableStones;
    public GameObject SpawnedObjectParent;
    public int MaxTreeCount;
    public int MaxStoneCount;

    void Start()
    {
        PlaceRandomTrees();
    }

    private void SpawnEntities(float width, float depth, List<GameObject> spawnableEntities, int entityCounter)
    {
        for (var i = 0; i < entityCounter; i++)
        {
            var x = Random.Range(-width / 2, width / 2);
            var z = Random.Range(-depth / 2, depth / 2);
            RaycastHit hit;

            if (Physics.Raycast(new Vector3(x, 100, z), Vector3.down, out hit, float.PositiveInfinity))
            {
                if (TagUtility.IsSpawnableArea(hit.transform.gameObject.tag))
                {
                    var tree = Instantiate(spawnableEntities[Random.Range(0, spawnableEntities.Count)]);
                    tree.transform.parent = SpawnedObjectParent.transform;
                    tree.transform.position = new Vector3(x, hit.point.y, z);
                }
            }
        }
    }

    private void PlaceRandomTrees()
    {
        var width = Forrest.GetComponent<MeshRenderer>().bounds.size.x;
        var depth = Forrest.GetComponent<MeshRenderer>().bounds.size.z;
        SpawnEntities(width, depth, SpawnableTrees, MaxTreeCount);
        SpawnEntities(width, depth, SpawnableStones, MaxStoneCount);  
    }
}