using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// this is a little screwy and should generally only be used for the player. Otherwise there is the possibility of strange visual issues.
public class TriggerSpriteLayer : TriggerBase
{
    [SerializeField] bool changeToLayer;
    [SerializeField] string layerDestination;
    [SerializeField] bool changeSortingOrder;
    [SerializeField] int sortingOrderDestination;
    [SerializeField] TilemapRenderer client;

    public override void DoAction()
    {
        if (client == null)
        {
            return;
        }

        if (changeToLayer)
        {
            Debug.Log("Changing Layer.");
            client.sortingLayerName = layerDestination;
        }

        if (changeSortingOrder)
        {
            Debug.Log("Changing order.");
            client.sortingOrder = sortingOrderDestination;
        }


    }
}
