using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemMeter : InventoryElement
{

    public AssemMeter(Vector3 givenPos)
    {
        initialPosition = givenPos;
        prefab_path = "AssemMeter";

        initialScale = new(.355f,.137f,0);
    }


}
