using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag_and_Drop : MonoBehaviour
{

    private void OnMouseDrag()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3((float)Math.Round(mousePos.x, 0), (float)Math.Round(mousePos.y, 0), 0);
    }

    private void OnMouseUp()
    {
        transform.position = new Vector3((float)Math.Round(transform.position.x, 0), (float)Math.Round(transform.position.y, 0), 0);
    }
}
