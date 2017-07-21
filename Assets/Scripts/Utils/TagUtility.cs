using UnityEngine;

/// <summary>
/// Represents a utility class which makes working with tags in Unity easier.
/// </summary>
public class TagUtility
{
    /// <summary>
    /// Checks if <see cref="tag"/> is something which should cause an explosion if touched by an explosive.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if the explosive should explode.</returns>
    public static bool IsExplodableEntity(string tag){
        var loweredTag = tag.ToLower();
        return "forest".Equals(loweredTag) || "mountain".Equals(loweredTag) || "path".Equals(loweredTag) ||
               "place".Equals(loweredTag) || "enemy".Equals(loweredTag);
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to an area which allowes the spawning of trees and stones.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if the area belongs to an area which allowes the spawning of trees and stones.</returns>
    public static bool IsSpawnableArea(string tag){
        var loweredTag = tag.ToLower();
        return "forest".Equals(loweredTag);
    }

    /// <summary>
    /// Checks if <see cref="tag"/> is the tag of a gamecontroller.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it is a gamecontroller.</returns>
    public static bool IsController(string tag){
        var loweredTag = tag.ToLower();
        return "gamecontroller".Equals(loweredTag);
    }

    /// <summary> 
    /// Checks if <see cref="tag"/> is something which should be hitable by a projectile from the player.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it should be hitable.</returns>
    public static bool IsShootableEntity(string tag){
        var loweredTag = tag.ToLower();
        return "forest".Equals(loweredTag) || "mountain".Equals(loweredTag) || "path".Equals(loweredTag) ||
               "place".Equals(loweredTag) || "enemy".Equals(loweredTag) || loweredTag.EndsWith("tower");
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a enemy.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a enemy.</returns>
    public static bool IsEnemy(string tag){
        var loweredTag = tag.ToLower();
        return "enemy".Equals(loweredTag);
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to an archer tower.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to an archer tower.</returns>
    public static bool IsArcherTower(string tag){
        return "archertower".Equals(tag.ToLower());
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a brick boy tower.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a brick boy tower.</returns>
    public static bool IsBrickBoyTower(string tag){
        return "brickboytower".Equals(tag.ToLower());
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a mage tower.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a mage tower.</returns>
    public static bool IsMageTower(string tag){
        return "magetower".Equals(tag.ToLower());
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to something which can handle the attaching of a arrow.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to something which can handle the attaching of a arrow.</returns>
    public static bool IsAttachable(string tag){
        return "bow".Equals(tag.ToLower());
    }

    /// <summary>
    /// Maps the given <see cref="tag"/> to the right <see cref="Role"/>.
    /// </summary>
    /// <param name="tag">Tag which gets mapped.</param>
    /// <returns>The correct <see cref="Role"/> for the given <see cref="tag"/>.</returns>
    public static Role TagToTowerRole(string tag){
        var role = Role.None;
        var loweredTag = tag.ToLower();
        if (loweredTag.Equals("archertower")) {
            role = Role.Archer;
        }
        else if (loweredTag.Equals("brickboytower")) {
            role = Role.BrickBoy;
        }
        else if (loweredTag.Equals("magetower")) {
            role = Role.Mage;
        }

        return role;
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a tutorial button.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a tutorial button.</returns>
    public static bool IsTutorialButton(string tag){
        return "tutorialbutton".Equals(tag.ToLower());
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a restart button.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a restart button.</returns>
    public static bool IsRestartButton(string tag){
        return "restartbutton".Equals(tag.ToLower());
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a quit button.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a quit button.</returns>
    public static bool IsQuitButton(string tag){
        return "quitbutton".Equals(tag.ToLower());
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a tutorial plane.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a tutorial plane.</returns>
    public static bool IsTutorialPlane(string tag){
        return "tutorialplane".Equals(tag.ToLower());
    }

    /// <summary>
    /// Compares if the given <see cref="currentRole"/> and <see cref="towerTag"/> belong to each other. 
    /// </summary>
    /// <param name="currentRole"><see cref="Role"/> which gets compared.</param>
    /// <param name="towerTag">Tag which gets compared.</param>
    /// <returns>Returns true if <see cref="currentRole"/> and <see cref="towerTag"/> belong to each other.</returns>
    public static bool CompareRoleWithTowerTag(Role currentRole, string towerTag){
        var lowerTag = towerTag.ToLower();
        switch (currentRole) {
            case Role.BrickBoy:
                return lowerTag == "brickboytower";
            case Role.Archer:
                return lowerTag == "archertower";
            case Role.Mage:
                return lowerTag == "magetower";
            case Role.None:
                Debug.LogError("Tag Role is none.");
                return lowerTag == "none";
            default:
                Debug.LogError("Tag Role is default.");
                return lowerTag == "none";
        }
    }

    /// <summary>
    /// Checks if <see cref="tag"/> belongs to a button.
    /// </summary>
    /// <param name="tag">Tag which gets checked.</param>
    /// <returns>Returns true if it belongs to a button.</returns>
    public static bool IsButton(string tag){
        return tag.ToLower().Contains("button");
    }
}