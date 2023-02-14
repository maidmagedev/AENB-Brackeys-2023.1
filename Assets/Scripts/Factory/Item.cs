using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public static class Item
{

    public static Dictionary<ItemType, (int maxStack, Sprite sprite)> item_definitions = new()
    {
        { ItemType.STONE,       (250, Resources.Load<Sprite>("item_stone")) },
        { ItemType.ORE_IRON,    (250, Resources.Load<Sprite>("item_iron_ore")) },
        { ItemType.COAL,        (250, null)},
        { ItemType.ORE_GOLD,    (250, Resources.Load<Sprite>("item_gold_ore")) },
        { ItemType.IRON,        (250, Resources.Load<Sprite>("item_iron_bar")) },
        { ItemType.GOLD,        (250, Resources.Load<Sprite>("item_gold_bar")) }
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
