using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        Vector3 temp = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(temp.x, temp.y, 5);
    }
}
