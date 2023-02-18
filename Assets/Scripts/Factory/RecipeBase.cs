using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeBase {

    public List<MachineType> acceptedCrafters = new();

    public List<ItemStack> inputs;

    public List<ItemStack> outputs;

    public double time;



    public RecipeBase(ICollection<ItemStack> inp, ICollection<ItemStack> outp, ICollection<MachineType> acc, double time) {

        acceptedCrafters.AddRange(acc);
        inputs = new(inp);
        outputs = new(outp);
        this.time = time;
    }


}
