using UnityEngine;

public class CookingSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (!IsOccupied() && transferItemHandler.HasKitchenItem)
        {
            transferItemHandler.GiveKitchenItem(out var kitchenItem);
            PlaceKitchenItem(kitchenItem);

            if (kitchenItem.gameObject.TryGetComponent<ICookableItem>(out var cookableItem))
            {
                if (kitchenItem.KitchenItemData.GetProcessRuleMatch(cookableItem.CurrentState, out KitchenItemSO.ProcessRule rule))
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

        else if (IsOccupied() && !transferItemHandler.HasKitchenItem)
        {
            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }
    }


}
