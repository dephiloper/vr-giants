using System;

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
    
    public static bool IsExplodableEntity(string tag)
    {
        var loweredTag = tag.ToLower();
        return "terrain".Equals(loweredTag) || "enemy".Equals(loweredTag);
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
}
