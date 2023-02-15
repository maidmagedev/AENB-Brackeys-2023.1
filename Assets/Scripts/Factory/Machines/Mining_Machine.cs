using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Miner_Inventory))]
public class Mining_Machine : Machine
{
    public GameObject iron_ore;
    public Mining_Machine()
    {
        inpBuf = null;
        outBuf = new(1);
        type = MachineType.MINER;
        footPrint = new(2, 2);
        child_start = Mining_Start;
        child_update = Mining_Update;
        //GetComponent<Miner_Inventory>().inventory = outBuf; 
    }

    private void Mining_Start()
    {
        GetComponent<Miner_Inventory>().inventory = outBuf;
        doing = new Recipe(Globals.allRecipes["ironOreMiner"]);
    }

    private void Mining_Update()
    {

        ItemStack inventory_items = GetComponent<Miner_Inventory>().inventory[0];
        if (inventory_items != null)
        {
            print("num items in miner inventory: " + inventory_items.quantity);
            // when num items reaches 10, send one item to item_spitter
        }
        
    }
    public override ItemCollection getInputBuffer()
    {
        return inpBuf;
    }

    public override ItemCollection getOutputBuffer()
    {
        return outBuf;
    }
}
