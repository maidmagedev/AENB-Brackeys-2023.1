using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{

    public double progress;

    private double inProgTime = 0;

    RecipeBase baseRef;

    public Recipe(RecipeBase recipe) {
        baseRef = recipe;
    }

    public IEnumerator doCraft(Machine calling)
    {

        while (inProgTime < baseRef.time)
        {
            inProgTime += Time.deltaTime;

            progress = inProgTime / baseRef.time;

            yield return null;
        }


        inProgTime = 0;
        output(calling);

        calling.working = false;

    }


    public void output(Machine calling) {
        baseRef.outputs.ForEach(stack =>
        {
            ItemStack o = calling.outBuf.Find((st) => st.of == stack.of);

            if (o != null)
            {
                o.quantity += stack.quantity;
            }
            else {
                calling.outBuf.Add(ItemStack.copy(stack));
            }

        });

    }

    public bool accept(ItemCollection potential) {
        var against = baseRef.inputs;

        return against.TrueForAll((stack)=> {
            ItemStack inpStack = potential.Find((st) => st.of == stack.of);

            if (inpStack == null) {
                return false;
            }

            return (inpStack.quantity >= stack.quantity);

        });
    }

    public void consume(ref ItemCollection incoming) {
        var against = baseRef.inputs;

        var tempIncoming = incoming;

        against.ForEach((stack) =>
        {
            tempIncoming.Remove(stack);
        });


        incoming = tempIncoming;

    }

}
