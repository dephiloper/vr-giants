using UnityEngine;

/// <summary>
/// Represents a behaviour which handles negative effects on entities.
/// </summary>
public class DebuffBehaviour : MonoBehaviour {
    private Timer slowTimer = null;
    private float speedRestoreMultiplier;

    private void Update() {
        if (slowTimer == null) return;

        if (!slowTimer.IsTimeUp()) return;

        var enemyBehaviour = GetComponent<EnemyBehaviour>();
        if (enemyBehaviour) {
            enemyBehaviour.Speed /= speedRestoreMultiplier;
            slowTimer = null;
        }
    }

    /// <summary>
    /// Reduces the TargetIndex of the <see cref="EnemyBehaviour"/> by 2.
    /// </summary>
    public void MoveBack() {
        var enemyBehaviour = GetComponent<EnemyBehaviour>();
        if (enemyBehaviour) {
            enemyBehaviour.TargetIndex = enemyBehaviour.TargetIndex > 2 ? enemyBehaviour.TargetIndex - 2 : 0;
        }
    }

    /// <summary>
    /// Reduces the movement speed of the enemy.
    /// </summary>
    /// <param name="speedReduction">Speed reduction percentage.</param>
    /// <param name="slowTime">Time in ms how long the enemy is slowed.</param>
    public void ReduceSpeed(float speedReduction, int slowTime) {
        if (slowTimer != null) {
            slowTimer.Reset();
        }
        else {
            var enemyBehaviour = GetComponent<EnemyBehaviour>();
            if (enemyBehaviour) {
                enemyBehaviour.Speed *= 1 - speedReduction;
                speedRestoreMultiplier = 1 - speedReduction;
                slowTimer = new Timer(slowTime);
            }
        }
    }
}