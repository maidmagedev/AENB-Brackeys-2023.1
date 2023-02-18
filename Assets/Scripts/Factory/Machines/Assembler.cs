using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : Machine
{
    public Assembler() {
        inpBuf = new(5);
        outBuf = new(5);
        type = MachineType.ASSEMBLER;
        footPrint = new(3, 3);
        child_start = AssemblerStart;
    }

    private void AssemblerStart()
    {
        doing = new Recipe(Globals.allRecipes["Miner_Machine"]);

        // not sure about this...
        GetComponentInChildren<AssemblerInventory>()[0] = inpBuf;
        GetComponentInChildren<AssemblerInventory>()[1] = inpBuf;
        GetComponentInChildren<AssemblerInventory>()[2] = inpBuf;
        GetComponentInChildren<AssemblerInventory>()[3] = outBuf;
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
