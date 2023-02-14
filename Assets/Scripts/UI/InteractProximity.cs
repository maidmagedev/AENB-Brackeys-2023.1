using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractProximity : MonoBehaviour
{
    Material mat;
    bool showGlow = false;

    private void Awake()
    {
        if (mat == null)
        {
            mat = GetComponent<Renderer>().material;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            showGlow = true;
            //mat.SetColor("Color", Color.red);
            Debug.Log("ENTER");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            showGlow = false;
            Debug.Log("EXIT");
        }
    }
}
