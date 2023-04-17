using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceInventory : BaseInventory
{
    [SerializeField] private GameObject progressBar;
    [SerializeField] private Furnace _furnace;

    public void updateProgressBar(float percent)
    {
        progressBar.GetComponent<Image>().fillAmount = percent;
    }
    /* public FurnaceInventory() {
         inventory_grid = new Dictionary<(int, int), InventoryElement>()
         {
             {(0,1), new ItemSlot(new Vector3(-6.2f, 5.8f, 0))},    // Input  Buffer - Input Item
             {(0,0), new ItemSlot(new Vector3(26.9f, -25.75f, 0))}, // Input  Buffer - Fuel
             {(1,0), new OutputSlot(new Vector3(26.9f, 5.8f, 0))},    // Output Buffer - Output Item
             {(-1, 0), new FurnaceMeter(new Vector3(10.4f, 5.8f,0))}
         };


         inventory.Add(new ItemCollection(2));
         inventory.Add(new ItemCollection(1));
     }*/

    public bool AddtoOutput(ItemStack givenStack)
   {
       Visible_InventorySlot insertLocation = slots[2];
        
       // If there is no stack at the given index, add
       if (insertLocation.containedStack == null)
       {
           slots[2].SetStack(givenStack);
           return true;
       }
       // // if like stack found with sufficient capacity combine 
       if ((insertLocation.containedStack.typeOf == givenStack.typeOf) && (insertLocation.containedStack.quantity + givenStack.quantity <= givenStack.max))
       {
           slots[2].containedStack.quantity += givenStack.quantity;
           slots[2].UpdateSlot(slots[2].containedStack);
           return true;
       }
       // otherwise abort add operation
       print("Add at index 3" + " aborted");
       return false;
   }
   
   public override bool AddAt(ItemStack givenStack, int toIndex, Visible_InventorySlot fromSlot = null)
    {
        Visible_InventorySlot insertLocation = slots[toIndex];
        print("adding to assembler inventory");
        // If there is no stack at the given index, add
        if (insertLocation.containedStack == null)
        {
            slots[toIndex].SetStack(givenStack);
            _furnace.set_inpBuf();
            return true;
        }
        // if like stack found 
        if ((insertLocation.containedStack.typeOf == givenStack.typeOf) && (insertLocation.containedStack.quantity < givenStack.max)/*&& (insertLocation.containedStack.quantity + givenStack.quantity <= givenStack.max)*/)
        {
            // combining stack quantities
            // important to use slots[index] as opposed to insertLocation here to make sure the original slot is updated
            slots[toIndex].containedStack.quantity += givenStack.quantity;

            // if the combined quantity is over max, calculate difference and update the slot
            if (slots[toIndex].containedStack.quantity > givenStack.max)
            {
                int difference = slots[toIndex].containedStack.quantity - givenStack.max;
                slots[toIndex].containedStack.quantity -= difference;
                slots[toIndex].UpdateSlot(slots[toIndex].containedStack);
                _furnace.set_inpBuf();

                // if no fromSlot is null, spawn the remaining difference as a pickup near the PLAYER
                if (fromSlot == null)
                {
                    Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
                    GameObject pickUp = Instantiate(Resources.Load("Items/GenericPickup"), new Vector3(playerPos.x, 
                        playerPos.y + 2, 0), Quaternion.identity).GameObject();
                    pickUp.GetComponent<PickUp>().setItem(givenStack.typeOf, difference);
                    return true;
                }

                // if fromSlot is not null, set the quantity of givenStack at the from slot to the remaining difference
                if (fromSlot.getInven().getSlots()[fromSlot.getIndex()].containedStack == null)
                {
                    print("from slot contained stack is null");
                    fromSlot.getInven().AddAt(new ItemStack(givenStack.typeOf, difference), fromSlot.getIndex());
                    return true;
                }
                
                // this does the same as above but assumes the contained stack is not null
                // As of right now, this path should never occur since draggableInventoryItem removes the fromSlot
                fromSlot.getInven().getSlots()[fromSlot.getIndex()].containedStack.quantity = difference;
                fromSlot.getInven().getSlots()[fromSlot.getIndex()].UpdateSlot(fromSlot.getInven().getSlots()[fromSlot.getIndex()].containedStack);
                return true;
            }
            // else if combined quantity is not over max, simply update the slot
            else
            {
                slots[toIndex].UpdateSlot(slots[toIndex].containedStack);
                _furnace.set_inpBuf();
                return true;
            }
            
        }
        // otherwise abort add operation
        print("Furnace Add at index " + toIndex + " aborted");
        return false;
    }
   
   public override DraggableInventoryItem Remove(int index)
   {
       if (slots == null || slots.Count == 0 || slots[index] == null)
       {
           print("inventory or slot null reference--cancelling remove operation");
           return null;
       }
       DraggableInventoryItem removedItem = slots[index].draggableItem_reference;
       slots[index].SetStack(null);
       _furnace.set_inpBuf();
       this.numStacks--;
       print("inventory at index " + index + " is set to null");
       return removedItem;
   }
   
   
   // this was based off the BaseInventory implementation of AddAt but has not been updated to reflect the most recent changes
   public bool base_AddAt(ItemStack givenStack, int index)
   {
       Visible_InventorySlot insertLocation = slots[index];
        
       // If there is no stack at the given index, add
       if (insertLocation.containedStack == null)
       {
           slots[index].SetStack(givenStack);
           return true;
       }
       // if like stack found with sufficient capacity combine 
       if ((insertLocation.containedStack.typeOf == givenStack.typeOf) && (insertLocation.containedStack.quantity + givenStack.quantity <= givenStack.max))
       {
           slots[index].containedStack.quantity += givenStack.quantity;
           slots[index].UpdateSlot(slots[index].containedStack);
           return true;
       }
       // otherwise abort add operation
       print("Add at index " + index + " aborted");
       return false;
   }
   
   public DraggableInventoryItem base_Remove(int index)
   {
       if (slots == null || slots.Count == 0)
       {
           print("inventory or slot null reference--cancelling remove operation");
           return null;
       }

       DraggableInventoryItem removedItem = slots[index].draggableItem_reference;
       slots[index].SetStack(null);
       numStacks--;
       print("inventory at index " + index + " is set to null");
       return removedItem;
   }
   
   public override List<ItemStack> getItems()
   {
       List<ItemStack> ret = new(){slots[0].containedStack, slots[1].containedStack};
       return ret;
   }
   
}
