using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInventory: MonoBehaviour
{
    public ItemCollection inventory;

    public List<InventoryElement> inventory_grid;

    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (InventoryElement item in inventory_grid)
        {
            print(item.prefab_path);
            GameObject prefab = Resources.Load<GameObject>(item.prefab_path);
            GameObject clone = Instantiate(prefab, this.transform, false);
            clone.transform.localPosition = item.initialPosition;
            clone.transform.localScale = new Vector3(0.125f, .125f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class InventoryElement
{
    public Vector3 initialPosition;
    public string prefab_path;

}

