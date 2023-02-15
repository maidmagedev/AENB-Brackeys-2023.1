using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractProximity : MonoBehaviour
{
    [Header("Glow")]
    Material mat;
    Color dim;
    Color glow;
    bool showGlow = false;
    float time = 0;
    float duration = 2;

    private void Awake()
    {
        if (mat == null)
        {
            mat = GetComponentInParent<Renderer>().material;
        }
        float f1 = Mathf.Pow(2, -10);
        float f2 = Mathf.Pow(2, 10);
        dim = new Color(mat.color.r * f1, mat.color.g * f1, mat.color.b * f1, mat.color.a);
        glow = new Color(mat.color.r * -f1, mat.color.g * f2, mat.color.b * f2, mat.color.a);

    }

    private void Update() {
        if (time < duration) {
            float t = time / duration;
            if (showGlow) {
                mat.color = Color.Lerp(mat.color, glow, t);
            } else {
                mat.color = Color.Lerp(mat.color, dim, t * 3);
            }
            time += Time.deltaTime;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            showGlow = true;
            time = 0;
            //mat.SetColor("_Color", Color.red);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {   
            showGlow = false;
            //mat.SetColor("_Color", glow);
            time = 0;
        }
    }

    
}
