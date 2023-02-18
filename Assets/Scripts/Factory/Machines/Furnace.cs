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
        child_update = Furnace_Update;
    }

    private void Furnace_Start()
    {
        inpBuf.Add(new ItemStack(ItemType.COAL, 5));
        // automatically searches for the items needed to complete the given recipe and consumes them and goes into the output buffer
        doing = new Recipe(Globals.allRecipes["ironOreToBar"]);
    }

    private void Furnace_Update()
    {
        
    }

    public void Set_Recipe(Recipe recipe)
    {
        doing = recipe;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }
}
