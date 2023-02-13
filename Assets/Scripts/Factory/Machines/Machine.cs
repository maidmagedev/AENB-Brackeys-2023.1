using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    List<Vector2Int> footPrint;

    Recipe doing;

    public ItemCollection inpBuf;

    public ItemCollection outBuf;

    public MachineType type;

    public bool working = false;

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