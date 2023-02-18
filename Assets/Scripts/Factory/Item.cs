using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.U2D;
using UnityEngine;

public static class Item
{

    public static Dictionary<ItemType, (int maxStack, Sprite sprite, GameObject g)> item_definitions = new()
    {
        { ItemType.STONE,       (250, Resources.Load<Sprite>("Items/item_stone"), Resources.Load<GameObject>("Items/Stone")) },
        { ItemType.ORE_IRON,    (250, Resources.Load<Sprite>("Items/item_iron_ore"), Resources.Load<GameObject>("Items/Iron Ore")) },
        { ItemType.COAL,        (250, Resources.Load<Sprite>("Items/item_coal"), Resources.Load<GameObject>("Items/Coal"))},
        { ItemType.ORE_GOLD,    (250, Resources.Load<Sprite>("Items/item_gold_ore"), Resources.Load<GameObject>("Items/Gold Ore")) },
        { ItemType.IRON,        (250, Resources.Load<Sprite>("Items/item_iron_bar"), Resources.Load<GameObject>("Items/Iron Bar")) },
        { ItemType.GOLD,        (250, Resources.Load<Sprite>("Items/item_gold_bar"), Resources.Load<GameObject>("Items/Gold Bar")) },
        { ItemType.FAMAS,       (1, Resources.Load<Sprite>("Items/item_famas"), Resources.Load<GameObject>("Items/Famas")) },
        { ItemType.SHOTGUN,     (1, Resources.Load<Sprite>("Items/item_shotgun"), Resources.Load<GameObject>("Items/Shotgun")) }

    };
    
    
    public static List<Sprite> toSprites(List<ItemStack> items){
        List<Sprite> sprites =new();

        items.ForEach(item=>sprites.Add(Item.item_definitions[item.of].sprite));

        return sprites;
    }


}

public enum ItemType
{
    STONE,
    ORE_IRON,
    COAL,
    ORE_GOLD,
    IRON,
    GOLD,
    FAMAS,
    SHOTGUN
}
