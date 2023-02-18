using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : Machine
{
    public Assembler() {
        inpBuf = new(3);
        outBuf = new(1);
        type = MachineType.ASSEMBLER;
        footPrint = new(3, 3);
        child_start = AssemblerStart;
    }

    private void AssemblerStart()
    {
        doing = new Recipe(Globals.allRecipes["Miner_Machine"]);

        GetComponentInChildren<AssemblerInventory>()[0] = inpBuf;
        GetComponentInChildren<AssemblerInventory>()[1] = outBuf;
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
