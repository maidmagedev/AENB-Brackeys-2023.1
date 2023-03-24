using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket_Machine : Machine
{
    private Rocket_Inventory myInventory;
    public new Recipe doing{
        get {return base.doing;} 
        set{
            base.doing = value;
            // replace this with call to cutscene or something
            doing.onComplete = ()=>{SceneManager.LoadScene(3);};
        }
    }
    
    public Rocket_Machine() {
        inpBuf = new(4);
        outBuf = new(1);
        type = MachineType.ROCKET;
        footPrint = new(3, 3);
        child_start = Rocket_Start;
    }
    private void Rocket_Start()
    {
        //doing = new Recipe(Globals.allRecipes["Rocket"]);
        myInventory = GetComponentInChildren<Rocket_Inventory>();
        //myInventory[0] = inpBuf;
        //myInventory[1] = outBuf;
    }
}
