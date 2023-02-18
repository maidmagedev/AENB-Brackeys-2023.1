using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private ItemStack item;

    [SerializeField] private ItemType item_type;

    [SerializeField] private int num;

    private PlayerInventory _inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        item = new ItemStack(item_type, num);
        _inventory = FindObjectOfType<PlayerInventory>();
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _inventory.Add(0, item);
            col.gameObject.GetComponent<PlayerActions>().UpdateSelectedItem();
            Destroy(gameObject);
        }
    }
    
}
