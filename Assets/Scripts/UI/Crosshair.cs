using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        
        transform.position = Input.mousePosition;
    }
    private void Awake()
    {
        Cursor.visible = false;
    }

}
