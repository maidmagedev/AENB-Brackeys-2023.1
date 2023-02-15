using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IODevice
{
    public ItemCollection getInputBuffer();
    public ItemCollection getOutputBuffer();
}


public enum Orientation {
    LR,
    RL,
    UD,
    DU
}

public static class OrientationHelper{

    public static Dictionary<Orientation, Vector2Int> takeFromBind = new(){
        {Orientation.LR, Vector2Int.left},
        {Orientation.RL, Vector2Int.right},
        {Orientation.UD, Vector2Int.up},
        {Orientation.DU, Vector2Int.down},
    };

    public static Dictionary<Orientation, Vector2Int> pushToBind = new(){
        {Orientation.LR, Vector2Int.right},
        {Orientation.RL, Vector2Int.left},
        {Orientation.UD, Vector2Int.down},
        {Orientation.DU, Vector2Int.up},
    };
}