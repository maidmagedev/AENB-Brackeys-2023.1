using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_and_Drop : LockedToGrid
{

    private void Update()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
