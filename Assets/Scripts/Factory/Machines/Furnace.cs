using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Furnace : Machine
{

    [SerializeField] private GameObject FurnaceUI;
    [SerializeField] private BaseInventory furnace_inventory;


    
    public Furnace() {
        inpBuf = new(2);
        outBuf = new(1);
        type = MachineType.FURNACE;
        footPrint = new(2, 2);
        child_start = Furnace_Start;
    }

    private void Furnace_Start()
    {
        // automatically searches for the items needed to complete the given recipe and consumes them and goes into the output buffer
        doing = new Recipe(Globals.allRecipes["ironOreToBar"]);
        
        GetComponentInChildren<FurnaceInventory>()[0] = inpBuf;
        GetComponentInChildren<FurnaceInventory>()[1] = outBuf;


        //inpBuf.Add(new ItemStack(ItemType.ORE_IRON, 100));
    }
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponentInChildren<Canvas>().enabled = true;
            FindObjectOfType<Crosshair_Canvas>().SetCrosshairVisibility(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInChildren<Canvas>().enabled = false;
            FindObjectOfType<Crosshair_Canvas>().SetCrosshairVisibility(true);
        }
    }
}
