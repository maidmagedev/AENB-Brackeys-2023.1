using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;

// This is meant to be used as a base class for visible inventory slots.
// By itself, there is not enough functionality to perform even basic operations.

// How to use this class:
// Make a prefab with this script on it.
// Place instances of this prefab in your scene as a child of a GameObject that contains a BaseInventory reference
// Organize the prefabs in a grid or whatever you want the inventory to look like
// IMPORTANT: The order of the prefabs in the hierarchy is the order in which the slots will be filled by default, i.e. their index

public abstract class Visible_InventorySlot : MonoBehaviour
{
    [Header("Drag a prefab that extends the DraggableInventoryItem class")]
    protected GameObject draggablePrefab;
    
    [Header("No need to set these up in the editor")]
    public DraggableInventoryItem draggableItem_reference;
    public ItemStack containedStack = null;
    protected int index = 0;

    public BaseInventory inven;
    private IEnumerator Start()
    {
        draggablePrefab = Resources.Load<GameObject>("Inventory/Draggable Inventory Item");
        yield return new WaitForSeconds(0.2f);
        inven = GetComponentInParent<BaseInventory>();
        index = inven.getSlotIndex(gameObject.name);
        if (index == -1)
        {
            print("slot index not found, wtf");
        }
    }

    public int GetIndex()
    {
        return index;
    }

    // This should always be overridden
    public virtual void UpdateSlot(ItemStack stack)
    {
        containedStack = stack;
    }
    
    // This should always be overridden
    public virtual void SetStack(ItemStack stack)
    {
        containedStack = stack;
    }
    
    
}
