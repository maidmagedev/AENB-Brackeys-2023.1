using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    List<Vector2Int> footPrint = new();

    Recipe doing;

    public List<ItemStack> inpBuf = new();

    public List<ItemStack> outBuf = new();

    public MachineType type;

    public bool working = false;

    private void Start()
    {
        inpBuf.Add(new ItemStack(Material.ORE_IRON, 12));
        doing = new Recipe(Globals.allRecipes.GetValueOrDefault("ironOreToBar"));
    }

    private void Update()
    {

        if (!working && doing != null && inpBuf.Count > 0 && doing.accept(inpBuf))
        {
            working = true;
            doing.consume(ref inpBuf);

            StartCoroutine(doing.doCraft(this));
        }
    }
}

public enum MachineType
{
    INVENTORY,
    ASSEMBLER,
    FURNACE
}