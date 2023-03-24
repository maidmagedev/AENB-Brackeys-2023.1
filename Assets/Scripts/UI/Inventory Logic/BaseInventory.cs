using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// How to use this class:
// Set up the inventory slots using the InventorySlot.cs script as a guide
// From there, simply call the public Add() method whenever you want to add to the inventory

// This inventory performs 4 main features:
// 1. Stores as many stacks as allowed by the number of slots
// 2. Automatically combines like-stacks if there is room
// 3. Updates each slot's sprite whenever the relevant stack is changed
// 4. Allows the user to remove/add itemStacks by index

public class BaseInventory : MonoBehaviour
{
    [SerializeField] protected List<Visible_InventorySlot> slots;
    
    protected int numStacks = 0;

    public virtual List<ItemStack> getItems()
    {
        List<ItemStack> ret = new();
        foreach (Visible_InventorySlot slot in slots)
        {
            if (slot.containedStack != null)
            {
                ret.Add(slot.containedStack);
            }
        }
        return ret;
    }

    public ref List<Visible_InventorySlot> getSlots()
    {
        return ref slots;
    }
    // Attempts to Add the given ItemStack with prioritization on combining like items
    // Returns a bool indicating whether the add operation was successful or not
    public bool Add(ItemStack givenStack)
    {
        // catching potential null references and out-of-bounds exceptions
        if (slots == null || slots.Count == 0)
        {
            print("Slot null reference--aborting add operation");
            return false;
        }
        
        foreach (Visible_InventorySlot slot in slots)
        {
            ItemStack stack = slot.containedStack;
            
            // if like stack found that is not full, combine stacks
            if ( (stack != null) && (stack.typeOf == givenStack.typeOf) && (stack.quantity < stack.max))
            {
                // combine stacks
                stack.quantity += givenStack.quantity;
                
                // if combined quantity is over max, calculate difference and perform various operations
                if (stack.quantity > stack.max)
                {
                    int difference = stack.quantity - stack.max;
                    stack.quantity -= difference;
                    slot.UpdateSlot(stack);
                    
                    // attempt to add remaining difference to a new slot
                    bool succesfulAdd = this.Add(new ItemStack(stack.typeOf, difference));
                    if (!succesfulAdd)
                    { 
                        // spawn a pickup near the PLAYER if there was no room
                        Vector3 playerPos = GameObject.FindWithTag("Player").transform.position;
                        GameObject pickUp = Instantiate(Resources.Load("Items/GenericPickup"), new Vector3(playerPos.x, 
                            playerPos.y + 2, 0), Quaternion.identity).GameObject();
                        pickUp.GetComponent<PickUp>().setItem(stack.typeOf, difference);
                    }
                    return true;         
                }
                // else simply update the slot with the combined quantity
                slot.UpdateSlot(stack);
                return true;
            }
        }
        
        // if there is no room abort add operation
        if (slots.Count <= numStacks)
        {
            print("Inventory full, cannot add");
            return false;
        }

        // Given there is room, iterate through the slots until an empty one is found
        foreach (Visible_InventorySlot slot in slots)
        {
            // if empty slot is found, add
            if (slot.containedStack == null)
            {
                slot.SetStack(givenStack);
                numStacks++;
                return true;
            }
        }
        
        // if this is reached there may be a bug
        print("potential fail in Add() in BaseInventory.cs");
        return false;
    }

    // Attempts to Add at the given index
    // Returns a bool indicating whether the addAt operation was successful or not
    public virtual bool AddAt(ItemStack givenStack, int toIndex, Visible_InventorySlot fromSlot = null)
    {
        Visible_InventorySlot insertLocation = slots[toIndex];
        
        // If there is no stack at the given index, add
        if (insertLocation.containedStack == null)
        {
            slots[toIndex].SetStack(givenStack);
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
                if (fromSlot.getInven().slots[fromSlot.getIndex()].containedStack == null)
                {
                    print("from slot contained stack is null");
                    fromSlot.getInven().AddAt(new ItemStack(givenStack.typeOf, difference), fromSlot.getIndex());
                    return true;
                }
                
                // this does the same as above but assumes the contained stack is not null
                // As of right now, this path should never occur since draggableInventoryItem removes the fromSlot
                fromSlot.getInven().slots[fromSlot.getIndex()].containedStack.quantity = difference;
                fromSlot.getInven().slots[fromSlot.getIndex()].UpdateSlot(fromSlot.getInven().slots[fromSlot.getIndex()].containedStack);
                return true;
            }
            // else if combined quantity is not over max, simply update the slot
            else
            {
                slots[toIndex].UpdateSlot(slots[toIndex].containedStack);
                return true;
            }
            
        }
        // otherwise abort add operation
        print("Add at index " + toIndex + " aborted");
        return false;
    }
    public virtual DraggableInventoryItem Remove(int index)
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
    

    public int getSlotIndex(string name)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
    public void printContents()
    {
        print("Contents:");
        foreach (Visible_InventorySlot slot in this.slots)
        {
            if (slot.containedStack == null)
            {
                continue;
            }
            print(slot.containedStack.typeOf + ": " + slot.containedStack.quantity);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<Visible_InventorySlot>().ToList();
    }
    
}
