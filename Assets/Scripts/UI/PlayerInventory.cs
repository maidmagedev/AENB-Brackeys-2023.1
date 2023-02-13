using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject InventoryUI;
    [SerializeField] Animator invenAnimator;
    [SerializeField] Settings settings;
    private bool inventoryActive = false;
    private bool midTransition = false;

    [Header("Item References")]
    [SerializeField] GameObject[] icons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!midTransition && Input.GetKeyDown(settings.inventoryKey)) {
            StartCoroutine(ToggleInventoryView());
        }
    }

    // Called by UIManager.cs to toggle the Pause Menu container gameobject as active or inactive.
    public IEnumerator ToggleInventoryView() {
        midTransition = true;
        if (inventoryActive) {
            invenAnimator.SetTrigger("InventoryToHotbar");
        } else {
            invenAnimator.SetTrigger("HotbarToInventory");
        }
        inventoryActive = !inventoryActive;
        yield return new WaitForSeconds(1.1f);
        midTransition = false;
    }
}

    
