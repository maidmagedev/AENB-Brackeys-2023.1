using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollection
{
    public ItemStack [] collection;

    public int maxSlots;

    int nonNullItems = 0;


    public ItemCollection(int max, ICollection<ItemStack> items = null) {
        maxSlots = max;
        collection = new ItemStack[max];

        if (items != null)
        {
            items.CopyTo(collection, 0);
        }

    }

    private ItemStack increaseStack(ItemStack inserting, ItemStack current) {
        if (current.quantity + inserting.quantity <= current.max)
        {
            current.quantity += inserting.quantity;

            return null;
        }
        else {
            var remaining = inserting.quantity - (current.max - current.quantity);

            current.quantity = current.max;

            return Add(new ItemStack(inserting.of, remaining), true).more;

        }
    }

    public (ItemStack more, int insertIndex) Add(ItemStack item, bool needsNewStack = false)
    {
        var current = new List<ItemStack>(collection).FindIndex((st) => st != null && st.of == item.of);
        if (!needsNewStack && current != -1 && collection[current] != null)
        {
            return (increaseStack(item, collection[current]), current);
        }
        else if (nonNullItems + 1 <= maxSlots)
        {
            collection[nonNullItems] = item;
            nonNullItems++;
            return (null, nonNullItems-1);
        }
        else
        {
            return (item, -1);
        }
    }

    public (bool requestCompelete, ItemStack partial) Remove(ItemStack item, bool doPartial = false)
    {
        var exists = new List<ItemStack>(collection).FindAll((st) => st!=null && st.of == item.of);

        exists.Reverse();

        int quantTotal = exists.Sum(st => st.quantity);

        if (item == null)
        {
            return (true, null);
        }
        int target = item.quantity;

        if (target > quantTotal && !doPartial)
        {
            return (false, null);
        }
        else if (target > quantTotal){
            target = quantTotal;
            while (target > 0) {
                var subbing = exists[0];
                if (subbing.quantity > target)
                {
                    subbing.quantity -= target;
                    target = 0;
                }
                else {
                    collection[new List<ItemStack>(collection).FindIndex(st=>st != null && st == subbing)] = null;

                    nonNullItems --;
                    target -= subbing.quantity;
                }
            }

            return (false, new ItemStack(item.of, quantTotal));
        }
        else {
            while (target > 0) {
                var subbing = exists[0];
                if (subbing.quantity > target)
                {
                    subbing.quantity -= target;
                    target = 0;
                }
                else {
                    collection[new List<ItemStack>(collection).FindIndex(st=>st != null && st == subbing)] = null;

                    nonNullItems --;
                    target -= subbing.quantity;
                }
            } 
            return (true, item);
        }
    }

    public ItemStack Extract(int amount){
        ItemStack retStack = new List<ItemStack>(collection).Find(st=> st != null);

        ItemStack ret = new ItemStack(retStack.of, amount);

        return Remove(ret, true).partial;
    }


    #region collection impl
    public int Size => maxSlots;

    public int Count => nonNullItems;

    public bool IsReadOnly => false;





    public void Clear()
    {
        collection = new ItemStack[maxSlots];
    }

    public bool Contains(ItemStack item)
    {
        return collection.Contains(item);
    }

    internal ItemStack Find(Predicate<ItemStack> p)
    {
        return new List<ItemStack>(collection).Find(p);
    }

    public IEnumerator<ItemStack> GetEnumerator()
    {
        return (IEnumerator<ItemStack>)collection.GetEnumerator();
    }


    public ItemStack this[int i] {
        get { return collection[i]; }
        set { collection[i] = value; }
    }

    internal int FindIndex(Predicate<ItemStack> p)
    {
        return new List<ItemStack>(collection).FindIndex(p);
    }
    #endregion
}
