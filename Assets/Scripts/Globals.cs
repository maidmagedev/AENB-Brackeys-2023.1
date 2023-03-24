using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{

    public static Dictionary<ItemType, (ItemType type, int max, Sprite sprite, GameObject pickUp)> item_definitions = new()
    {
        { ItemType.STONE,       (ItemType.STONE, 250, Resources.Load<Sprite>("Items/item_stone"), Resources.Load<GameObject>("Items/Stone"))},
        { ItemType.ORE_IRON,    (ItemType.ORE_IRON, 250, Resources.Load<Sprite>("Items/item_iron_ore"), Resources.Load<GameObject>("Items/Iron Ore")) },
        { ItemType.COAL,        (ItemType.COAL, 250, Resources.Load<Sprite>("Items/item_coal"), Resources.Load<GameObject>("Items/Coal"))},
        { ItemType.ORE_GOLD,    (ItemType.ORE_GOLD, 250, Resources.Load<Sprite>("Items/item_gold_ore"), Resources.Load<GameObject>("Items/Gold Ore")) },
        { ItemType.IRON,        (ItemType.IRON, 250, Resources.Load<Sprite>("Items/item_iron_bar"), Resources.Load<GameObject>("Items/Iron Bar")) },
        { ItemType.GOLD,        (ItemType.GOLD, 250, Resources.Load<Sprite>("Items/item_gold_bar"), Resources.Load<GameObject>("Items/Gold Bar")) },
        { ItemType.FAMAS,       (ItemType.FAMAS, 1, Resources.Load<Sprite>("Items/item_famas"), Resources.Load<GameObject>("Items/Famas")) },
        { ItemType.SHOTGUN,     (ItemType.SHOTGUN, 1, Resources.Load<Sprite>("Items/item_shotgun"), Resources.Load<GameObject>("Items/Shotgun")) },
        { ItemType.MINER,       (ItemType.MINER, 5, Resources.Load<Sprite>("Machine/mining_unit"), Resources.Load<GameObject>("Machine/Miner"))},
        { ItemType.FURNACE,     (ItemType.FURNACE, 5, Resources.Load<Sprite>("Machine/furnace"), Resources.Load<GameObject>("Machine/Furnace"))},
        { ItemType.GRABBER,     (ItemType.GRABBER, 20, Resources.Load<Sprite>("Machine/grabber_combined"), Resources.Load<GameObject>("Machine/Grabber"))},
        { ItemType.BELT,        (ItemType.BELT, 20, Resources.Load<Sprite>("Machine/belt-1"), Resources.Load<GameObject>("Machine/Belt"))},
        { ItemType.SPITTER,     (ItemType.SPITTER, 5, Resources.Load<Sprite>("Machine/spitter"), Resources.Load<GameObject>("Machine/Spitter"))},
        {ItemType.ASSEMBLER,    (ItemType.ASSEMBLER, 5, Resources.Load<Sprite>("Machine/assembler-1"), Resources.Load<GameObject>("Machine/Assembler"))},
        { ItemType.PISTOL,      (ItemType.PISTOL, 1, Resources.Load<Sprite>("Items/item_pistol"), Resources.Load<GameObject>("Items/Pistol"))},
        { ItemType.QUEST,       (ItemType.QUEST, 1, Resources.Load<Sprite>("Items/item_quest"), Resources.Load<GameObject>("Item/Quest"))},
        { ItemType.BOLTACTION,  (ItemType.BOLTACTION, 1, Resources.Load<Sprite>("Items/item_boltaction"), Resources.Load<GameObject>("Items/BoltAction")) }
    };
    
    
    public static Dictionary<string, RecipeBase> allRecipes = new()
    {
        { "ironOreToBar", new RecipeBase(new ItemStack[] { new(ItemType.ORE_IRON, 1), new (ItemType.COAL, 1) }, new ItemStack[] { new(ItemType.IRON, 1) }, new MachineType[] { MachineType.FURNACE }, 1.2) },
        {"goldOreToBar", new RecipeBase(new ItemStack[] {new (ItemType.ORE_GOLD, 1), new (ItemType.COAL, 1)}, new ItemStack[] {new(ItemType.GOLD, 1)}, new MachineType[] {MachineType.FURNACE}, 1.2)},
        {"ironOreMiner", new RecipeBase(new ItemStack[]{}, new ItemStack[]{new ItemStack(ItemType.ORE_IRON, 1)}, new MachineType[]{MachineType.MINER},10 )},
        {"goldOreMiner", new RecipeBase(new ItemStack[]{}, new ItemStack[]{new ItemStack(ItemType.ORE_GOLD, 1)}, new MachineType[]{MachineType.MINER},10 )},
        {"CoalMiner", new RecipeBase(new ItemStack[]{}, new ItemStack[]{new ItemStack(ItemType.COAL, 1)}, new MachineType[]{MachineType.MINER},10 )},
        {"Miner_Machine", new RecipeBase(new ItemStack[]{new (ItemType.IRON, 3), new(ItemType.COAL, 3), new (ItemType.GOLD, 1)}, new ItemStack[] {new(ItemType.MINER, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},10)},
        {"Furnace_Machine", new RecipeBase(new ItemStack[]{ new(ItemType.STONE, 3) }, new ItemStack[] {new(ItemType.FURNACE, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},5)},
        {"Famas", new RecipeBase(new ItemStack[]{new(ItemType.IRON, 3)}, new ItemStack[] {new (ItemType.FAMAS, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},5 ) },
        {"Shotgun", new RecipeBase(new ItemStack[]{new(ItemType.IRON, 3), new(ItemType.GOLD, 1)}, new ItemStack[] {new (ItemType.SHOTGUN, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},10 ) },
        {"Grabber", new RecipeBase(new ItemStack[]{new(ItemType.IRON, 1)}, new ItemStack[] {new (ItemType.GRABBER, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},3 ) },
        {"Spitter", new RecipeBase(new ItemStack[]{new(ItemType.IRON, 2)}, new ItemStack[] {new (ItemType.SPITTER, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},5 ) },
        {"Belt", new RecipeBase(new ItemStack[]{new(ItemType.GOLD, 1)}, new ItemStack[] {new (ItemType.BELT, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},5 ) },
        {"Rocket", new RecipeBase(new ItemStack[] {new (ItemType.IRON, 50), new (ItemType.GOLD, 50), new (ItemType.COAL, 50), new(ItemType.QUEST, 1)}, new ItemStack[] {new (ItemType.SHOTGUN, 1)}, new MachineType[]{MachineType.ROCKET, MachineType.INVENTORY}, 1)}
    };
    
    
}
