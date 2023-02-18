using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerUI : MonoBehaviour
{
    //[SerializeField] Crosshair_Canvas crosshairCanv;
    private Animator invenAnimator;
    private bool Inventory_Enabled = false;
    // Start is called before the first frame update
    void Start()
    {
        invenAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !Inventory_Enabled)
        {
            Inventory_Enabled = true;
            Toggle_Inventory();
            invenAnimator.SetTrigger("HotbarToInventory");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            Inventory_Enabled = false;
            Toggle_Inventory();
            invenAnimator.SetTrigger("InventoryToHotbar");
        }
    }

    private void Toggle_Inventory()
    {
        //crosshairCanv.SetCrosshairVisibility(!Inventory_Enabled);
    }
}
