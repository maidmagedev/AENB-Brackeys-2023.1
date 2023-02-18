using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemMeter : InventoryElement
{

    public AssemMeter(Vector3 givenPos)
    {
        initialPosition = givenPos;
        prefab_path = "AssemMeter";

        initialScale = new(1,1,0);
    }


}
