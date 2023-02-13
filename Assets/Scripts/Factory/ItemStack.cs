using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStack
{
    public Material of;

    public int quantity;

    public ItemStack(Material of, int quantity) {
        this.of = of;
        this.quantity = quantity;
    }


    public static ItemStack copy(ItemStack orig) {
        return new ItemStack(orig.of, orig.quantity);
    }
}
