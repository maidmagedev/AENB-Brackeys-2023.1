using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour, IODevice
{
    private ItemCollection IOBuf = new(6);

    private void Update()
    {
        //see grabber, but belt only
    }

    public ItemCollection getInputBuffer()
    {
        return IOBuf;
    }

    public ItemCollection getOutputBuffer()
    {
        return IOBuf;
    }
}
