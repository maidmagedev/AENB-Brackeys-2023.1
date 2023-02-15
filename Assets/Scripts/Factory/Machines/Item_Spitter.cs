using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spitter : Machine
{
    public Item_Spitter()
    {
        inpBuf = new(1);
        outBuf = new(0);  // could maybe make this null?
        type = MachineType.SPITTER;
        footPrint = new(3, 3);
    }
}
