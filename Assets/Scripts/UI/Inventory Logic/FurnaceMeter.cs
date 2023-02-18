using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceMeter : InventoryElement
{
    public FurnaceMeter(Vector3 givenPos)
    {
        initialPosition = givenPos;
        prefab_path = "FurnaceMeter";

        initialScale = new(1,1,0);
    }
}
