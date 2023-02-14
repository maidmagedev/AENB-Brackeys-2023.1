using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : BaseInventory
{
    public PlayerInventory()
    {
        inventory_grid = new List<InventoryElement>()
        {
            new ItemSlot(new Vector3(-25, 11, 0)),
            new ItemSlot(new Vector3(-8.25f, 11, 0)),
            new ItemSlot(new Vector3(8.25f, 11, 0)),
            new ItemSlot(new Vector3(25, 11, 0)),
            new ItemSlot(new Vector3(-25, -9, 0)),
            new ItemSlot(new Vector3(-8.25f, -9, 0)),
            new ItemSlot(new Vector3(8.25f, -9, 0)),
            new ItemSlot(new Vector3(25, -9, 0)),
            new ItemSlot(new Vector3(-25, -25.75f, 0)),
            new ItemSlot(new Vector3(-8.25f, -25.75f, 0)),
            new ItemSlot(new Vector3(8.25f, -25.75f, 0)),
            new ItemSlot(new Vector3(25, -25.75f, 0))
            
        };
        inventory = new ItemCollection(12);
    }
    
    
}
