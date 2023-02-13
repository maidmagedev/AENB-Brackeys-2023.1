using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject InventoryUI;
    [SerializeField] Animator invenAnimator;
    [SerializeField] Settings settings;
    private bool inventoryActive = false;
    public bool midTransition = false;

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

    
