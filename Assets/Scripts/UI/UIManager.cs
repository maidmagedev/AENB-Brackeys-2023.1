using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PlayerInventoryUI playerInventoryUIScript;
    [SerializeField] PauseMenu pauseMenuScript;
    [SerializeField] Settings settings;


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
        if (Input.GetKeyDown(settings.pauseKey)) {
            pauseMenuScript.CyclePauseMenuView();
        }
        if (Input.GetKeyDown(settings.inventoryKey)) {
            playerInventoryUIScript.ToggleInventoryView();
        }
    }
}
