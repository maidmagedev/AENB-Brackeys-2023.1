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

        inventory.AddListener(handleInvUpdate);
    }


    private void handleInvUpdate(ItemColChangeEvent evt){
        print(gameObject.name + " " + evt.changeType + " " + evt.affectedindices[0]);
        switch(evt.changeType){
            case ChangeType.SET:
                evt.affectedindices.ForEach(i=>updateIcon(i));
            break;
            case ChangeType.REMOVE:
                evt.affectedindices.ForEach(i=>updateIcon(i));
            break;
            case ChangeType.ADD:
                evt.affectedindices.ForEach(i=>updateIcon(i));
            break;
            case ChangeType.SWAP:
                throw new System.Exception("SWAP detected, should not exist!");
            default:
                throw new System.NotImplementedException("Update baseINvetory with new ChangeTypes!");
        }
    }

    public void swapItem(int from, int to){
        var temp = inventory[from];

        inventory[from] = inventory[to];

        inventory[to] = temp;
    }
    
    public void Add(ItemStack input, bool needsNew = false)
    {
        var result = inventory.Add(input, needsNew);
    }
    

    public void Remove(ItemStack o){
        inventory.Remove(o);
    }

    int counter = 0;

    private void instantiate_icon(int index)
    {
        Draggable_Inventory_Item prefab = Resources.Load<Draggable_Inventory_Item>("InventoryItem");
        Draggable_Inventory_Item clone = Instantiate(prefab, this.transform, false);
        clone.Init(this, index);
        clone.transform.localPosition = inventory_grid[index].initialPosition;
        clone.transform.localScale = new Vector3(0.125f, 0.125f, 0);
        clone.GetComponent<Image>().sprite = Item.item_definitions[inventory[index].of].sprite;

        clone.gameObject.name = counter.ToString();
        counter++;
        inventory_grid[index].data.item_obj(clone);
    }

    private void updateIcon(int index){
        if (inventory_grid != null){
            if (inventory_grid[index].data.item_object != null){
                print("destroying " + inventory_grid[index].data.item_object.gameObject.name);
                Destroy(inventory_grid[index].data.item_object.gameObject);
            }

        
            if (inventory[index] != null){
                instantiate_icon(index);
            }
        
        }
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