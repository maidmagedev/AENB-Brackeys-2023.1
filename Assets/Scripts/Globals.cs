using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Dictionary<string, RecipeBase> allRecipes = new()
    {
        { "ironOreToBar", new RecipeBase(new ItemStack[] { new(Material.ORE_IRON, 1) }, new ItemStack[] { new(Material.IRON, 1) }, new MachineType[] { MachineType.FURNACE }, 1.2) }
    };
}