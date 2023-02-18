using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceInventory : BaseInventory
{
    public FurnaceInventory() {
        inventory_grid = new Dictionary<(int, int), InventoryElement>()
        {
            {(0,1), new ItemSlot(new Vector3(-6.2f, 5.8f, 0))},    // Input  Buffer - Input Item
            {(0,0), new ItemSlot(new Vector3(26.9f, -25.75f, 0))}, // Input  Buffer - Fuel
            {(1,0), new OutputSlot(new Vector3(26.9f, 5.8f, 0))},    // Output Buffer - Output Item
            {(-1, 0), new FurnaceMeter(new Vector3(0,0,0))}
        };
        

        inventory.Add(new ItemCollection(2));
        inventory.Add(new ItemCollection(1));
    }
}
