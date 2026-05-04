using System.Collections.Generic;
using Zenject;

public class RecipeMatchEvaluator
{
    [Inject] private OrderConfigSO orderConfig;

    public bool TryRecipeMatch(HashSet<IngredientEntry> entries, out RecipeSO recipeSO)
    {
        recipeSO = null;
        var recipeList = orderConfig.RecipeList;
        foreach (var recipe in recipeList)
        {
            bool isExactlyEqual = recipe.Ingredients.Count == entries.Count && entries.SetEquals(recipe.Ingredients);
            if (isExactlyEqual)
            {
                recipeSO = recipe;
                return true;
            }
        }
        return false;
    }
}
