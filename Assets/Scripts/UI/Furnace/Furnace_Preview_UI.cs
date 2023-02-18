using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace_Preview_UI : MonoBehaviour
{
    
    private List<(string input, string output, string fuel)> furnaceRecipes = new()
    {
         ("Items/item_iron_ore", "Items/item_iron_bar", "Items/item_coal"),
         ("Items/item_gold_ore", "Items/item_gold_bar", "Items/item_coal")
    };

    // need a list of images so we can set the image on the slots
    [SerializeField] private List<Image> slots = new();

    private (string input, string output, string fuel) recipe;
    private int recipeIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        // set default recipe here
        recipe = furnaceRecipes[recipeIndex];
        slots[0].sprite = Resources.Load<Sprite>(recipe.output);
        slots[1].sprite = Resources.Load<Sprite>(recipe.input);
        slots[2].sprite = Resources.Load<Sprite>(recipe.fuel);
        slots[3].sprite = Resources.Load<Sprite>(recipe.output);
        reduceOpacity();
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
