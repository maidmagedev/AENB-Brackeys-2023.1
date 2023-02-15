using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : Machine, IODevice
{
    private ItemCollection IOBuf = new(1);

    private Orientation orientation; 

    float delay = .6f;

    public Grabber(){
        type = MachineType.GRABBER;

        footPrint = new(1,1);
    }

    Vector2Int intendTake, intendPushTo;

    void Start()
    {
        intendTake = new Vector2Int((int)position.x, (int)position.y) + OrientationHelper.takeFromBind[orientation];
        intendPushTo = new Vector2Int((int)position.x, (int)position.y) + OrientationHelper.pushToBind[orientation];
    }


    private void Update()
    {
        if(!working){
            TileData takingFrom;

            TileManager.tileData.TryGetValue(intendTake, out takingFrom);

            Machine take = takingFrom != null ? takingFrom.occupiedBy : null;

            if(take != null){
                var items = take.getOutputBuffer().Extract(1);

                if (items != null){
                    IOBuf.Add(items);
                    working = true;
                    StartCoroutine(doCooldown());
                }
            }
        }

        //Find IODevice "behind", remove 1, wait (time)
        //then, attempt to find IODevice "ahead", add 1, wait (time)
    }

    private float workingTime = 0;

    public IEnumerator doCooldown(){
        while (workingTime < delay)
        {
            workingTime += Time.deltaTime;

            yield return null;
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
