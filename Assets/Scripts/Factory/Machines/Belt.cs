using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : Machine, IODevice
{
    private ItemCollection IOBuf = new(6);

    private Orientation orientation;

    public Belt(){
        type = MachineType.BELT;
        footPrint = new(1,1);
    }

    private void Update()
    {
        //see grabber, but belt only
    }

    public override ItemCollection getInputBuffer()
    {
        return IOBuf;
    }

    public override ItemCollection getOutputBuffer()
    {
        return IOBuf;
    }
}
