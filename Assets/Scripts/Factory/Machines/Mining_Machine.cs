using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Miner_Inventory))]
public class Mining_Machine : Machine, IKillable
{
    public new Recipe doing{
        get {return base.doing;} 
        set{
            base.doing = value;
            // could put my own custom logic here
            doing.onComplete = ()=>{outPut();};
        }
    }

    // This generates a new prefab for each item that is mined.
    // It would be much more efficient to use one prefab and update the quantity instead
    private void outPut()
    {
        // instantiate prefab
        GameObject pickup = Instantiate(Resources.Load<GameObject>("Items/GenericPickup"), 
                new Vector3(transform.position.x - 2, transform.position.y, 0),
                Quaternion.identity);
        pickup.GetComponent<PickUp>().setItem(outBuf[0].typeOf, outBuf[0].quantity);
        outBuf[0] = null;
    }
    public Mining_Machine()
    {
        inpBuf = null;
        outBuf = new(1);
        type = MachineType.MINER;
        footPrint = new(2, 2);
        child_start = Mining_Start;
        //GetComponent<Miner_Inventory>().inventory = outBuf; 
    }

    private void Mining_Start()
    {
        //GetComponent<Miner_Inventory>().inventory[0] = outBuf;
        // change this to decide what ore to mine
        doing = new Recipe(Globals.allRecipes["ironOreMiner"]);
    }

    public void setDoing(string recipe)
    {
        doing = new Recipe(Globals.allRecipes[recipe]);
    }


    public override ItemCollection getOutputBuffer()
    {
        return outBuf;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void NotifyDamage()
    {
        throw new System.NotImplementedException();
    }
}
