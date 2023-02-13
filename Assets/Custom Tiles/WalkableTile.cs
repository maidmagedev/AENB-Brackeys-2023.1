using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// How to use this class:
/*
 * Make a prefab called "WalkableTile" that contains at minimum a sprite renderer and this script.
 * Make a tilemap grid at (0,0,0) in your scene, and create a tilemap child inside it at (0.5,0.5,0) relative to the grid
 * Drag and drop one "WalkableTile" into the tilemap child and line it up with the grid.
 * Use the GameObject brush and the eye-dropper tool to select and paint as many tiles as you want into your scene.
 */
public class WalkableTile : BaseTile
{
    private SpriteRenderer tileSprite;
    // Start is called before the first frame update
    void Start()
    {
        tileSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        
    }

    /*private void OnMouseEnter()
    {
        TileManager tm = FindObjectOfType<TileManager>();
        PathFinder pf = FindObjectOfType<PathFinder>();
        PlayerPathFinder ppf = FindObjectOfType<PlayerPathFinder>();

        if (tm.MayFindPath)
        {
            tileSprite.color = Color.gray;
            List<Vector2Int> path = pf.Dijkstra(ppf.GetPlayerPos(), new Vector2Int((int)transform.position.x, (int)transform.position.y));

            //print("path destination: " + transform.position.x + " " + transform.position.y);
            foreach (var tilePosition in path)
            {
                TileManager.WalkableTilePositions[tilePosition].GetComponent<SpriteRenderer>().color = Color.gray;
            }
            //print("Path end");
        }
    }*/

   /* private void OnMouseExit()
    {
        tileSprite.color = Color.white;
        FindObjectOfType<PathFinder>().clearPath();
    }*/
}
