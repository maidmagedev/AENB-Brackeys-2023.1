using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles Hotbar Management and other input controls from the player, including attacks.
public class PlayerActions : MonoBehaviour
{
    private int selectedItemSlot; // valid num: 0, 1, 2, 3,
    private ItemType selectedItem;

    [SerializeField] PlayerInventory playerInv;
    [SerializeField] PlayerInventoryUI invenUI;
    [SerializeField] GameObject[] hands;

    [Header("Weapons")]
    [SerializeField] Famas famas;
    [SerializeField] GameObject famasObj;
    [SerializeField] Shotgun shotgun;
    [SerializeField] GameObject shotgunObj;

    private void Start() {
        selectedItemSlot = 0;
        invenUI.EnableHotbarSlot(0, 0);
    }

    private void Update() {
        HotbarSelect();
        
        // Action Handling based on Selected Item
        if (ItemInSlotExists(selectedItemSlot) && Input.GetKeyDown(KeyCode.Mouse0)) {
            switch(selectedItem) {
                case ItemType.FAMAS:  
                    famas.GetInput();
                    break;
                case ItemType.SHOTGUN:
                    shotgun.GetInput();
                    break;
            }
        }
    }

    private void HotbarSelect() {
        // Item Selection
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
            UpdateSelectedItem();
        }
    }

    // Disables all hotbar item objects. Hides the famas, and other active items.
    private void DisableItemObjects() {
        famasObj.SetActive(false);
        shotgunObj.SetActive(false);
    }

    private void EnableHands(bool on) {
        hands[0].SetActive(on);
        hands[1].SetActive(on);
    }

    private ItemType GetItemTypeFromSlotNum(int slotNum) {

        return playerInv.inventory[slotNum].of;
    }

    private bool ItemInSlotExists(int slotNum) {
        return playerInv.inventory[selectedItemSlot] != null;
    }

    public void UpdateSelectedItem() {
        DisableItemObjects();
        if (ItemInSlotExists(selectedItemSlot)) {
            EnableHands(false);
            selectedItem = GetItemTypeFromSlotNum(selectedItemSlot);
            switch(selectedItem) {
                case ItemType.FAMAS:  
                    famasObj.SetActive(true);               
                    break;
                case ItemType.SHOTGUN:
                    shotgunObj.SetActive(true);
                    break;
                default:
                    EnableHands(true);
                    break;
            }
        } else {
            EnableHands(true);
        }
        
        
    }

    private void UseItem() {
    }

}
