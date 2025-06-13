using UnityEngine;

public class CookingSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied && transferItemHandler.HasKitchenItem)
        {
            if (transferItemHandler.GetKitchenItem.TryGetComponent<ICookableItem>(out var cookableItem))
            {
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);

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

        else if (IsOccupied && !transferItemHandler.HasKitchenItem)
        {
            if (currentKitchenItem.gameObject.TryGetComponent<ICookableItem>(out var cookableItem))
            {
                cookableItem.CancelCook();
            }

            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }

        else if (IsOccupied && transferItemHandler.HasKitchenItem)
        {
            if (transferItemHandler.GetKitchenItem.TryGetComponent<IContainerItem>(out var containerItem))
            {
                if (containerItem.CanPuttableOnPlate(currentKitchenItem))
                {
                    currentKitchenItem.TryGetComponent<ICookableItem>(out var cookable);
                    cookable.CancelCook();
                    containerItem.PutOnPlate(RemoveKitchenItem());
                }
            }
        }
    }
}
