using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable_Inventory_Item : DraggableUI, IEndDragHandler
{
    

    private BaseInventory InventoryObj;
    private int currentIndex;

    public Draggable_Inventory_Item Init(BaseInventory inv, int index){
        InventoryObj = inv;
        currentIndex = index;

        return this;
    }

    public void reset_slot_position()
    {   
        transform.localPosition = InventoryObj.inventory_grid[currentIndex].initialPosition;
    }

    public void OnEndDrag(PointerEventData evt){

        List<RaycastResult> raysastResults = new List<RaycastResult>();


        EventSystem.current.RaycastAll(evt, raysastResults);

        GameObject slot = raysastResults.Find(r=>r.gameObject.CompareTag("InvItemSlot")).gameObject;


        if (slot != null){
            InventoryElement [] eles = new InventoryElement[InventoryObj.inventory_grid.Count];   

            InventoryObj.inventory_grid.Values.CopyTo(eles, 0);

            var invSlot = new List<InventoryElement>(eles).Find(IE=>IE.data.slot_object == slot);

            InventoryObj.swapItem(currentIndex, invSlot.data.indexInGrid);
            
        }
        else{
            reset_slot_position();
        }
    }


    
}
