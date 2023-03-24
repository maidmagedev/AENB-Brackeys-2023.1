using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// This script primarily enables basic click and drag UI
// It also sets its image component to the given itemstack
public class DraggableInventoryItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    // Dragging logic
    private Vector2 mousePosition = new Vector2();
    private Vector2 startPosition = new Vector2();
    private Vector2 differencePoint = new Vector2();
    
    
    public ItemStack item;
    public Visible_InventorySlot currSlot; // could make these setters and getters instead of being public

    private Image image;
    
    [Header("This needs to be set in editor")]
    public TextMeshProUGUI stackCount;
    
    // Inventory logic
    private GameObject touching = null;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = Globals.item_definitions[item.typeOf].sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            UpdateMousePosition();
        }
        if (Input.GetMouseButtonDown(0))
        {
            UpdateStartPosition();
            UpdateDifferencePoint();
        }
    }

    public void setScale(Vector2 scale)
    {
        transform.localScale = scale;
    }
    // maybe try setting sort order at runtime to fix sorting layer issues
    public void OnDrag(PointerEventData eventData)
    {
        /*Minus the difference point so you can pick the 
        element from the edges, without any jerk*/

        transform.position = mousePosition - differencePoint;
    }
    
    public void OnEndDrag(PointerEventData evt)
    {
        // check if inside an inventory
        // move, swap, merge, etc... as needed
        
        // if touching another inventory slot on release
        if (touching != null)
        {
            print("released on an inventory slot");
            
            // clear the current slot
            currSlot.inven.Remove(currSlot.GetIndex());
            
            // get the new slot
            Visible_InventorySlot newSlot = touching.GetComponent<Visible_InventorySlot>();
            if (newSlot == null)
            {
                print("newSlot " + touching.name + " is null, aborting endDrag operations");
                return;
            }
            
            // attempt to add to new slot
            bool wasAdded = newSlot.inven.AddAt(item, newSlot.GetIndex(), currSlot);
            
            // if the new slot cannot be added to, swap
            if (!wasAdded)
            {
                // clear the new slot
                GameObject temp = newSlot.draggableItem_reference.gameObject; 
                Destroy(newSlot.draggableItem_reference.gameObject);
                newSlot.inven.Remove(newSlot.GetIndex());
                
                // add to new slot
                newSlot.inven.AddAt(item, newSlot.GetIndex(),currSlot);
                // add to old slot
                currSlot.inven.AddAt(temp.GetComponent<DraggableInventoryItem>().item, currSlot.GetIndex(),newSlot);
            }
            // destroy the old item instance, a new one has been created already
            Destroy(gameObject);
        }
        else
        {
            print("released outside any inventories");
            // there are two options here so we need to pick one:
            
            // reset position
            //transform.position = currSlot.transform.position;
            
            // drop stack
            var screenToWorldPosition = Camera.main.ScreenToWorldPoint(transform.position);
            GameObject pickup = Instantiate(Resources.Load<GameObject>("Items/GenericPickup"), 
                new Vector3(screenToWorldPosition.x, screenToWorldPosition.y, 0),
                Quaternion.identity);
            pickup.GetComponent<PickUp>().setItem(item.typeOf, item.quantity);
            currSlot.inven.Remove(currSlot.GetIndex());
            Destroy(gameObject);
        }
        /*int player_inventory_layer = LayerMask.NameToLayer("Player Inventory");
        Collider2D col = Physics2D.OverlapCircle(transform.position, 5, player_inventory_layer);*/
        
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Inventory"))
        {
            touching = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Inventory"))
        {
            touching = null;
        }
    }

    private void UpdateMousePosition()
    {
        mousePosition.x = Input.mousePosition.x;
        mousePosition.y = Input.mousePosition.y;
    }

    private void UpdateStartPosition()
    {
        startPosition.x = transform.position.x;
        startPosition.y = transform.position.y;
    }

    private void UpdateDifferencePoint()
    {
        differencePoint = mousePosition - startPosition;
    }
}
