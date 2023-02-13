using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Item
{

    public static Dictionary<ItemType, int> stackSize = new()
    {
        { ItemType.STONE,       250 },
        { ItemType.ORE_IRON,    250 },
        { ItemType.COAL,        250 },
        { ItemType.ORE_GOLD,    250 },
        { ItemType.IRON,        250 },
        { ItemType.GOLD,        250 }
    };



}

public enum ItemType
{
    STONE,
    ORE_IRON,
    COAL,
    ORE_GOLD,
    IRON,
    GOLD
}
