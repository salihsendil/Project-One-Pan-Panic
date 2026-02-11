using System.Collections.Generic;
using Zenject;

public class RecipeMatchEvaluator
{
    [Inject] private OrderSystem orderSystem;

    //List For Copy Plate Ingredient
    private List<IngredientEntry> plateTemp = new();

    public bool TryRecipeMatch(List<IngredientEntry> entries, out RecipeSO recipeSO)
    {
        recipeSO = null;
        var recipes = orderSystem.OrderConfig.RecipeList;

        foreach (var recipe in recipes)
        {
            var ingredients = recipe.Ingredients;

            if (ingredients.Count != entries.Count) { continue; }

            plateTemp.AddRange(entries);

            bool isAllIngredientsMatch = true;

            for (int i = 0; i < ingredients.Count; i++)
            {
                bool isMatch = false;
                for (int j = 0; j < plateTemp.Count; j++)
                {
                    if (ingredients[i].Equals(plateTemp[j]))
                    {
                        isMatch = true;
                        plateTemp.RemoveAt(j);
                        break;
                    }
                }

                if (!isMatch) { isAllIngredientsMatch = false; break; }
            }

            plateTemp.Clear();
            if (isAllIngredientsMatch) { recipeSO = recipe; return true; }
        }

        return false;
    }
}
