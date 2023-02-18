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
