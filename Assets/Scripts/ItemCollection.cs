using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollection
{
    public List<ItemStack> collection;

    public int maxSlots;


    public ItemCollection(int max, ICollection<ItemStack> items = null) {
        maxSlots = max;
        if (items != null)
        {
            collection = new(items);
        }
        else {
            collection = new();
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
        var current = collection.FindIndex((st) => st.of == item.of);
        if (!needsNewStack && current != -1 && collection[current] != null)
        {
            return (increaseStack(item, collection[current]), current);
        }
        else if (collection.Count + 1 <= maxSlots)
        {
            collection.Add(item);
            return (null, collection.Count-1);
        }
        else
        {
            return (item, -1);
        }
    }

    public bool Remove(ItemStack item)
    {
        var exists = collection.FindAll((st) => st.of == item.of);

        exists.Reverse();

        int quantTotal = exists.Sum(st => st.quantity);

        int target = item.quantity;

        if (target > quantTotal)
        {
            return false;
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
                    collection.Remove(subbing);
                    target -= subbing.quantity;
                }
            } 
            return true;
        }
    }

    #region collection impl
    public int Count => collection.Count;

    public bool IsReadOnly => false;





    public void Clear()
    {
        collection.Clear();
    }

    public bool Contains(ItemStack item)
    {
        return collection.Contains(item);
    }

    internal ItemStack Find(Predicate<ItemStack> p)
    {
        return collection.Find(p);
    }

    public IEnumerator<ItemStack> GetEnumerator()
    {
        return collection.GetEnumerator();
    }


    public ItemStack this[int i] {
        get { return collection[i]; }
        set { collection[i] = value; }
    }

    internal int FindIndex(Predicate<ItemStack> p)
    {
        return collection.FindIndex(p);
    }
    #endregion
}
