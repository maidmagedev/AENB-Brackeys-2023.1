using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public MaterialType of;

    public int quantity;

    public int max;

    public ItemStack(MaterialType of, int quantity) {
        this.of = of;
        this.quantity = quantity;
        max = Material.stackSize[of];
    }


    public static ItemStack copy(ItemStack orig) {
        return new ItemStack(orig.of, orig.quantity);
    }

    public override string ToString()
    {
        return of + ", " + quantity + "/" + max;
    }
}
