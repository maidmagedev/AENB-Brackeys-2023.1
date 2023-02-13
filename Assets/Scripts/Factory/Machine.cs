using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    List<Vector2Int> footPrint = new();

    Recipe doing;

    public List<ItemStack> inpBuf = new();

    public List<ItemStack> outBuf = new();

    MachineType type;

    public bool working = false;

    private void Update()
    {
        if (!working && doing != null && inpBuf.Count > 0 && doing.accept(inpBuf))
        {
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