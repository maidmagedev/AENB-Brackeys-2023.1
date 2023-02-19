using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : Machine, IODevice
{
    private ItemCollection IOBuf = new(1);

    [SerializeField] private GameObject arm;
    [SerializeField] private SpriteRenderer item; 

    private float initialArmAngle;

    float delay = 1f;

    public Grabber(){
        type = MachineType.GRABBER;

        footPrint = new(1,1);

        child_start = GrabberStart;
    }

    Vector2Int intendTake, intendPushTo;

    void GrabberStart()
    {
        intendTake = new Vector2Int((int)position.x, (int)position.y) + OrientationHelper.takeFromBind[orientation];
        intendPushTo = new Vector2Int((int)position.x, (int)position.y) + OrientationHelper.pushToBind[orientation];

        IOBuf.AddListener((evt=>{
            if (evt.changeType == ChangeType.ADD){
                item.sprite = Globals.item_definitions[IOBuf[0].of].sprite;
            }
            else if (evt.changeType == ChangeType.REMOVE){
                item.sprite = null;
            }
        }));

        switch (orientation){
            case Orientation.LR:
                arm.transform.eulerAngles = new Vector3(0, 0, 180);
                break;
            case Orientation.UD:
                arm.transform.eulerAngles = new Vector3(0, 0, 90);
                break;
            case Orientation.DU:
                arm.transform.eulerAngles = new Vector3(0, 0, 270);
                break;
            case Orientation.RL:
                arm.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
        }

        //initialArmAngle = arm.transform.eulerAngles.z;
        //currentAngle = initialArmAngle;
    }


    public override void Update()
    {
        if(!working && IOBuf.Count == 0){
            TileData takingFrom;

            TileManager.tileData.TryGetValue(intendTake, out takingFrom);

            Machine take = takingFrom != null ? takingFrom.occupiedBy : null;

            if(take != null){
                var items = take.getOutputBuffer().Extract(1);

                if (items != null){
                    
                    working = true;
                    //print("work start");
                    IOBuf.Add(items);
                    GetComponent<Animator>().SetTrigger("receiveItem");
                    StartCoroutine(doCooldown(()=>{}));
                }
            }
        }
        else if (!working){
            TileData putTo;

            TileManager.tileData.TryGetValue(intendPushTo, out putTo);

            Machine put = putTo != null ? putTo.occupiedBy : null;

            if(put != null){
                var items = put.getInputBuffer().Add(IOBuf[0]).more;

                if (items != null){
                    
                    working = true;
                    //print("work start");
                    int transferred = IOBuf[0].quantity - items.quantity;
                    IOBuf.Remove(new ItemStack(items.of, transferred));

                    
                    StartCoroutine(doCooldown(()=>{}));
                }
                else{
                    
                    working = true;
                    //print("work start");
                    IOBuf.Remove(IOBuf[0]);
                    StartCoroutine(doCooldown(()=>{}));
                }
            }
        }

        //Find IODevice "behind", remove 1, wait (time)
        //then, attempt to find IODevice "ahead", add 1, wait (time)
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


    public override ItemCollection getInputBuffer()
    {
        return IOBuf;
    }

    public override ItemCollection getOutputBuffer()
    {
        return IOBuf;
    }


    public override void RotationToOrientation(Vector3 rotation)
    {
        switch(rotation.z % 360){
            case 0:
                orientation = Orientation.RL;
            break;
            case 90:
                orientation = Orientation.UD;
            break;
            case 180:
                orientation = Orientation.LR;
            break;
            case 270:
                orientation = Orientation.DU;
            break;
        }

        transform.eulerAngles = new Vector3(0,0,0);
    }
}
