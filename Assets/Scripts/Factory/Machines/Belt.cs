using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : Machine, IODevice
{
    private ItemCollection IOBuf = new(6);

    public Orientation orientation;

    float delay = .5f;

    public Belt(){
        type = MachineType.BELT;
        footPrint = new(1,1);
        child_start = BeltStart;

    }
    
    Vector2Int intendPushTo;

    public Sprite def;

    void BeltStart()
    {
        intendPushTo = new Vector2Int((int)position.x, (int)position.y) + OrientationHelper.pushToBind[orientation];
    }

    public override void Update()
    {
        if(IOBuf[0] != null){
            GetComponent<SpriteRenderer>().sprite = Item.item_definitions[IOBuf[0].of].sprite;
        }
        else{
            GetComponent<SpriteRenderer>().sprite = def;
        }

        if (!working){
            TileData putTo;

            TileManager.tileData.TryGetValue(intendPushTo, out putTo);

            Machine put = putTo != null && putTo.occupiedBy.type == MachineType.BELT ? putTo.occupiedBy : null;

            if(put != null){
                
                ItemStack items = null;


                if (IOBuf[0] != null){
                    working = true;
                    StartCoroutine(doCooldown(()=>{
                        items = put.getInputBuffer().Add(IOBuf[0]).more;
                        
                        if (items != null){
                            int transferred = IOBuf[0].quantity - items.quantity;
                            IOBuf.Remove(new ItemStack(items.of, transferred));
                        }
                        else if (IOBuf[0] != null){
                    
                            IOBuf.Remove(IOBuf[0]); 
                            advanceBelt();
                        }
                    }));
                    
                }

                

                
            }
        }
    }


    private float workingTime = 0;

    public IEnumerator doCooldown(Action onFinish){
        while (workingTime < delay)
        {
            workingTime += Time.deltaTime;
            //print("frame");
            yield return null;
        }

        onFinish.Invoke();
        workingTime = 0;
        working = false;
        //print("workFin");
    }


    public void advanceBelt(){
        if(IOBuf[0] != null){
            throw new Exception("push failed?");
        }
        else{
            for (int i = 0; i < IOBuf.Size - 2; i++){
                IOBuf[i] = IOBuf[i+1];
            }

            IOBuf[IOBuf.Size -1] = null;
        }
    }
    
    public override ItemCollection getInputBuffer()
    {
        return IOBuf;
    }

    public override ItemCollection getOutputBuffer()
    {
        return IOBuf;
    }
}
