using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable_Inventory_Item : MonoBehaviour
{
    private int positionInCollection;
    [SerializeField] BoxCollider2D boxColl;

    private Sprite heldItem_image;

    // Start is called before the first frame update
    void Start()
    {
        if (boxColl == null) {
            boxColl = GetComponent<BoxCollider2D>();
        }

    }


    public void SetItem(ItemStack item, Sprite  image)
    {
        
    }

    public void reset_slot_position()
    {   

        //get ref to invent, then do invent.inventory_grid[positionInCollection].initialPosition


        //transform.localPosition = initialPosition;
    }



    
}
