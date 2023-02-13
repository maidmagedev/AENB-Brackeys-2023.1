using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Machine[] allTiles;
    [SerializeField] private bool LockTilesToGrid = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // locks out of place tiles onto the grid when set in the editor
        if (LockTilesToGrid)
        {

            allTiles = FindObjectsOfType<Machine>();
            foreach (Machine tile in allTiles)
            {
                tile.transform.position = new Vector3((float)Math.Round(tile.transform.position.x, 0), (float)Math.Round(tile.transform.position.y, 0), 0);
            }
        }
    }
}
