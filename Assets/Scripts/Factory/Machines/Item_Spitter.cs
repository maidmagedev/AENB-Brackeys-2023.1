using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spitter : Machine
{

    public GameObject iron_ore;
    public Item_Spitter()
    {
        inpBuf = new(1);
        outBuf = new(0);  // could maybe make this null?
        type = MachineType.SPITTER;
        footPrint = new(3, 3);

        child_start = Spitter_Start;
    }

    public void Spitter_Start(){
        iron_ore = Resources.Load<GameObject>("Items/Iron Ore");
    }

    public override void Update()
    {
        //print("spitCount: " + inpBuf[0]);
        if(inpBuf.Count > 0){
            ItemStack contents = inpBuf[0];

            inpBuf.Remove(contents);

            Instantiate(iron_ore, new Vector3Int((int)this.position.x, (int)this.position.y + 2, 0),Quaternion.identity);

            print("spitting");
        }
    }
}
