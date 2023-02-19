using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public ItemStack item;

    [SerializeField] private ItemType item_type;

    [SerializeField] public int num;

    private PlayerInventory _inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        Init(item_type, num);
    }

    public void Init(ItemType item_type, int num){
        this.item_type = item_type;
        this.num = num;
        item = new ItemStack(item_type, num);

        gameObject.transform.localScale = new Vector3(1 / GetComponent<SpriteRenderer>().sprite.bounds.size.x, 1 / GetComponent<SpriteRenderer>().sprite.bounds.size.y);
        _inventory = FindObjectOfType<PlayerInventory>();
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            //print(item.quantity);
            _inventory.Add(0, item);
            col.gameObject.GetComponent<PlayerActions>().UpdateSelectedItem();
            Destroy(gameObject);
        }
    }
    
}
