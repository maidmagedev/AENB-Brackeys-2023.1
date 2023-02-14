using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IODevice
{
    public ItemCollection getInputBuffer();
    public ItemCollection getOutputBuffer();
}


public enum Orientation{
    LR,
    RL,
    UD,
    DU
}