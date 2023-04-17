using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Furnace : Machine, IKillable
{

    private FurnaceInventory myInventory;

    public new Recipe doing{
        get {return base.doing;} 
        set{
            base.doing = value;
            // could put my own custom logic here
            doing.onComplete = ()=>{ myInventory.updateProgressBar(0); outPut();};
            doing.onProgress = (d)=>{myInventory.updateProgressBar((float)d);};
            doing.onConsume = () =>
            {
                // TODO: Update only the changed items, as opposed to clearing the entire inventory and re-instantiating
                // clear slot 0 and add the new inputbuffer
                var removed = myInventory.base_Remove(0);
                if (removed != null)
                {
                    Destroy(removed.gameObject);
                }
                myInventory.base_AddAt(inpBuf[0], 0);
                print("adding " + inpBuf[0] + " to input slot 0 of furnace inventory ");
                
                // clear slot 1 and add the new inputbuffer
                removed = myInventory.base_Remove(1);
                if (removed != null)
                {
                    Destroy(removed.gameObject);
                }
                myInventory.base_AddAt(inpBuf[1], 1);
                print("adding" + inpBuf[1] + " to input slot 1 of furnace inventory");
            };
        }
    }
    
    private void outPut()
    {
        myInventory.AddtoOutput(outBuf[0]);
        outBuf[0] = null;
    }

    
    public Furnace() {
        inpBuf = new(2);
        outBuf = new(1);
        type = MachineType.FURNACE;
        footPrint = new(2, 2);
        child_start = Furnace_Start;
    }
    
    private void Furnace_Start()
    {
        // default recipe
        doing = new Recipe(Globals.allRecipes["ironOreToBar"]);
        // setting inventory ref
        myInventory = GetComponentInChildren<FurnaceInventory>();
    }
    
    public override ItemCollection getOutputBuffer()
    {
        return outBuf;
    }
    public override void Set_Recipe(Recipe recipe)
    {
        doing = recipe;
    }
    
    public void Die()
    {
        Destroy(gameObject);
    }

    public void NotifyDamage()
    {
        //throw new System.NotImplementedException();
    }
    
    public void set_inpBuf()
    {
        // Furnace Inventory is null at the start because I disabled
        // the gameobject by default
        if (myInventory == null)
        {
            myInventory = GetComponentInChildren<FurnaceInventory>();
        }
        var inputItems = new ItemCollection(12, myInventory.getItems());
        inpBuf = inputItems;
        print("setting inpbuff");
        foreach (ItemStack VARIABLE in inputItems)
        {
            //print(VARIABLE.typeOf);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // press "E" to do this...
            GetComponentInChildren<Canvas>().enabled = true;
            FindObjectOfType<PlayerInventoryUI>().SetInventoryView(true);
        }
    }*/

    /*private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInChildren<Canvas>().enabled = false;
            FindObjectOfType<PlayerInventoryUI>().SetInventoryView(false);
        }
    }*/
    
}
