using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MachineUI : MonoBehaviour
{
    
    private Canvas canvas;

    private Canvas tipCanvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
