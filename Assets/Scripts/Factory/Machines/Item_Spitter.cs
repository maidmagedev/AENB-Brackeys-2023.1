using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Spitter : Machine
{
    
    public Item_Spitter()
    {
        inpBuf = new(1);
        outBuf = new(0);  // could maybe make this null?
        type = MachineType.SPITTER;
        footPrint = new(3, 3);

        child_start = Spitter_Start;
    }

    public void Spitter_Start()
    {
        
        
    }

    public override void Update()
    {
        //print("spitCount: " + inpBuf[0]);
        if(inpBuf.Count > 0){
            ItemStack contents = inpBuf[0];
            Instantiate(Item.item_definitions[contents.of].g, new Vector3Int((int)this.position.x, (int)this.position.y + 2, 0),Quaternion.identity);
            inpBuf.Remove(contents);
            print("spitting");
        }
    }
}
