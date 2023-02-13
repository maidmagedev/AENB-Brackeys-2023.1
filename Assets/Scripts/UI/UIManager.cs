using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] KeyCode pauseKey = KeyCode.Escape;
    [SerializeField] KeyCode inventoryKey = KeyCode.I;

    [Header("Scripts")]
    [SerializeField] PlayerInventory playerInventoryScript;
    [SerializeField] PauseMenu pauseMenuScript;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        getInputs();
    }

    void getInputs() {
        if (Input.GetKeyDown(pauseKey)) {
            pauseMenuScript.CyclePauseMenuView();
        }
        if (Input.GetKeyDown(inventoryKey)) {
            playerInventoryScript.ToggleInventoryView();
        }
    }
}
