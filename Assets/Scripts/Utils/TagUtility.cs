using System;
using UnityEngine;

public class TagUtility  {
    
    // Tags:
    // enemy
    // bow
    // projectile
    // archertower
    // brickboytower
    // magetower
    // arrow
    // selectiontower
    // terrain
    // gamecontroller
    // button
    
    public static bool IsExplodableEntity(string tag)
    {
        var loweredTag = tag.ToLower();
        return "terrain".Equals(loweredTag) || "enemy".Equals(loweredTag);
    }
    
    public static bool IsController(string tag)
    {
        var loweredTag = tag.ToLower();
        return "gamecontroller".Equals(loweredTag);
    }

    public static bool IsShootableEntity(string tag)
    {
        var loweredTag = tag.ToLower();
        return "terrain".Equals(loweredTag) || "enemy".Equals(loweredTag) || loweredTag.EndsWith("tower");
    }

    public static bool IsArcherTower(string tag)
    {
        return "archertower".Equals(tag.ToLower());
    }
    
    public static bool IsBrickBoyTower(string tag)
    {
        return "brickboytower".Equals(tag.ToLower());
    }
    
    public static bool IsMageTower(string tag)
    {
        return "magetower".Equals(tag.ToLower());
    }

    public static bool IsAttachable(string tag)
    {
        return "bow".Equals(tag.ToLower());
    }

    public static Role TagToTowerRole(string tag)
    {
        var role = Role.None;
        var loweredTag = tag.ToLower();
        if (loweredTag.Equals("archertower"))
        {
            role = Role.Archer;
        } else if (loweredTag.Equals("brickboytower"))
        {
            role = Role.BrickBoy;
        } else if (loweredTag.Equals("magetower"))
        {
            role = Role.Mage;
        }

        return role;
    }

    public static bool IsButton(string tag){
        return "button".Equals(tag.ToLower());
    }

    public static bool CompareRoleWithTowerTag(Role currentRole, string towerTag){
        var lowerTag = towerTag.ToLower();
        switch (currentRole) {
            case Role.BrickBoy:
                return lowerTag == "brickboytower";
            case Role.Archer:
                return towerTag == "archertower";
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
}
