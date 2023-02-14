using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_and_Drop : LockedToGrid
{

    private void OnMouseDrag()
    {
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    protected void OnMouseUp()
    {
        position = transform.position;
    }
}
