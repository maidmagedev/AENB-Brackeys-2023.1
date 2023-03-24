using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Canvas : MonoBehaviour
{
    private bool player_inRange = false;
    private Canvas[] canvases;
    [SerializeField] private GameObject InventoryHolder;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            player_inRange = true;

            canvases = GetComponentsInChildren<Canvas>();
            // enable interact canvas
            canvases[0].enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player_inRange = false;

            // disable all canvases
            canvases = GetComponentsInChildren<Canvas>();
            foreach (Canvas canvas in canvases)
            {
                canvas.enabled = false;
            }
            // leave inventory view
            FindObjectOfType<PlayerInventoryUI>().SetInventoryView(false);
            InventoryHolder.SetActive(false);
        }
    }

    private void toggle_mainCanvas()
    {
        canvases = GetComponentsInChildren<Canvas>();
        // interact canvas
        canvases[0].enabled = !canvases[0].enabled;
        // main canvas
        canvases[1].enabled = !canvases[1].enabled;
        // enter inventory view
        FindObjectOfType<PlayerInventoryUI>().SetInventoryView(canvases[1].enabled);
        InventoryHolder.SetActive(canvases[1].enabled);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && player_inRange)
        {
            toggle_mainCanvas();
        }
    }
}