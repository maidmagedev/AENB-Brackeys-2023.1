using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblerInventory : BaseInventory
{
    public AssemblerInventory() {
        inventory_grid = new Dictionary<int, InventoryElement>()
        {
            // this arrangement is based off the furnace inventory and should be changed
            {0, new ItemSlot(new Vector3(23.9f, -25.75f, 0))}, // input buffer?
            {1, new ItemSlot(new Vector3(-4.2f, 5.8f, 0))},    // input buffer?
            {2, new ItemSlot(new Vector3(23.9f, 5.8f, 0))},    // output buffer
        };
        
        inventory = new ItemCollection(3);
    }
}
