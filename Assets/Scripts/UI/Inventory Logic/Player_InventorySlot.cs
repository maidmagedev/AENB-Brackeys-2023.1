using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InventorySlot : Visible_InventorySlot
{
    // This should only be called when a draggableItem_reference has already been instantiated--In other words,
    // This method assumes the slot already contains a stack, and is simply having its quantity increased.
    public override void UpdateSlot(ItemStack stack)
    {
        containedStack = stack;
        draggableItem_reference.item = stack;
        // update visual to show quantity change
        draggableItem_reference.stackCount.text = containedStack.quantity.ToString();
    }
    
    public override void SetStack(ItemStack stack)
    {
        if (stack != null)
        {
            containedStack = stack;
            draggableItem_reference = Instantiate(draggablePrefab, this.transform.position, Quaternion.identity, this.transform.parent).GetComponent<DraggableInventoryItem>();
            draggableItem_reference.item = containedStack;
            draggableItem_reference.currSlot = gameObject.GetComponent<Visible_InventorySlot>();
            // update visual to show quantity
            draggableItem_reference.stackCount.text = containedStack.quantity.ToString();
        }
        else
        {
            draggableItem_reference = null;
            containedStack = null;
            // It is assumed the draggableItem_reference GameObject has/will be destroyed inside its own script
        }
    }
}
