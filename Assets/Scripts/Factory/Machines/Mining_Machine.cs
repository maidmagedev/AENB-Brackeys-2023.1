using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEditor;
using UnityEngine;

public class Mining_Machine : Machine
{
    public GameObject iron_ore;
    public Mining_Machine()
    {
        inpBuf = null;
        outBuf = new(1);
        type = MachineType.MINER;
        footPrint = new(3, 3);
        child_start = Mining_Start;
        child_update = Mining_Update;
    }

    private void Mining_Start()
    {
        //iron_ore = Resources.Load<GameObject>("Items/Iron Ore");
        inpBuf = new(0);
        outBuf = new(1);
        doing = new Recipe(Globals.allRecipes["ironOreMiner"]);
    }
    private void Mining_Update()
    {
        if (outBuf.Count > 0)
        {
            //Instantiate(iron_ore, new Vector3Int((int)this.position.x, (int)this.position.y + 1, 0),Quaternion.identity);
            GetComponent<Miner_Inventory>().inventory.Add(new ItemStack(ItemType.ORE_IRON, 1));
            outBuf.Remove(outBuf[0]);
        }
    }
    public override ItemCollection getInputBuffer()
    {
        return inpBuf;
    }

    public override ItemCollection getOutputBuffer()
    {
        return outBuf;
    }
}
