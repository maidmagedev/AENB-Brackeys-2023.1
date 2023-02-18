using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Recipe
{

    public double progress;

    private double inProgTime = 0;

    RecipeBase baseRef;

    public Action<double> onProgress = (i)=>{};

    public Action onComplete = ()=>{};

    public Recipe(RecipeBase recipe) {
        baseRef = recipe;
    }

    public IEnumerator doCraft(Machine calling)
    {

        while (inProgTime < baseRef.time)
        {
            inProgTime += Time.deltaTime;

            progress = inProgTime / baseRef.time;
            
            onProgress(progress);

            yield return null;
        }

        //Debug.Log("craft completed");
        inProgTime = 0;
        progress = 0;
        output(calling);

        calling.working = false;
        onComplete();
    }


    public void output(Machine calling) {
        baseRef.outputs.ForEach(stack =>
        {
            ItemStack o = calling.getOutputBuffer().Find((st) => st != null && st.of == stack.of);
            if (o != null)
            {
                o.quantity += stack.quantity;
            }
            else {
                calling.getOutputBuffer().Add(ItemStack.copy(stack));
            }

        });

    }

    public bool accept(ItemCollection potential) {
        var against = baseRef.inputs;
        if (against == null)
        {
            return true;
        }
        return against.TrueForAll((stack)=> {
            ItemStack inpStack = potential.Find((st) => st != null && st.of == stack.of);

            if (inpStack == null) {
                return false;
            }

            return (inpStack.quantity >= stack.quantity);

        });
    }

    public void consume(ref ItemCollection incoming) {
        
        var against = baseRef.inputs;
        if (against == null)
        {
            return;
        }
        var tempIncoming = incoming;

        against.ForEach((stack) =>
        {
            tempIncoming.Remove(stack);
        });


        incoming = tempIncoming;

    }


    public static List<ItemStack> getAllInvolvedItems(string recipeName){
        List<ItemStack> ret = new();

        ret.AddRange(Globals.allRecipes[recipeName].inputs);
        ret.AddRange(Globals.allRecipes[recipeName].outputs);

        return ret;
    }

}
