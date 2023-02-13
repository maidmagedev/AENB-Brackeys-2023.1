using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour, IODevice
{
    private ItemCollection IOBuf = new(1);

    private void Update()
    {
        //Find IODevice "behind", remove 1, wait (time)
        //then, attempt to find IODevice "ahead", add 1, wait (time)
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
