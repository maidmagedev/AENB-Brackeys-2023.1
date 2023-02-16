using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles Hotbar Management and other input controls from the player, including attacks.
public class PlayerActions : MonoBehaviour
{
    private int selectedItemSlot; // valid num: 0, 1, 2, 3,
    [SerializeField] PlayerInventoryUI invenUI;

    private void Start() {
        selectedItemSlot = 0;
        invenUI.EnableHotbarSlot(0, 0);
    }

    private void Update() {
        GrabInput();
    }

    private void GrabInput() {
        // i wish this was a switch statement
        int oldSlot = selectedItemSlot;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            selectedItemSlot = 0;
            Debug.Log("Item 1");
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            selectedItemSlot = 1;
            Debug.Log("Item 2");
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            selectedItemSlot = 2;
            Debug.Log("Item 3");
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            selectedItemSlot = 3;
            Debug.Log("Item 4");
        }
        if (oldSlot != selectedItemSlot) {
            invenUI.EnableHotbarSlot(selectedItemSlot, oldSlot);
        }
    }

}
