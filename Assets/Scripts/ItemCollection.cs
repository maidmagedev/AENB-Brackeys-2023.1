using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCollection
{
    private ItemStack [] collection;

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
            var ret = (increaseStack(item, collection[current]), current);

            NotifyListeners(new ItemColChangeEvent(ChangeType.ADD, new List<int>(){current}));
            return ret;
        }
        else if (nonNullItems + 1 <= maxSlots)
        {
            nonNullItems++;

            var insertInd = Array.FindIndex(collection, st=>st == null);
            collection[insertInd] = item;

            NotifyListeners(new ItemColChangeEvent(ChangeType.ADD, new List<int>(){insertInd}));
            return (null, insertInd);
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
        else {
            bool type = false;
            if (target > quantTotal){
                target = quantTotal;
                type = true;
            }

            List<int> affected = new();
            while (target > 0) {
                var subbing = exists[0];
                var affectedInd = new List<ItemStack>(collection).FindIndex(st=>st != null && st == subbing);
                affected.Add(affectedInd);
                if (subbing.quantity > target)
                {
                    subbing.quantity -= target;
                    target = 0;
                }
                else {
                    nonNullItems --;
                    collection[affectedInd] = null;
                    target -= subbing.quantity;
                }
            }

            NotifyListeners(new ItemColChangeEvent(ChangeType.REMOVE, affected));

            if (type){
                return (false, new ItemStack(item.of, quantTotal));
            }
            else{
                return (true, item);
            }
        }
    }

    public ItemStack Extract(int amount){
        ItemStack retStack = new List<ItemStack>(collection).Find(st=> st != null);

        if (retStack == null){
            return null;
        }

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
        set {
            ItemStack itemPrev = collection[i];
            collection[i] = value;
            if (value == null){
                nonNullItems--;
                NotifyListeners(new ItemColChangeEvent(ChangeType.REMOVE, new List<int>(){i}));
            }
            else if (itemPrev == null){
                nonNullItems++;
                NotifyListeners(new ItemColChangeEvent(ChangeType.ADD, new List<int>(){i}));
            }
            else{
                Debug.Log("potential fail");
                NotifyListeners(new ItemColChangeEvent(ChangeType.SWAP, new List<int>(){i}));
            }
        }
    }

    internal int FindIndex(Predicate<ItemStack> p)
    {
        return new List<ItemStack>(collection).FindIndex(p);
    }


    public List<ItemStack> Reverse(){
        List<ItemStack> ret = new(collection);

        ret.Reverse();

        return ret;
    }
    #endregion

    private List<Action<ItemColChangeEvent>> listeners = new();

    public void AddListener(Action<ItemColChangeEvent> listener){
        listeners.Add(listener);
    }

    public void RemoveListener(Action<ItemColChangeEvent> listener){
        listeners.Remove(listener);
    }

    private void NotifyListeners(ItemColChangeEvent evt){
        listeners.ForEach(listen=>listen.Invoke(evt));
    }
}

public class ItemColChangeEvent {

    private ChangeType _changeType;

    public ChangeType changeType {get {return _changeType;} }
    private List<int> _affectedindices;

    public List<int> affectedindices {get {return _affectedindices;} }

    public ItemColChangeEvent(ChangeType type, List<int> affects){
        _changeType = type;
        _affectedindices = affects;
    }
    
}

public enum ChangeType{
    SWAP,
    ADD,
    REMOVE,
    OTHER
}