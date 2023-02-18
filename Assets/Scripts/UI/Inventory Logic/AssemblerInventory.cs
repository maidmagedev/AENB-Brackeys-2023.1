using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblerInventory : BaseInventory
{
    public AssemblerInventory() {
        inventory_grid = new Dictionary<(int, int), InventoryElement>()
        {
            // this arrangement is based off the furnace inventory and should be changed
            {(0,0), new ItemSlot(new Vector3(-6.26f, -25.78f, 0))}, // Input  Buffer - 1
            {(0,1), new ItemSlot(new Vector3(10.35f, -25.78f, 0))}, // Input  Buffer - 2
            {(0,2), new ItemSlot(new Vector3(26.96f, -25.75f, 0))},    // Input  Buffer - 3
            {(1,0), new OutputSlot(new Vector3(10.4f, 5.72f, 0))},    // Output Buffer - Output Item
        };
        
        inventory.Add(new ItemCollection(3));
        inventory.Add(new ItemCollection(1));
        setInventoryListener(0);
        setInventoryListener(1);
    }
}
