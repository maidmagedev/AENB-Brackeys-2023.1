using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Dictionary<string, RecipeBase> allRecipes = new()
    {
        { "ironOreToBar", new RecipeBase(new ItemStack[] { new(ItemType.ORE_IRON, 1), new (ItemType.COAL, 1) }, new ItemStack[] { new(ItemType.IRON, 1) }, new MachineType[] { MachineType.FURNACE }, 1.2) },
        {"goldOreToBar", new RecipeBase(new ItemStack[] {new (ItemType.ORE_GOLD, 1), new (ItemType.COAL, 1)}, new ItemStack[] {new(ItemType.GOLD, 1)}, new MachineType[] {MachineType.FURNACE}, 1.2)},
        {"ironOreMiner", new RecipeBase(new ItemStack[]{}, new ItemStack[]{new ItemStack(ItemType.ORE_IRON, 1)}, new MachineType[]{MachineType.MINER},10 )},
        {"Miner_Machine", new RecipeBase(new ItemStack[]{new (ItemType.IRON, 3), new(ItemType.COAL, 3), new (ItemType.GOLD, 1)}, new ItemStack[] {new(ItemType.MINER, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},5)}
    };
}
