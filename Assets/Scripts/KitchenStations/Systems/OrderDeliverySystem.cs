using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System.Linq;

public class OrderDeliverySystem : KitchenStation
{
    [Inject] private OrderManager orderManager;
    private readonly RecipeIngredientComparer comparer = new RecipeIngredientComparer();

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (transferItemHandler.HasKitchenItem && transferItemHandler.GetKitchenItem.TryGetComponent<IContainerItem>(out var containerItem))
        {
            if (CheckDeliveryInOrderList(containerItem.KitchemItemsDatas, orderManager.CurrentOrderList, out RecipeSO recipe))
            {
                Debug.Log("eþleþme bulundu.");

                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);
                orderManager.CurrentOrderList.Remove(recipe);
                Destroy(kitchenItem.gameObject, 2f);
                Debug.Log("sipariþ teslim edildi.");
            }

            else
            {
                Debug.Log("eþleþme yokk");
            }

        }
    }

    private bool CheckDeliveryInOrderList(HashSet<RecipeSO.RecipeIngredient> order, List<RecipeSO> orderList, out RecipeSO matchedRecipe)
    {
        matchedRecipe = null;

        foreach (var recipe in orderList)
        {
            var recipeSet = new HashSet<RecipeSO.RecipeIngredient>(recipe.recipeIngredients, comparer);

            Debug.Log("Tarif: " + string.Join(", ", recipeSet.Select(x => x.kitchenItemSO.name + "-" + x.kitchenItemState)));
            Debug.Log("Oyuncu: " + string.Join(", ", order.Select(x => x.kitchenItemSO.name + "-" + x.kitchenItemState)));

            if (recipeSet.SetEquals(order))
            {
                matchedRecipe = recipe;
                Debug.Log("tamamen eþleþti");
                return true;
            };
        }
        return false;
    }
}
