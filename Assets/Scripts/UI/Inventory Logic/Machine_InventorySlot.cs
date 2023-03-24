using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_InventorySlot : Visible_InventorySlot
{
    [SerializeField] private bool isVisible;
    
    // enable/disable draggableItem_reference as needed
    
    // This should only be called when a draggableItem_reference has already been instantiated--In other words,
    // This method assumes the slot already contains a stack, and is simply having its quantity increased.
    public override void UpdateSlot(ItemStack stack)
    {
        containedStack = stack;
        if (isVisible)
        {
            // update the draggable reference
            draggableItem_reference.item = stack;
            draggableItem_reference.stackCount.text = containedStack.quantity.ToString();
        }
        
    }
    
    
    public override void SetStack(ItemStack stack)
    {
        if (stack != null)
        {
            containedStack = stack;
            draggableItem_reference = Instantiate(draggablePrefab, this.transform.position, Quaternion.identity, this.transform.parent).GetComponent<DraggableInventoryItem>();
            draggableItem_reference.item = containedStack;
            draggableItem_reference.currSlot = gameObject.GetComponent<Visible_InventorySlot>();
            draggableItem_reference.stackCount.text = containedStack.quantity.ToString();
            draggableItem_reference.setScale(new Vector2(0.25f, 0.25f));
            // only display item if isVisible is set to true
            draggableItem_reference.enabled = isVisible;
        }
        else
        {
            draggableItem_reference = null;
            containedStack = null;
            // It is assumed the draggableItem_reference GameObject has/will be destroyed inside its own script
        }
    }
    
}
