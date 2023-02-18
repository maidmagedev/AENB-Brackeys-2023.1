using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{

    public static Dictionary<ItemType, Item_Data> item_definitions = new()
    {
        { ItemType.STONE,       new Item_Data(ItemType.STONE, 250, Resources.Load<Sprite>("Items/item_stone"), Resources.Load<GameObject>("Items/Stone"), UseBehavior.DROP)},
        { ItemType.ORE_IRON,    new Item_Data(ItemType.ORE_IRON, 250, Resources.Load<Sprite>("Items/item_iron_ore"), Resources.Load<GameObject>("Items/Iron Ore"), UseBehavior.DROP) },
        { ItemType.COAL,        new Item_Data(ItemType.COAL, 250, Resources.Load<Sprite>("Items/item_coal"), Resources.Load<GameObject>("Items/Coal"), UseBehavior.DROP)},
        { ItemType.ORE_GOLD,    new Item_Data(ItemType.ORE_GOLD, 250, Resources.Load<Sprite>("Items/item_gold_ore"), Resources.Load<GameObject>("Items/Gold Ore"), UseBehavior.DROP) },
        { ItemType.IRON,        new Item_Data(ItemType.IRON, 250, Resources.Load<Sprite>("Items/item_iron_bar"), Resources.Load<GameObject>("Items/Iron Bar"), UseBehavior.DROP) },
        { ItemType.GOLD,        new Item_Data(ItemType.GOLD, 250, Resources.Load<Sprite>("Items/item_gold_bar"), Resources.Load<GameObject>("Items/Gold Bar"), UseBehavior.DROP) },
        { ItemType.FAMAS,       new Item_Data(ItemType.FAMAS, 1, Resources.Load<Sprite>("Items/item_famas"), Resources.Load<GameObject>("Items/Famas"), UseBehavior.SHOOT) },
        { ItemType.SHOTGUN,     new Item_Data(ItemType.SHOTGUN, 1, Resources.Load<Sprite>("Items/item_shotgun"), Resources.Load<GameObject>("Items/Shotgun"), UseBehavior.SHOOT) },
        {ItemType.MINER,        new Item_Data(ItemType.MINER, 5, Resources.Load<Sprite>("Machine/mining_unit"), Resources.Load<GameObject>("Machine/Miner"), UseBehavior.PLACE)},
        {ItemType.FURNACE,      new Item_Data(ItemType.FURNACE, 5, Resources.Load<Sprite>("Machine/furnace"), Resources.Load<GameObject>("Machine/Furnace"), UseBehavior.PLACE)},
    };
    
    public static Dictionary<string, RecipeBase> allRecipes = new()
    {
        { "ironOreToBar", new RecipeBase(new ItemStack[] { new(ItemType.ORE_IRON, 1), new (ItemType.COAL, 1) }, new ItemStack[] { new(ItemType.IRON, 1) }, new MachineType[] { MachineType.FURNACE }, 1.2) },
        {"goldOreToBar", new RecipeBase(new ItemStack[] {new (ItemType.ORE_GOLD, 1), new (ItemType.COAL, 1)}, new ItemStack[] {new(ItemType.GOLD, 1)}, new MachineType[] {MachineType.FURNACE}, 1.2)},
        {"ironOreMiner", new RecipeBase(new ItemStack[]{}, new ItemStack[]{new ItemStack(ItemType.ORE_IRON, 1)}, new MachineType[]{MachineType.MINER},10 )},
        {"Miner_Machine", new RecipeBase(new ItemStack[]{new (ItemType.IRON, 3), new(ItemType.COAL, 3), new (ItemType.GOLD, 1)}, new ItemStack[] {new(ItemType.MINER, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},5)},
        {"Furnace_Machine", new RecipeBase(new ItemStack[]{ new(ItemType.STONE, 3) }, new ItemStack[] {new(ItemType.FURNACE, 1)}, new MachineType[]{MachineType.ASSEMBLER, MachineType.INVENTORY},5)}
    };

    
}
