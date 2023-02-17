using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : Machine, IODevice
{
    private ItemCollection IOBuf = new(4);

    public Orientation orientation;

    float delay = .5f;

    public Belt(){
        type = MachineType.BELT;
        footPrint = new(1,1);
        child_start = BeltStart;

         IOBuf.AddListener(evt=>{
            if (evt.changeType == ChangeType.ADD){
                itemSlots[evt.affectedindices[0]].sprite = Item.item_definitions[IOBuf[evt.affectedindices[0]].of].sprite;
            }
            else if (evt.changeType == ChangeType.REMOVE){
                foreach(int ind in evt.affectedindices){
                    itemSlots[ind].sprite = null;
                }
            }
        });
    }
    
    Vector2Int intendPushTo;

    public Sprite def;

    [SerializeField]private List<SpriteRenderer> itemSlots;

    void BeltStart()
    {
        intendPushTo = new Vector2Int((int)position.x, (int)position.y) + OrientationHelper.pushToBind[orientation];

        switch (orientation){
            case Orientation.LR:
                transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            case Orientation.UD:
                transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case Orientation.RL:
                transform.eulerAngles = new Vector3(0, 0, 90);
                break;
        }
    }

    public int c = 0;
    public override void Update()
    {
        c = IOBuf.Count;
        if (!working){
            TileData putTo;

            TileManager.tileData.TryGetValue(intendPushTo, out putTo);

            Machine put = putTo != null && putTo.occupiedBy.type == MachineType.BELT ? putTo.occupiedBy : null;

            if(put != null){
                working = true;

                if (IOBuf[0] != null){
                    
                    StartCoroutine(doCooldown(()=>{
                        //int ind = put.getInputBuffer().Reverse().FindIndex(st=>st!=null);
                        if (put.getInputBuffer()[3] == null){
                            put.getInputBuffer()[3] = IOBuf[0];
                            IOBuf[0] = null;
                        }
                        
                    }));
                    
                }
                else{
                    StartCoroutine(doCooldown(()=>advanceBelt()));
                }

                

                
            }
            else{
                working = true;
                StartCoroutine(doCooldown(()=>advanceBelt()));
            }
        }
    }


    public IEnumerator doCooldown(Action onFinish){
        float workingTime = 0;
        while (workingTime < delay)
        {
            workingTime += Time.deltaTime;
            //print("frame");
            yield return null;
        }

        onFinish.Invoke();
        working = false;
        //print("workFin");
    }


    public void advanceBelt(){
        for (int i = 0; i < IOBuf.Size - 1; i++){
            if (IOBuf[i] == null && IOBuf[i+1] != null){
                IOBuf[i] = IOBuf[i+1];
                IOBuf[i+1] = null;
            }
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
