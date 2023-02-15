using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    public static Dictionary<Vector2Int, TileData> tileData = new();
    private static bool LockTilesToGrid = false;

}


public class TileData{
    public Vector2Int pos;

    public Machine occupiedBy;

    public ResourceDeposit Deposit;


    public TileData(Vector2Int pos, Machine occupiedBy){
        this.pos = pos;
        this.occupiedBy = occupiedBy;
    }

    public TileData(Vector2Int pos, ResourceDeposit deposit)
    {
        this.pos = pos;
        this.Deposit = deposit;
    }
    //others as necessary: items, ore, etc
}