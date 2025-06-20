using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System.Linq;

public class OrderDeliverySystem : KitchenStation
{
    [Inject] private OrderManager orderManager;
    [Inject] private GameStatsManager gameStatsManager;
    private readonly RecipeIngredientComparer comparer = new RecipeIngredientComparer();

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (transferItemHandler.HasKitchenItem && transferItemHandler.GetKitchenItem.TryGetComponent<IContainerItem>(out var containerItem))
        {
            if (CheckDeliveryInOrderList(containerItem.KitchemItemsDatas, gameStatsManager.CurrentOrderInstances, out OrderInstance recipe))
            {
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);
                gameStatsManager.DeleteOrder(recipe);
                Destroy(kitchenItem.gameObject, 2f);
            }

            else
            {
                Debug.Log("eþleþme yokk");
            }

        }
    }

    private bool CheckDeliveryInOrderList(HashSet<RecipeSO.RecipeIngredient> order, List<OrderInstance> orderList, out OrderInstance matchedRecipe)
    {
        matchedRecipe = null;

        foreach (var recipe in orderList)
        {
            if (recipe.RecipeSO.recipeIngredients.Count != order.Count) { continue; }

            var recipeSet = new HashSet<RecipeSO.RecipeIngredient>(recipe.RecipeSO.recipeIngredients, comparer);

            Debug.Log("Tarif: " + string.Join(", ", recipeSet.Select(x => x.kitchenItemSO.name + "-" + x.kitchenItemState)));
            Debug.Log("Oyuncu: " + string.Join(", ", order.Select(x => x.kitchenItemSO.name + "-" + x.kitchenItemState)));

            if (recipeSet.SetEquals(order))
            {
                matchedRecipe = recipe;
                return true;
            };
        }
        return false;
    }
}
