using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblerInventory : BaseInventory
{
    public AssemblerInventory() {
        inventory_grid = new Dictionary<int, InventoryElement>()
        {
            // this arrangement is based off the furnace inventory and should be changed
            {0, new ItemSlot(new Vector3(-6.2f, -25.75f, 0))}, // Input  Buffer - 1
            {1, new ItemSlot(new Vector3(10.5f, -25.75f, 0))}, // Input  Buffer - 2
            {2, new ItemSlot(new Vector3(26.9f, -25.75f, 0))},    // Input  Buffer - 3
            {3, new ItemSlot(new Vector3(10.5f, 5.8f, 0))},    // Output Buffer - Output Item
        };
        
        inventory = new ItemCollection(4);
    }
}
