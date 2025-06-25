using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class OrderDeliverySystem : KitchenStation
{
    [Inject] private OrderManager orderManager;
    private readonly RecipeIngredientComparer comparer = new RecipeIngredientComparer();

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (transferItemHandler.HasKitchenItem && transferItemHandler.GetKitchenItem.TryGetComponent<IContainerItem>(out var containerItem))
        {
            if (CheckDeliveryInOrderList(containerItem.KitchemItemsDatas, orderManager.OrderInstances, out OrderInstance orderInstance))
            {
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);
                orderManager.OrderDelivered(orderInstance);
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
            if (recipe.HasOrderExpired) { continue; }

            if (recipe.RecipeSO.recipeIngredients.Count != order.Count) { continue; }

            var recipeSet = new HashSet<RecipeSO.RecipeIngredient>(recipe.RecipeSO.recipeIngredients, comparer);

            //Debug.Log("Tarif: " + string.Join(", ", recipeSet.Select(x => x.kitchenItemSO.name + "-" + x.kitchenItemState)));
            //Debug.Log("Oyuncu: " + string.Join(", ", order.Select(x => x.kitchenItemSO.name + "-" + x.kitchenItemState)));

            if (recipeSet.SetEquals(order))
            {
                matchedRecipe = recipe;
                return true;
            };
        }
        return false;
    }
}
