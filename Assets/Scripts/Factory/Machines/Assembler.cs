using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Assembler : Machine, IKillable
{

    private AssemblerInventory myInventory;
    [SerializeField] private Canvas interact_canvas;

    public new Recipe doing{
        get {return base.doing;} 
        set{
            base.doing = value;
            // could put my own custom logic here
            doing.onComplete = ()=>{myInventory.updateProgressBar(0); outPut();};
            doing.onProgress = (d)=>{myInventory.updateProgressBar((float)d);};
            doing.onConsume = () =>
            {
                // clear slot 0 and add the inputbuffer
                var removed = myInventory.base_Remove(0);
                if (removed != null)
                {
                    Destroy(removed.gameObject);
                }
                myInventory.base_AddAt(inpBuf[0], 0);
                print("adding " + inpBuf[0] + " to index 0");
                // clear slot 1 and add the inputbuffer
                removed = myInventory.base_Remove(1);
                if (removed != null)
                {
                    Destroy(removed.gameObject);
                }
                myInventory.base_AddAt(inpBuf[1], 1);
                
                // clear slot 2 and add the inputbuffer
                removed = myInventory.base_Remove(2);
                if (removed != null)
                {
                    Destroy(removed.gameObject);
                }
                myInventory.base_AddAt(inpBuf[2], 2);
            };
        }
    }

    private void outPut()
    {
        //myInventory.AddtoOutput(new ItemStack(ItemType.MINER, 1));
        //print("outbuf " + outBuf[0].typeOf + " " + outBuf[0].quantity);
        myInventory.AddtoOutput(outBuf[0]);
        outBuf[0] = null;
    }
    public Assembler() {
        inpBuf = new(3);
        outBuf = new(1);
        type = MachineType.ASSEMBLER;
        footPrint = new(3, 3);
        child_start = AssemblerStart;
    }

    private void AssemblerStart()
    {
        doing = new Recipe(Globals.allRecipes["Miner_Machine"]);
        myInventory = GetComponentInChildren<AssemblerInventory>();
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
        // Assembler Inventory is null at the start because I disabled
        // the gameobject by default
        if (myInventory == null)
        {
            myInventory = GetComponentInChildren<AssemblerInventory>();
        }
        var inputItems = new ItemCollection(12, myInventory.getItems());
        inpBuf = inputItems;
        print("setting inpbuff");
        foreach (ItemStack VARIABLE in inputItems)
        {
            //print(VARIABLE.typeOf);
        }
    }
}
