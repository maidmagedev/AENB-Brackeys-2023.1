using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assembler : Machine, IKillable
{

    private AssemblerInventory myInventory;
    [SerializeField] private Canvas interact_canvas;

    public new Recipe doing{
        get {return base.doing;} 
        set{
            base.doing = value;
            doing.onComplete = ()=>{myInventory.updateProgressBar(0);};
            doing.onProgress = (d)=>{myInventory.updateProgressBar((float)d);};
        }
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

        myInventory[0] = inpBuf;
        myInventory[1] = outBuf;
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
}
