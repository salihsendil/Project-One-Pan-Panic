using System.Collections.Generic;
using System.Linq;
using Zenject;
using UnityEngine;

public class OrderDeliverySystem : KitchenStation
{
    [Inject] private OrderManager orderManager;
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

    private bool CheckDeliveryInOrderList(List<KitchenItemSO> order, List<RecipeSO> orderList, out RecipeSO matchedRecipe)
    {
        matchedRecipe = null;
        foreach (RecipeSO recipe in orderList)
        {
            var sortedList1 = recipe.recipeList.OrderBy(x => x.name).ToList();
            var sortedList2 = order.OrderBy(x => x.name).ToList();

            if (sortedList1.SequenceEqual(sortedList2))
            {
                matchedRecipe = recipe;
                return true;
            }
        }
        return false;
    }
}
