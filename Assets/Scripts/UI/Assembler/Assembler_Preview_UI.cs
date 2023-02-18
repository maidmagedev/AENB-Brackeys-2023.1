using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Assembler_Preview_UI : MonoBehaviour
{

   private Dictionary<int, string> assemblerRecipes = new();
   
    // need a list of images so we can set the image on the slots
    [SerializeField] private List<Image> slots = new();

    private List<Sprite> recipe = new();

    Sprite defaultSprite;
    private int recipeIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

        defaultSprite = Resources.Load<Sprite>("Items/item_empty");
        List<string> keys = (from key in Globals.allRecipes.Keys
            where Globals.allRecipes[key].acceptedCrafters.Contains(MachineType.ASSEMBLER)
            select key).ToList();
        for (int i = 0; i < keys.Count; i++)
        {
            assemblerRecipes.Add(i, keys[i]);
        }

        // set default recipe here
        recipe = Item_Data.toSprites(Recipe.getAllInvolvedItems(assemblerRecipes[0]));
        for (int i = 0; i < recipe.Count; i++)
        {
            if (i == recipe.Count - 1)
            {
                slots[3].sprite = recipe.Last();
                slots[4].sprite = recipe.Last();
                slots[3].GetComponentInChildren<TextMeshProUGUI>().text =
                    Recipe.getAllInvolvedItems(assemblerRecipes[0]).Last().quantity.ToString();
                slots[4].GetComponentInChildren<TextMeshProUGUI>().text =
                    Recipe.getAllInvolvedItems(assemblerRecipes[0]).Last().quantity.ToString();
            }
            else
            {
                slots[i].sprite = recipe[i];
                slots[i].GetComponentInChildren<TextMeshProUGUI>().text =
                    Recipe.getAllInvolvedItems(assemblerRecipes[0])[i].quantity.ToString();
            }
        }
        
        reduceOpacity();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CycleRecipe(int direction)
    {
        recipeIndex += direction;
        if (recipeIndex >= assemblerRecipes.Count)
        {
            recipeIndex = 0;
        }
        else if (recipeIndex < 0)
        {
            recipeIndex = assemblerRecipes.Count - 1;
        }

        recipe = Item_Data.toSprites(Recipe.getAllInvolvedItems(assemblerRecipes[recipeIndex]));

        slots.ForEach(image=>{image.sprite = defaultSprite; image.GetComponentInChildren<TextMeshProUGUI>().enabled = false;});
        
        for (int i = 0; i < recipe.Count; i++)
        {
            if (i == recipe.Count - 1)
            {
                slots[3].sprite = recipe.Last();
                slots[4].sprite = recipe.Last();
                TextMeshProUGUI text = slots[3].GetComponentInChildren<TextMeshProUGUI>();
                text.text = Recipe.getAllInvolvedItems(assemblerRecipes[recipeIndex]).Last().quantity.ToString();
                text.enabled = true;
                text = slots[4].GetComponentInChildren<TextMeshProUGUI>();
                text.enabled = true;
                text.text =
                    Recipe.getAllInvolvedItems(assemblerRecipes[recipeIndex]).Last().quantity.ToString();
            }
            else
            {
                slots[i].sprite = recipe[i];
                TextMeshProUGUI text = slots[i].GetComponentInChildren<TextMeshProUGUI>();
                text.enabled = true;
                text.text =
                    Recipe.getAllInvolvedItems(assemblerRecipes[recipeIndex])[i].quantity.ToString();
            }
        }
        reduceOpacity();
        GetComponentInParent<Assembler>().Set_Recipe(new Recipe(Globals.allRecipes[assemblerRecipes[recipeIndex]]));

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
