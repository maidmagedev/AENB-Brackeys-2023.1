using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BaseInventory : MonoBehaviour
{
    public ItemCollection inventory;

    public Dictionary<int, InventoryElement> inventory_grid;

    // Start is called before the first frame update
    void Start()
    {
        // instantiating the empty inventory grid
        if (inventory_grid != null)
        {
            foreach (int itemIndex in inventory_grid.Keys)
            {
                InventoryElement item = inventory_grid[itemIndex];

                GameObject prefab = Resources.Load<GameObject>(item.prefab_path);
                GameObject clone = Instantiate(prefab, this.transform, false);
                clone.transform.localPosition = item.initialPosition;
                clone.transform.localScale = new Vector3(0.125f, .125f, 0);

                item.data.slot_obj(clone).index(itemIndex);
            }
        }
        
    }


    public void swapItem(int from, int to){
        var temp = inventory[from];

        inventory[from] = inventory[to];

        inventory[to] = temp;

        refresh_inventory();
    }
    
    public void Add(ItemStack input)
    {
        var result = inventory.Add(input);
        instantiate_icon(input, result.insertIndex);
    }

    public void Remove(ItemStack o){
        inventory.Remove(o);
    }

    private void refresh_inventory()
    {
        foreach (InventoryElement g in inventory_grid.Values){

            if (g.data.item_object != null)
            {
                Destroy(g.data.item_object.gameObject);
                g.data.item_object = null;
            }
        }

        for (int i = 0; i < inventory.Size; i++)
        {
            if (inventory[i] != null){
                instantiate_icon(inventory[i], i);
            }
        }
    }

    int counter = 0;

    private void instantiate_icon(ItemStack item, int index)
    {
        Draggable_Inventory_Item prefab = Resources.Load<Draggable_Inventory_Item>("InventoryItem");
        Draggable_Inventory_Item clone = Instantiate(prefab, this.transform, false);
        clone.Init(this, index);
        clone.transform.localPosition = inventory_grid[index].initialPosition;
        clone.transform.localScale = new Vector3(0.125f, 0.125f, 0);
        clone.GetComponent<Image>().sprite = Item.item_definitions[item.of].sprite;

        clone.gameObject.name = counter.ToString();
        counter++;
        inventory_grid[index].data.item_obj(clone);
    }

}

public class InventoryElement
{
    public Vector3 initialPosition;
    public string prefab_path;

    public InventorySlotData data = InventorySlotData.Builder();

}

public class InventorySlotData {
    public Draggable_Inventory_Item item_object;
    public GameObject slot_object;

    public int indexInGrid;

    private InventorySlotData(){
    }

    public static InventorySlotData Builder(){
        return new();
    }

    public InventorySlotData item_obj(Draggable_Inventory_Item obj){
        this.item_object = obj;
        return this;
    }

    public InventorySlotData slot_obj(GameObject obj){
        this.slot_object = obj;
        return this;
    }

    public InventorySlotData index(int ind){
        this.indexInGrid = ind;
        return this;
    }
}