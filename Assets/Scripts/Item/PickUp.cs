using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private BoxCollider2D col;
    private ItemStack item;

    [SerializeField] private ItemType item_type;

    [SerializeField] private int num;

    [SerializeField] private PlayerInventory _inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        item = new ItemStack(item_type, num);
        col = GetComponent<BoxCollider2D>();
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _inventory.Add(item);
            Destroy(gameObject);
        }
    }
    
}
