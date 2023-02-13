using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ItemCollection col = new(5);


        col.Add(new ItemStack(ItemType.STONE, 250));

        Debug.Log(col.Remove(new ItemStack(ItemType.ORE_IRON, 1)));

        Debug.Log(col.Remove(new ItemStack(ItemType.STONE, 1)));

        Debug.Log(col.collection[0]);

        Debug.Log(col.Remove(new ItemStack(ItemType.STONE, 250)));

        Debug.Log(col.collection[0]);
    }
}
