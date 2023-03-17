using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is currently being used by the Boss Doctor.
public class Grenade : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Throw();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)) {
            Throw();
        }
    }

    void Throw() {
        print("adding force");
        rb.AddForce(Vector2.left * 100f, ForceMode2D.Force);
        rb.AddForce(Vector2.up * 150f, ForceMode2D.Force);
    }
}
