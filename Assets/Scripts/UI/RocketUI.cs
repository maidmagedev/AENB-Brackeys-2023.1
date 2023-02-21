using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketUI : MonoBehaviour
{
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // press "E" to do this...
            canvas.enabled = true;
            FindObjectOfType<PlayerInventoryUI>().SetInventoryView(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.enabled = false;
            FindObjectOfType<PlayerInventoryUI>().SetInventoryView(false);
        }
    }
}
