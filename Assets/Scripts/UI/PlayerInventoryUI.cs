using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryUI : MonoBehaviour
{
    [Header("External Components")]
    [SerializeField] Animator invenAnimator;
    [SerializeField] Settings settings;
    [SerializeField] Crosshair_Canvas crosshairCanv;

    [Header("UI Elements")]
    [SerializeField] GameObject InventoryUI;
    public GameObject[] hotbarItems; // must be of size 4.

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
            crosshairCanv.SetCrosshairVisibility(true);
        } else {
            crosshairCanv.SetCrosshairVisibility(false);
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

    // Enables the UI visual for the selected item slot.
    // Expected input, 0, 1, 2, 3.
    public void EnableHotbarSlot(int newActive, int oldActive) {
        hotbarItems[oldActive].SetActive(false);
        hotbarItems[newActive].SetActive(true);
    }
}

    
