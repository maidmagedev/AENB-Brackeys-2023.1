using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseInventory : MonoBehaviour
{
    public List<ItemCollection> inventory = new();
    public Dictionary<(int invInd, int indInInv), InventoryElement> inventory_grid;

    // Start is called before the first frame update
    void Start()
    {
        // instantiating the empty inventory grid
        if (inventory_grid != null)
        {
            foreach (var itemIndex in inventory_grid.Keys)
            {
                InventoryElement item = inventory_grid[itemIndex];
                GameObject prefab = Resources.Load<GameObject>(item.prefab_path);
                GameObject clone = Instantiate(prefab, this.transform, false);
//                print(clone.name);
                clone.transform.localPosition = item.initialPosition;
                clone.transform.localScale = item.initialScale;

                item.data.slot_obj(clone).index(itemIndex);
            }
    
        
        }
        
    }

    public ItemCollection this[int i]{
        get { return inventory[i];}
        set {
            //print("here");
            inventory[i] = value;
            inventory[i].AddListener(evt=>handleInvUpdate(i, evt));
        }
    }


    public void setInventoryListener(int invInd){
        inventory[invInd].AddListener(evt=>handleInvUpdate(invInd, evt));
    }

    private void handleInvUpdate(int invIndex, ItemColChangeEvent evt){
        //print(gameObject.name + " " + evt.changeType + " " + invIndex + " " + evt.affectedindices[0]);
        switch(evt.changeType){
            case ChangeType.REMOVE:
                evt.affectedindices.ForEach(i=>updateIcon((invIndex,i)));
            break;
            case ChangeType.ADD:
                evt.affectedindices.ForEach(i=>updateIcon((invIndex,i)));
            break;
            case ChangeType.SWAP:
                evt.affectedindices.ForEach(i=>updateIcon((invIndex,i)));
            break;
            default:
                throw new System.NotImplementedException("Update baseInventory with new ChangeTypes!");
        }
    }

    public void swapItem((int, int) from, (int, int) to){
        var temp = inventory[from.Item1][from.Item2];

        inventory[from.Item1][from.Item2] = inventory[to.Item1][to.Item2];

        inventory[to.Item1][to.Item2] = temp;
    }
    
    public void Add(int invInd, ItemStack input, bool needsNew = false)
    {
        var result = inventory[invInd].Add(input, needsNew);
    }
    

    public void Remove(int invInd, ItemStack o){
        inventory[invInd].Remove(o);
    }

    int counter = 0;

    // I made this return the GameObject so I could use it in updateIcon
    private GameObject instantiate_icon((int,int) indexData)
    {
        Draggable_Inventory_Item prefab = Resources.Load<Draggable_Inventory_Item>("InventoryItem");
        Draggable_Inventory_Item clone = Instantiate(prefab, this.transform, false);
        clone.Init(this, indexData);
        clone.transform.localPosition = inventory_grid[indexData].initialPosition;
        clone.transform.localScale = new Vector3(0.125f, 0.125f, 0);
        clone.GetComponent<Image>().sprite = Globals.item_definitions[inventory[indexData.Item1][indexData.Item2].of].sprite;

        clone.gameObject.name = counter.ToString();
        counter++;
        inventory_grid[indexData].data.item_obj(clone);
        return clone.gameObject;
    }

    private void updateIcon((int, int) indexData)
    {
        GameObject destroyedObj = null;
        // if there is an item already here, destroy icon on the grid
        if (inventory_grid != null){
            if (inventory_grid[indexData].data.item_object != null){
                //print("destroying " + inventory_grid[indexData].data.item_object.gameObject.name);
                destroyedObj = inventory_grid[indexData].data.item_object.gameObject;
                Destroy(inventory_grid[indexData].data.item_object.gameObject);
            }

        
            // if the icon has been destroyed, instantiate a new icon
            if (inventory[indexData.Item1][indexData.Item2] != null){
                GameObject icon_object = instantiate_icon(indexData);
                // incrementing stack count UI
                TextMeshProUGUI stack_count = icon_object.GetComponentInChildren<TextMeshProUGUI>();
                int count = inventory[indexData.Item1][indexData.Item2].quantity;
                stack_count.text = count.ToString();
            }
        
        }
    }



    public void updateProgressBar(float percent){
        inventory_grid[(-1, 0)].data.slot_object.GetComponent<Image>().fillAmount = percent;
    }
}

public class InventoryElement
{
    public Vector3 initialPosition;
    public string prefab_path;

    public Vector3 initialScale =new Vector3(0.125f, .125f, 0);

    public InventorySlotData data = InventorySlotData.Builder();

}

public class InventorySlotData {
    public Draggable_Inventory_Item item_object;
    public GameObject slot_object;

    public (int, int) indexInGrid;

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

    public InventorySlotData index((int, int) ind){
        this.indexInGrid = ind;
        return this;
    }
}