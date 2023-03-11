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
    [SerializeField] PlayerAnimations playerAnims;
    [SerializeField] TopDownMovementComponent topDownMovementComponent;
    [SerializeField] GameObject[] hands;

    [Header("Weapons")]
    [SerializeField] Famas famas;
    [SerializeField] GameObject famasObj;
    [SerializeField] Shotgun shotgun;
    [SerializeField] GameObject shotgunObj;
    [SerializeField] Pistol pistol;
    [SerializeField] GameObject pistolObj;
    [SerializeField] BoltAction boltAction;
    [SerializeField] GameObject boltActionObj;

    private void Start() {
        selectedItemSlot = 0;
        invenUI.EnableHotbarSlot(0, 0);
    }

    private void Update() {
        HotbarSelect();
        
        // Action Handling based on Selected Item
        if (ItemInSlotExists(selectedItemSlot) && Input.GetKeyDown(KeyCode.Mouse0)) {

            //print("11111");
            Globals.item_definitions[selectedItem].useBehavior(playerInv, playerInv[0][selectedItemSlot]);
        }
        if (Globals.item_definitions[selectedItem].placing == true && Globals.item_definitions[selectedItem].canRotate && Input.GetKeyDown(KeyCode.Mouse1)){
            Globals.item_definitions[selectedItem].inWorldPreview.transform.Rotate(new Vector3(0,0,90), Space.Self);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            Debug.Log("DashRoll");
            StartCoroutine(playerAnims.addRoll());
            //StartCoroutine(dashMovement());
        }
    }

    private IEnumerator dashMovement() {
        topDownMovementComponent.DisableMovement();
        yield return new WaitForSeconds(0.667f);
        topDownMovementComponent.EnableMovement();
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
            Globals.item_definitions[selectedItem].placing = false;
            Destroy(Globals.item_definitions[selectedItem].inWorldPreview);
            invenUI.EnableHotbarSlot(selectedItemSlot, oldSlot);
            UpdateSelectedItem();
        }
    }

    // Disables all hotbar item objects. Hides the famas, and other active items.
    private void DisableItemObjects() {
        famasObj.SetActive(false);
        shotgunObj.SetActive(false);
        pistolObj.SetActive(false);
        boltActionObj.SetActive(false);
    }

    private void EnableHands(bool on) {
        hands[0].SetActive(on);
        hands[1].SetActive(on);
    }

    private ItemType GetItemTypeFromSlotNum(int slotNum) {

        return playerInv.inventory[0][slotNum].of;
    }

    private bool ItemInSlotExists(int slotNum) {
        return playerInv.inventory[0][selectedItemSlot] != null;
    }

    public void UpdateSelectedItem() {
        DisableItemObjects();
        if (ItemInSlotExists(selectedItemSlot)) {
            EnableHands(false);
            selectedItem = GetItemTypeFromSlotNum(selectedItemSlot);
            switch(selectedItem) {
                case ItemType.FAMAS:  
                    famasObj.SetActive(true); 
                    StartCoroutine(famas.Equip());              
                    break;
                case ItemType.SHOTGUN:
                    shotgunObj.SetActive(true);
                    StartCoroutine(shotgun.Equip());
                    break;
                case ItemType.PISTOL:
                    pistolObj.SetActive(true);
                    StartCoroutine(pistol.Equip());
                    break;
                case ItemType.BOLTACTION:
                    boltActionObj.SetActive(true);
                    StartCoroutine(boltAction.Equip());
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
