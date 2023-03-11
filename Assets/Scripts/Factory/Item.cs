using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Data
{

    public int maxStack;

    public Sprite sprite;

    public GameObject g;
    public Action<PlayerInventory, ItemStack> useBehavior;

    public Item_Data(ItemType type, int maxStack, Sprite sprite, GameObject g, UseBehavior useBehavior){
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


    public bool placing = false;
    

    public GameObject inWorldPreview;

    public bool canRotate;

    public void Place(PlayerInventory host, ItemStack stack){
        if (!placing){
            GameObject preview = GameObject.Instantiate(Resources.Load<GameObject>("MachinePreview"));
            inWorldPreview = GameObject.Instantiate(preview, new Vector3(0,0,0), Quaternion.identity);
            inWorldPreview.GetComponent<SpriteRenderer>().sprite = Globals.item_definitions[stack.of].sprite;


            if (stack.of == ItemType.FURNACE || stack.of == ItemType.MINER){
                canRotate = false;
            }
            else{
                canRotate = true;
            }
        }
        else{
            //confirm place

            Vector3 position = inWorldPreview.transform.position;
            Quaternion rotation = inWorldPreview.transform.rotation;
            GameObject.Destroy(inWorldPreview);
            GameObject.Instantiate(Globals.item_definitions[stack.of].g, position, rotation);
            host.Remove(0, new ItemStack(stack.of, 1));
        }

        placing = !placing;
    }

    public void Drop(PlayerInventory host, ItemStack stack){
        if(!GameObject.FindObjectOfType<PlayerInventoryUI>().GetComponent<PlayerInventoryUI>().inventoryActive){
            host.Remove(0, stack);

            var n = GameObject.Instantiate(g, GameObject.FindWithTag("Player").transform.position + new Vector3(0,2,0), Quaternion.identity);
            n.GetComponent<PickUp>().num = stack.quantity;
        }
    }

    public void Shoot(PlayerInventory ignored, ItemStack stack){
        switch (stack.of){
            case ItemType.FAMAS:
                GameObject.FindObjectOfType<Famas>().GetComponent<Famas>().GetInput();
            break;
            case ItemType.SHOTGUN:
                GameObject.FindObjectOfType<Shotgun>().GetComponent<Shotgun>().GetInput();
            break;
            case ItemType.PISTOL:
                GameObject.FindObjectOfType<Pistol>().GetComponent<Pistol>().GetInput();
            break;
            case ItemType.BOLTACTION:
                GameObject.FindObjectOfType<BoltAction>().GetComponent<BoltAction>().GetInput();
            break;
            case ItemType.QUEST:
                Debug.Log("shhhhh... im abusing the logic.... no one needs to know... - spencer :)");
                break;
            default:
                throw new NotImplementedException("New gun type detected. see Item.cs for firing behavior.");
        }
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
    GRABBER,
    BELT,
    SPITTER,
    ASSEMBLER,
    PISTOL,
    QUEST,
    BOLTACTION
}
