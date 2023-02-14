using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : Drag_and_Drop, IODevice
{
    public Vector2Int footPrint;

    Recipe doing;

    public ItemCollection inpBuf;

    public ItemCollection outBuf;

    public MachineType type;

    public bool working = false;

    void Start()
    {
        OnMouseUp();


        TileManager.tileData[new Vector2Int((int)position.x, (int)position.y)].occupiedBy = this;
    }

    private void Update()
    {

        if (!working && doing != null && inpBuf.Count > 0 && doing.accept(inpBuf))
        {
            working = true;
            doing.consume(ref inpBuf);

            StartCoroutine(doing.doCraft(this));
        }
    }

    public ItemCollection getInputBuffer()
    {
        return inpBuf;
    }

    public ItemCollection getOutputBuffer()
    {
        return outBuf;
    }
}

public enum MachineType
{
    INVENTORY,
    ASSEMBLER,
    FURNACE
}