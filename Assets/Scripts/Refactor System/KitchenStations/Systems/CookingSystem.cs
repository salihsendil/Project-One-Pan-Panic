using UnityEngine;

public class CookingSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied && transferItemHandler.HasKitchenItem)
        {
            transferItemHandler.GiveKitchenItem(out var kitchenItem);
            PlaceKitchenItem(kitchenItem);
        }

        else if (IsOccupied && !transferItemHandler.HasKitchenItem)
        {
            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }
    }

    public override void InteractAlternate()
    {
        if (currentKitchenItem.gameObject.TryGetComponent<ICookableItem>(out var cookableItem))
        {
            if (currentKitchenItem.KitchenItemData.GetProcessRuleMatch(cookableItem.CurrentState, out KitchenItemSO.ProcessRule rule))
            {
                cookableItem.StartCook(rule);
            }
        }

        else
        {
            //ui warning pop up
            Debug.Log("Bu eþya piþirilemez!");
        }
    }


}
