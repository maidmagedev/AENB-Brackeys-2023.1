using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    
    public ItemType typeOf;

    public int quantity;

    public int max;
    
    public ItemStack(ItemType typeOf, int quantity) {
        this.typeOf = typeOf;
        this.quantity = quantity;
        this.max = Globals.item_definitions[typeOf].max;
    }
    public static ItemStack copy(ItemStack orig) {
        return new ItemStack(orig.typeOf, orig.quantity);
    }
    
}

// Each ItemStack contains a list Items
// An item is always of quantity one and contains sprite and gameobject information
public class Item
{
    public Sprite sprite;
    public ItemType typeOf;
    //public GameObject item;
    
    public Item(ItemType givenType)
    {
        this.typeOf = givenType;
        //this.sprite = Globals.item_sprite_definitions[givenType];
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
    SHOTGUN,
    MINER,
    FURNACE,
    GRABBER,
    BELT,
    SPITTER,
    ASSEMBLER,
    PISTOL,
    QUEST,
    BOLTACTION
}
