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
            doing.onComplete = ()=>{myInventory.updateProgressBar(0);};
            doing.onProgress = (d)=>{myInventory.updateProgressBar((float)d);};
        }
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
        // automatically searches for the items needed to complete the given recipe and consumes them and goes into the output buffer
        doing = new Recipe(Globals.allRecipes["ironOreToBar"]);
        
        myInventory = GetComponentInChildren<FurnaceInventory>();

        myInventory[0] = inpBuf;
        myInventory[1] = outBuf;


        //inpBuf.Add(new ItemStack(ItemType.ORE_IRON, 100));
    }
    
    public override void Set_Recipe(Recipe recipe)
    {
        doing = recipe;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponentInChildren<Canvas>().enabled = false;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void NotifyDamage()
    {
        throw new NotImplementedException();
    }
}
