using System.Collections.Generic;

public class RecipeIngredientComparer : IEqualityComparer<RecipeSO.RecipeIngredient>
{
    public bool Equals(RecipeSO.RecipeIngredient x, RecipeSO.RecipeIngredient y)
    {
        return x.kitchenItemSO == y.kitchenItemSO && x.kitchenItemState == y.kitchenItemState;
    }

    public int GetHashCode(RecipeSO.RecipeIngredient obj)
    {
        int hash1 = obj.kitchenItemSO != null ? obj.kitchenItemSO.GetHashCode() : 0;
        int hash2 = obj.kitchenItemState.GetHashCode();
        return hash1 ^ hash2;
    }
}
