using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : BaseInventory
{
    private void Awake()
    {
        // set layer to Player Inventory
        //int player_inventory_layer = LayerMask.NameToLayer("Player Inventory");
        //this.gameObject.layer = player_inventory_layer;
    }
}
