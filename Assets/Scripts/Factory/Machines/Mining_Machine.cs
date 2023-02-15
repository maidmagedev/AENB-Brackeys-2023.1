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
    }

    private void Start()
    {
        //iron_ore = Resources.Load<GameObject>("Items/Iron Ore");
        inpBuf = null;
        outBuf = new(1);
        doing = new Recipe(Globals.allRecipes["ironOreMiner"]);
    }
    private void Update()
    {
        print(outBuf.Count);
        if (outBuf.Count > 0)
        {
            Instantiate(iron_ore, new Vector3Int((int)this.position.x, (int)this.position.y + 4, 0),Quaternion.identity);
            outBuf.Remove(outBuf[0]);
        }
    }
}
