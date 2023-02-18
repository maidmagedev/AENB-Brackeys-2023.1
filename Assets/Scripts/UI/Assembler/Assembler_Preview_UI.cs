using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Assembler_Preview_UI : MonoBehaviour
{
   /* private List<(string input1, string input2, string input3, string output)> assemblerRecipes = new()
    {
         ("Items/item_iron_ore", "Items/item_iron_bar", "Items/item_coal"),
         ("Items/item_gold_ore", "Items/item_gold_bar", "Items/item_coal")
    };*/

   private Dictionary<int, string> assemblerRecipes = new();
   
    // need a list of images so we can set the image on the slots
    [SerializeField] private List<Image> slots = new();

    private List<Sprite> recipe = new();
    private int recipeIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        List<string> keys = (from key in Globals.allRecipes.Keys
            where Globals.allRecipes[key].acceptedCrafters.Contains(MachineType.ASSEMBLER)
            select key).ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            assemblerRecipes.Add(i, keys[i]);
        }
        
        // set default recipe here
        for (int i = 0; i < recipe.Count; i++)
        {
            if (i == recipe.Count - 1)
            {
                slots[3].sprite = recipe.Last();
                slots[4].sprite = recipe.Last();
            }
            else
            {
                slots[i].sprite = recipe[i];
            }
        }
        
        // 5 total slots
        slots[0].sprite = Resources.Load<Sprite>(recipe.output);
        slots[1].sprite = Resources.Load<Sprite>(recipe.input);
        slots[2].sprite = Resources.Load<Sprite>(recipe.fuel);
        slots[3].sprite = Resources.Load<Sprite>(recipe.output);
        reduceOpacity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CycleRecipe(int direction)
    {
        recipeIndex += direction;
        if (recipeIndex >= furnaceRecipes.Count)
        {
            recipeIndex = 0;
        }
        else if (recipeIndex < 0)
        {
            recipeIndex = furnaceRecipes.Count - 1;
        }
        recipe = furnaceRecipes[recipeIndex];
        slots[0].sprite = Resources.Load<Sprite>(recipe.output);
        slots[1].sprite = Resources.Load<Sprite>(recipe.input);
        slots[2].sprite = Resources.Load<Sprite>(recipe.fuel);
        slots[3].sprite = Resources.Load<Sprite>(recipe.output);
        reduceOpacity();
        switch (recipeIndex)
        {
            case 0:
                GetComponentInParent<Furnace>().Set_Recipe(new Recipe(Globals.allRecipes["ironOreToBar"]));
                break;
            case 1:
                GetComponentInParent<Furnace>().Set_Recipe(new Recipe(Globals.allRecipes["goldOreToBar"]));
                break;
        }
    }

    private void reduceOpacity()
    {
        foreach (Image image in slots)
        {
            Color temp = image.color;
            temp.a = 0.2f;
            image.color = temp;
        }
    }
}
