using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Machine[] allTiles;
    [SerializeField] private static bool LockTilesToGrid = false;


    void Start()
    {

        allTiles = FindObjectsOfType<Machine>();
        // locks out of place tiles onto the grid when set in the editor
        if (LockTilesToGrid)
        {
            foreach (Machine tile in allTiles)
            {
                tile.transform.position = new Vector3((float)Math.Round(tile.transform.position.x, 0), (float)Math.Round(tile.transform.position.y, 0), 0);
            }
        }
    }
}
