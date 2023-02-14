using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : BaseInventory
{
    public PlayerInventory()
    {
        inventory_grid = new Dictionary<int, InventoryElement>()
        {
            {0, new ItemSlot(new Vector3(-25, 11, 0))},
            {1, new ItemSlot(new Vector3(-8.25f, 11, 0))},
            {2, new ItemSlot(new Vector3(8.25f, 11, 0))},
            {3, new ItemSlot(new Vector3(25, 11, 0))},
            {4, new ItemSlot(new Vector3(-25, -9, 0))},
            {5, new ItemSlot(new Vector3(-8.25f, -9, 0))},
            {6, new ItemSlot(new Vector3(8.25f, -9, 0))},
            {7, new ItemSlot(new Vector3(25, -9, 0))},
            {8, new ItemSlot(new Vector3(-25, -25.75f, 0))},
            {9, new ItemSlot(new Vector3(-8.25f, -25.75f, 0))},
            {10, new ItemSlot(new Vector3(8.25f, -25.75f, 0))},
            {11, new ItemSlot(new Vector3(25, -25.75f, 0))}
            
        };
        
        inventory = new ItemCollection(12);
        
        transform_to_position = new Dictionary<Vector3, int>()
        {
            {new Vector3(-25, 11, 0), 0},
            {new Vector3(-8.25f, 11, 0), 1},
            {new Vector3(8.25f, 11, 0), 2},
            {new Vector3(25, 11, 0), 3},
            {new Vector3(-25, -9, 0), 4},
            {new Vector3(-8.25f, -9, 0), 5},
            {new Vector3(8.25f, -9, 0), 6},
            {new Vector3(25, -9, 0), 7},
            {new Vector3(-25, -25.75f, 0), 8},
            {new Vector3(-8.25f, -25.75f, 0), 9},
            {new Vector3(8.25f, -25.75f, 0), 10},
            {new Vector3(25, -25.75f, 0), 11}
            
        };
        
    }
    
    
}
