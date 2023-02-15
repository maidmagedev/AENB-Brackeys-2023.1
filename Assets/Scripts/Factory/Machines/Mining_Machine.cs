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
        MachineStart();
        //iron_ore = Resources.Load<GameObject>("Items/Iron Ore");
        inpBuf = new(0);
        outBuf = new(1);
        doing = new Recipe(Globals.allRecipes["ironOreMiner"]);
    }
    private void Update()
    {
        print("working:" + working);
        print("doing" + doing);
        print("inBuf" + inpBuf.Size);
        if (!working && doing != null && doing.accept(inpBuf))
        {
            working = true;
            doing.consume(ref inpBuf);

            StartCoroutine(doing.doCraft(this));
        }
        //print(outBuf.Count);
        if (outBuf.Count > 0)
        {
            Instantiate(iron_ore, new Vector3Int((int)this.position.x, (int)this.position.y + 4, 0),Quaternion.identity);
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
