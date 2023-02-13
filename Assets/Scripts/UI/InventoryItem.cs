using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private Vector3 initialPosition;
    [SerializeField] BoxCollider2D boxColl;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
        if (boxColl == null) {
            boxColl = GetComponent<BoxCollider2D>();
        }

    }

    // Update is called once per frame
    void Update()
    {
            
    }

    public void reset_slot_position()
    {
        transform.localPosition = initialPosition;
    }

    
}
