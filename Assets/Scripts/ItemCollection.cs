using System;
using System.Collections;
using System.Collections.Generic;
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

            return Add(new ItemStack(inserting.of, remaining), true);

        }
    }

    public ItemStack Add(ItemStack item, bool needsNewStack = false)
    {
        var current = collection.Find((st) => st.of == item.of);
        if (!needsNewStack && current != null)
        {
            return increaseStack(item, current);
        }
        else if (collection.Count + 1 <= maxSlots)
        {
            collection.Add(item);
            return null;
        }
        else
        {
            return item;
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

    public bool Remove(ItemStack item)
    {
        return collection.Remove(item);
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
