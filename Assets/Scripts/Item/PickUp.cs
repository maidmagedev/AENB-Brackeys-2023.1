using UnityEngine;


/// <summary>
///  This script should detect when the player is hovering over the given object and:
/// Add itself to the player's inventory if there is room
/// </summary>
public class PickUp : MonoBehaviour
{
    [SerializeField] private ItemType item_type;
    [SerializeField] private int num;
    private PlayerInventory _inventory;
    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _inventory = FindObjectOfType<PlayerInventory>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.transform.localScale = new Vector3(1 / _spriteRenderer.sprite.bounds.size.x, 1 / _spriteRenderer.sprite.bounds.size.y);
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            bool successfull_add = _inventory.Add(new ItemStack(item_type, num));   // max is set via globals
            //_inventory.printContents();
            col.gameObject.GetComponent<PlayerActions>().UpdateSelectedItem();
            if (successfull_add)
            {
                Destroy(gameObject);
            }
        }
    }

    public void setItem(ItemType itemType, int quantity)
    {
        this.item_type = itemType;
        this._spriteRenderer.sprite = Globals.item_definitions[itemType].sprite;
        this.num = quantity;
    }
    
}