using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInventory: MonoBehaviour
{
    public ItemCollection inventory;

    public Dictionary<int, InventoryElement> inventory_grid;
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (InventoryElement item in inventory_grid.Values)
        {
            print(item.prefab_path);
            GameObject prefab = Resources.Load<GameObject>(item.prefab_path);
            GameObject clone = Instantiate(prefab, this.transform, false);
            clone.transform.localPosition = item.initialPosition;
            clone.transform.localScale = new Vector3(0.125f, .125f, 0);
        }
    }

    public void SwapItem(int fromIndex, int toIndex){
        ItemStack temp = inventory[fromIndex];
        inventory[fromIndex] = inventory[toIndex];
        inventory[toIndex] = temp;
    }

}

public class InventoryElement
{
    public Vector3 initialPosition;
    public string prefab_path;

}

