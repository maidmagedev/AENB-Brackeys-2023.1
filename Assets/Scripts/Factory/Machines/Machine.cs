using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Drag_and_Drop, IODevice
{
    public Vector2Int footPrint;

    public Recipe doing;

    public ItemCollection inpBuf;

    public ItemCollection outBuf;

    public MachineType type;

    public bool working = false;

    Dictionary<Vector2Int, TileData> myPositions = new();

    public Action child_start;
    public Action child_update;

    public void Start()
    {
        child_start.Invoke();
        OnMouseUp();

        var pos = new Vector2Int((int)position.x, (int)position.y);


        var minusOffset = Mathf.FloorToInt((footPrint.x - 1) /2);
        var plusOffset = Mathf.FloorToInt(footPrint.x / 2);

        //1->0 2->0 3->1 4->1 5->2 6->2
        var left = pos.x - minusOffset;
        //0 1 1 2 2 3
        var right = pos.x + plusOffset;

        var bottom = pos.y - minusOffset;
        var top = pos.y + plusOffset;

        for(int x = left; x <= right; x++){
            for (int y = bottom; y<= top; y++){
                var varPos = new Vector2Int(x,y);
                myPositions.Add(varPos, new TileData(varPos, this));
                TileManager.tileData.Add(varPos, new TileData(varPos, this));
            }
        }

    }

    public void Delete(){
        foreach (Vector2Int pos in myPositions.Keys){
            TileManager.tileData.Remove(pos);
        }
        Destroy(this.gameObject);
    }

    public void Update()
    {
        child_update.Invoke();
        if (!working && doing != null && doing.accept(inpBuf))
        {
            working = true;
            doing.consume(ref inpBuf);

            StartCoroutine(doing.doCraft(this));
        }
    }

    public virtual ItemCollection getInputBuffer()
    {
        return inpBuf;
    }

    public virtual ItemCollection getOutputBuffer()
    {
        return outBuf;
    }
}

public enum MachineType
{
    INVENTORY,
    ASSEMBLER,
    FURNACE,
    GRABBER,
    BELT,
    MINER
}