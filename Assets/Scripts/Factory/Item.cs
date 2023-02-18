using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.U2D;
using UnityEngine;

public class Item_Data
{

    public int maxStack;

    public Sprite sprite;

    public GameObject g;

    public Action useBehavior;

    public Item_Data(int maxStack, Sprite sprite, GameObject g, UseBehavior useBehavior){
        this.maxStack = maxStack;
        this.sprite = sprite;
        this.g = g;
        
        switch(useBehavior){
            case UseBehavior.SHOOT:
                this.useBehavior = Shoot;
                break;
            case UseBehavior.PLACE:
                this.useBehavior = Place;
                break;
            case UseBehavior.DROP:
                this.useBehavior = Drop;
                break;
            default:
                throw new NotImplementedException("Item missing useBehavior Specification!");

        }

    }
    
    
    public static List<Sprite> toSprites(List<ItemStack> items){
        List<Sprite> sprites =new();

        items.ForEach(item=>sprites.Add(Globals.item_definitions[item.of].sprite));

        return sprites;
    }


    public void Place(){

    }

    public void Drop(){
        
    }

    public void Shoot(){

    }

}

public enum UseBehavior{
    PLACE,
    SHOOT,
    DROP
}

public enum ItemType
{
    STONE,
    ORE_IRON,
    COAL,
    ORE_GOLD,
    IRON,
    GOLD,
    FAMAS,
    SHOTGUN,
    MINER,
    FURNACE,
    EMPTY
}
