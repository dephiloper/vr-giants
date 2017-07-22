using UnityEngine;

/// <summary>
/// Represents a enemy wave in the game.
/// </summary>
public class Wave {
    /// <summary>
    /// Gets the amout of enemies this <see cref="Wave"/> instance contains.
    /// </summary>
    public int Units { get; private set; }

    private const float EnemyProportion = 0.7f;
    private readonly GameObject[] normalEnemies;
    private readonly GameObject[] midEnemies;
    private readonly GameObject bossEnemy;

    /// <summary>
    /// Initializes a new instance of the <see cref="Wave"/> class. 
    /// </summary>
    /// <param name="units">Amount of enemies this <see cref="Wave"/> instance contains.</param>
    /// <param name="normalEnemies">Instances of normal sized enemies this <see cref="Wave"/> instance contains.</param>
    /// <param name="midEnemies">Instances of medium sized enemies this <see cref="Wave"/> instance contains.</param>
    public Wave(int units, GameObject[] normalEnemies, GameObject[] midEnemies) {
        Units = units;
        this.normalEnemies = normalEnemies;
        this.midEnemies = midEnemies;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Wave"/> class.
    /// </summary>
    /// <param name="units">Amount of enemies this <see cref="Wave"/> instance contains.</param>
    /// <param name="bossEnemy">Instance of the boss enemy this <see cref="Wave"/> instance contains.</param>
    public Wave(int units, GameObject bossEnemy) {
        Units = units;
        this.bossEnemy = bossEnemy;
    }

    /// <summary>
    /// Returns the next enemy which should get spawned.
    /// </summary>
    /// <returns>The next enemy of this wave.</returns>
    public GameObject NextEnemy() {
        if (bossEnemy != null) {
            return bossEnemy;
        }

        if (Random.value <= EnemyProportion) {
            return normalEnemies[Random.Range(0, normalEnemies.Length)];
        }

        return midEnemies[Random.Range(0, midEnemies.Length)];
    }
}