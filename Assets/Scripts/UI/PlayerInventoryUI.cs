using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject InventoryUI;
    [SerializeField] Animator invenAnimator;
    [SerializeField] Settings settings;
    private bool inventoryActive = false;
    //private bool midTransition = false;
    private bool craftingActive = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(settings.inventoryKey)) {
            if (craftingActive) {
                StartCoroutine(ToggleFurnaceView());
            }
            StartCoroutine(ToggleInventoryView());
        } else if (Input.GetKeyDown(settings.interactKey)) {
            if (!inventoryActive) {
                StartCoroutine(ToggleInventoryView());
            }
            StartCoroutine(ToggleFurnaceView());
        }
        
    }

    // Called by UIManager.cs to toggle the Pause Menu container gameobject as active or inactive.
    public IEnumerator ToggleInventoryView() {
        //midTransition = true;
        if (inventoryActive) {
            invenAnimator.SetTrigger("InventoryToHotbar");
        } else {
            invenAnimator.SetTrigger("HotbarToInventory");
        }
        inventoryActive = !inventoryActive;
        yield return new WaitForSeconds(1.083f);
        //midTransition = false;
    }

    public IEnumerator ToggleFurnaceView() {
        //midTransition = true;
        if (craftingActive) {
            invenAnimator.SetTrigger("CloseFurnace");
        } else {
            invenAnimator.SetTrigger("OpenFurnace");
        }
        craftingActive = !craftingActive;
        yield return new WaitForSeconds(0.550f);
        //midTransition = false;
    }
}

    
