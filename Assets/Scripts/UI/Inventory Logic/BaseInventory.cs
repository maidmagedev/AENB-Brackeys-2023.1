using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BaseInventory: MonoBehaviour
{
    public ItemCollection inventory;

    public Dictionary<int, InventoryElement> inventory_grid;

    public Dictionary<Vector3, int> transform_to_position;

    public List<GameObject> items_in_inventory = new();


    // Start is called before the first frame update
    void Start()
    {
        // instantiating the empty inventory grid
        foreach (InventoryElement item in inventory_grid.Values)
        {
            //print(item.prefab_path);
            GameObject prefab = Resources.Load<GameObject>(item.prefab_path);
            GameObject clone = Instantiate(prefab, this.transform, false);
            clone.transform.localPosition = item.initialPosition;
            clone.transform.localScale = new Vector3(0.125f, .125f, 0);
        }
    }
    
    public void Add(ItemStack input)
    {
        inventory.Add(input);
        refresh_inventory();
    }

    public void Remove(ItemStack input)
    {
        inventory.Remove(input);
        refresh_inventory();
    }

    private void refresh_inventory()
    {
        items_in_inventory.ForEach(g=>Destroy(g));
        for (int i = 0; i < inventory.Count; i++)
        {
            instantiate_icon(inventory[i], i);
        }
    }

    private void instantiate_icon(ItemStack item, int index)
    {
        GameObject prefab = Resources.Load<GameObject>("InventoryItem");
        GameObject clone = Instantiate(prefab, this.transform, false);
        clone.transform.localPosition = inventory_grid[index].initialPosition;
        clone.transform.localScale = new Vector3(0.125f, 0.125f, 0);
        clone.GetComponent<Image>().sprite = Item.item_definitions[item.of].sprite;
        items_in_inventory.Add(clone);
    }

}

public class InventoryElement
{
    public Vector3 initialPosition;
    public string prefab_path;

}

