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
    [SerializeField] GameObject FurnaceUI;
    [SerializeField] GameObject AssemblerUI;
    [SerializeField] GameObject CraftingUI;

    public GameObject[] hotbarItems; // must be of size 4.

    public bool inventoryActive = false;
    //private bool midTransition = false;
    private bool secondaryActive = false; // if the secondary menu is active, this could be a furnace, assembler, or crafting menu.
    public SecondaryMenu secondaryMenu = SecondaryMenu.Furnace;

    public enum SecondaryMenu {
        Furnace,
        Assembler,
        Crafting
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(settings.inventoryKey)) {
            if (secondaryActive) {
                // guaranteed to disable secondaries due to logic chain
                ToggleSecondaries();
            }
            StartCoroutine(ToggleInventoryView());
        } /*else if (Input.GetKeyDown(settings.interactKey)) {    // I commented this out so pressing "E" can be used somewhere else --Joseph
            if (!inventoryActive) {
                StartCoroutine(ToggleInventoryView());
            }
            ToggleSecondaries();
        }*/
        
    }

    private void ToggleSecondaries() {
        //Secondary Menu Details---------------
        switch(secondaryMenu) {
            case SecondaryMenu.Furnace:
                StartCoroutine(ToggleFurnaceView());
                break;
            case SecondaryMenu.Assembler:
                StartCoroutine(ToggleAssemblerView());
                break;
            case SecondaryMenu.Crafting:
                StartCoroutine(ToggleCraftingView());
                break;
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

    public void SetInventoryView(bool enabled)
    {
        if (enabled)
        {
            invenAnimator.SetTrigger("HotbarToInventory");
            inventoryActive = true;
        }
        else
        {
            invenAnimator.SetTrigger("InventoryToHotbar");
            inventoryActive = false;
        }
    }

    public IEnumerator ToggleFurnaceView() {
        //midTransition = true;
        if (secondaryActive) {
            invenAnimator.SetTrigger("CloseFurnace");
        } else {
            invenAnimator.SetTrigger("OpenFurnace");
        }
        secondaryActive = !secondaryActive;
        yield return new WaitForSeconds(0.550f);
        //midTransition = false;
    }

    public IEnumerator ToggleAssemblerView() {
        //midTransition = true;
        if (secondaryActive) {
            invenAnimator.SetTrigger("CloseAssembler");
        } else {
            invenAnimator.SetTrigger("OpenAssembler");
        }
        secondaryActive = !secondaryActive;
        yield return new WaitForSeconds(0.550f);
    }

    public IEnumerator ToggleCraftingView() {
        //midTransition = true;
        if (secondaryActive) {
            //invenAnimator.SetTrigger("CloseCrafting");
        } else {
            //invenAnimator.SetTrigger("OpenCrafting");
        }
        secondaryActive = !secondaryActive;
        yield return new WaitForSeconds(0.550f);
    }

    // Enables the UI visual for the selected item slot.
    // Expected input, 0, 1, 2, 3.
    public void EnableHotbarSlot(int newActive, int oldActive) {
        hotbarItems[oldActive].SetActive(false);
        hotbarItems[newActive].SetActive(true);
    }
}

    
