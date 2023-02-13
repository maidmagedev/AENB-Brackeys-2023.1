using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Material
{

    public static Dictionary<MaterialType, int> stackSize = new()
    {
        { MaterialType.STONE,       250 },
        { MaterialType.ORE_IRON,    250 },
        { MaterialType.COAL,        250 },
        { MaterialType.ORE_GOLD,    250 },
        { MaterialType.IRON,        250 },
        { MaterialType.GOLD,        250 }
    };



}

public enum MaterialType
{
    STONE,
    ORE_IRON,
    COAL,
    ORE_GOLD,
    IRON,
    GOLD
}
