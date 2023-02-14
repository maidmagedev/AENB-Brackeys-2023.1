using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable_Inventory_Item : DraggableUI
{
    

    private BaseInventory InventoryObj;
    private int currentIndex;

    public Draggable_Inventory_Item Init(BaseInventory inv, int index){
        InventoryObj = inv;
        currentIndex = index;

        return this;
    }



    public void SwapItem()
    {
        
    }

    public void reset_slot_position()
    {   
        transform.localPosition = InventoryObj.inventory_grid[currentIndex].initialPosition;
    }

    public void OnDragEnd(PointerEventData evt){
        /*
            if pointer intersects slot
                inventoryObj.grid.Values.Find(intersected)
                do stuuf
            else
                reset_slot_position()
        
        */
    }

    
}
