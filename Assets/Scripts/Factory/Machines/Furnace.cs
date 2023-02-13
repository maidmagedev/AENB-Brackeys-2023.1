using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Machine
{
    public Furnace() {
        inpBuf = new(2);
        outBuf = new(1);
        type = MachineType.FURNACE;
    }
}
