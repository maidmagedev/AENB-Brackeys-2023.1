using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : LockedToGrid, IODevice
{
    public Vector2Int footPrint;

    private Recipe _doing;

    public Recipe doing{get {return _doing;} set{_doing = value;}}

    public ItemCollection inpBuf;

    public ItemCollection outBuf;

    public MachineType type;

    public bool working = false;

    public Orientation orientation;

    Dictionary<Vector2Int, TileData> myPositions = new();

    public Action child_start;
    public Action child_update;

    private Dictionary<MachineType, ItemType> machineItemMapping = new(){
        {MachineType.ASSEMBLER, ItemType.ASSEMBLER},
        {MachineType.BELT, ItemType.BELT},
        {MachineType.GRABBER, ItemType.GRABBER},
        {MachineType.SPITTER, ItemType.SPITTER},
        {MachineType.FURNACE, ItemType.FURNACE},
        {MachineType.MINER, ItemType.MINER}
        
    };
    

    public virtual void Start()
    {
        RotationToOrientation(transform.eulerAngles);
        if (child_start != null){
            child_start.Invoke();
        }
        position = transform.position;

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
                
                TileData tryOut = null;
                TileManager.tileData.TryGetValue(varPos, out tryOut);

                if (tryOut != null){

                    Destroy(this.gameObject);

                    var pickup = Instantiate(Resources.Load<GameObject>("Items/GenericPickup"), transform.position + new Vector3(0,2,0), Quaternion.identity);
                    pickup.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

                    //pickup.GetComponent<PickUp>().Init(machineItemMapping[type], 1);
                    pickup.GetComponent<PickUp>().setItem(machineItemMapping[type], 1);
                    
                    return;
                }
            }
        }

        foreach (var key in myPositions.Keys)
        {
            TileManager.tileData.Add(key, new TileData(key, this));
        }
        

    }

    public void Delete(){
        foreach (Vector2Int pos in myPositions.Keys){
            TileManager.tileData.Remove(pos);
        }
        Destroy(this.gameObject);
    }

    public virtual void Set_Recipe(Recipe recipe)
    {
        doing = recipe;
    }
    public virtual void Update()
    {
        if (child_update != null){
            child_update.Invoke();
        }

        if (!working && _doing != null && _doing.accept(inpBuf))
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

    public virtual void RotationToOrientation(Vector3 rotation){

    }


}

public enum MachineType
{
    INVENTORY,
    ASSEMBLER,
    FURNACE,
    GRABBER,
    BELT,
    MINER,
    SPITTER,
    ROCKET
}