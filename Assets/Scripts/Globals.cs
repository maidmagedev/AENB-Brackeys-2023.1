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
        { ItemType.MINER,       new Item_Data(ItemType.MINER, 5, Resources.Load<Sprite>("Machine/mining_unit"), Resources.Load<GameObject>("Machine/Miner"), UseBehavior.PLACE)},
        { ItemType.FURNACE,     new Item_Data(ItemType.FURNACE, 5, Resources.Load<Sprite>("Machine/furnace"), Resources.Load<GameObject>("Machine/Furnace"), UseBehavior.PLACE)},
        { ItemType.GRABBER,     new Item_Data(ItemType.GRABBER, 20, Resources.Load<Sprite>("Machine/grabber_combined"), Resources.Load<GameObject>("Machine/Grabber"), UseBehavior.PLACE)},
        { ItemType.BELT,        new Item_Data(ItemType.BELT, 20, Resources.Load<Sprite>("Machine/belt-1"), Resources.Load<GameObject>("Machine/Belt"), UseBehavior.PLACE)},
        { ItemType.SPITTER,     new Item_Data(ItemType.SPITTER, 5, Resources.Load<Sprite>("Machine/spitter"), Resources.Load<GameObject>("Machine/Spitter"), UseBehavior.PLACE)},
        {ItemType.ASSEMBLER,    new Item_Data(ItemType.ASSEMBLER, 5, Resources.Load<Sprite>("Machine/assembler-1"), Resources.Load<GameObject>("Machine/Assembler"), UseBehavior.PLACE)},
        { ItemType.PISTOL,      new Item_Data(ItemType.PISTOL, 1, Resources.Load<Sprite>("Items/item_pistol"), Resources.Load<GameObject>("Items/Pistol"), UseBehavior.SHOOT)},
        { ItemType.QUEST,       new Item_Data(ItemType.QUEST, 1, Resources.Load<Sprite>("Items/item_quest"), Resources.Load<GameObject>("Item/Quest"), UseBehavior.SHOOT)},
        { ItemType.BOLTACTION,  new Item_Data(ItemType.BOLTACTION, 1, Resources.Load<Sprite>("Items/item_boltaction"), Resources.Load<GameObject>("Items/BoltAction"), UseBehavior.SHOOT) },
        { ItemType.ASSAULTRIFLE, new Item_Data(ItemType.ASSAULTRIFLE, 1, Resources.Load<Sprite>("Items/item_assault_rifle"), Resources.Load<GameObject>("Item/AssaultRifle"), UseBehavior.SHOOT)}
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
