using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : InventoryElement
{
    public ItemSlot(Vector3 givenPos)
    {
        initialPosition = givenPos;
        prefab_path = "InventoryItem";
    }

    private ItemStack heldItem = null;
    private Sprite heldItem_image = null;
    
    
    
}
