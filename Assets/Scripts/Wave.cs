using UnityEngine;

public class Wave {
    public GameObject Enemy { get; set; }
    public int Units { get; set; }

    public Wave(GameObject enemy, int units)
    {
        Enemy = enemy;
        Units = units;
    }
}
