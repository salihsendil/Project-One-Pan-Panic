using System.Collections.Generic;
using Zenject;

public class RecipeMatchEvaluator
{
    [Inject] private OrderConfigSO orderConfig;

    public bool TryRecipeMatch(HashSet<IngredientEntry> entries, out RecipeSO recipeSO)
    {
        recipeSO = null;
        var recipeEntries = orderConfig.RecipeEntries;
        foreach (var entry in recipeEntries)
        {
            bool isExactlyEqual = entry.Recipe.Ingredients.Count == entries.Count && entries.SetEquals(entry.Recipe.Ingredients);
            if (isExactlyEqual)
            {
                recipeSO = entry.Recipe;
                return true;
            }
        }
        return false;
    }
}
