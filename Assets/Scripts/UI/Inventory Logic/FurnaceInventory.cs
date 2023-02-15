using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInventory : BaseInventory
{
    public FurnaceInventory() {
        inventory_grid = new Dictionary<int, InventoryElement>()
        {
            {0, new ItemSlot(new Vector3(23.9f, -25.75f, 0))}, // Input  Buffer - Fuel
            {1, new ItemSlot(new Vector3(-4.2f, 5.8f, 0))},    // Input  Buffer - Input Item
            {2, new ItemSlot(new Vector3(23.9f, 5.8f, 0))},    // Output Buffer - Output Item
        };
        
        inventory = new ItemCollection(3);
    }
}
