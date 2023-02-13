using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public ItemType of;

    public int quantity;

    public int max;

    public ItemStack(ItemType of, int quantity) {
        this.of = of;
        this.quantity = quantity;
        max = Item.stackSize[of];
    }


    public static ItemStack copy(ItemStack orig) {
        return new ItemStack(orig.of, orig.quantity);
    }

    public override string ToString()
    {
        return of + ", " + quantity + "/" + max;
    }
}
