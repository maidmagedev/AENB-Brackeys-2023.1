using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace_Outputter : MonoBehaviour
{
    [SerializeField] private GameObject furnaceUI;
    
    // NOTE: If you update this dictionary, be sure to also update the corresponding one located in Scripts/UI/Furnace/Furnace_Preview_UI
    private List<(string input, string output, string fuel)> furnaceRecipes = new()
    {
        ("Items/item_iron_ore", "Items/item_iron_bar", "Items/coal"),
        ("Items/item_gold_ore", "Items/item_gold_bar", "Items/coal")
    };
    private BaseInventory furnace_inventory;
    // Start is called before the first frame update
    void Start()
    {
        furnace_inventory = GetComponent<BaseInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Craft((string input, string output, string fuel) recipe)
    {
        if (furnace_inventory.inventory.Contains(new ItemStack(ItemType.COAL, 1)))
        {
            
        }
    }
}
