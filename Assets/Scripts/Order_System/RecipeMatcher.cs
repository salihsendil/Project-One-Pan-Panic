using System.Collections.Generic;
using Zenject;

public class RecipeMatcher
{
    [Inject] private OrderConfigSO orderConfig;

    private List<RecipeIngredient> tempList = new();

    public bool TryGetRecipeMatch(List<ContainerSlot> containerSlots, out RecipeSO recipeSO)
    {
        recipeSO = null;

        foreach (var entry in orderConfig.Recipes)
        {
            if (entry.Ingredients.Count < containerSlots.Count) continue;

            tempList.Clear();
            tempList.AddRange(entry.Ingredients);

            foreach (var slot in containerSlots)
            {
                var ingredient = slot.IngredientItem;

                int index = tempList.FindIndex(x => ingredient.IngredientItemSO == x.IngredientSO && ingredient.PreparationStage == x.RequiredStage);

                if (index == -1)
                    break;

                else
                    tempList.RemoveAt(index);
            }

            if (tempList.Count == 0)
            {
                recipeSO = entry;
                return true;
            }

            else if (tempList.Count > 0)
            {
                return true;
            }
        }


        return false;
    }
}
