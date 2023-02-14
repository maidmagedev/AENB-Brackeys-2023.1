using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TileManager
{
    public static Dictionary<Vector2Int, TileData> tileData;
    private static bool LockTilesToGrid = false;

}


public class TileData{
    public Vector2Int pos;

    public Machine occupiedBy;

    //others as necessary: items, ore, etc
}