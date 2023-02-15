using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner_Inventory : BaseInventory
{
    // supposedly this is called when added to an object
    public Miner_Inventory()
    {

        inventory = new ItemCollection(10);

    }

}
