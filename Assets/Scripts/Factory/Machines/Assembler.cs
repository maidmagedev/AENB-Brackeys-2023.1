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
}
