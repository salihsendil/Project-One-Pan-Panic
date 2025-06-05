using UnityEngine;

public class CookingSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (transferItemHandler.TryPlaceKitchenItem(out var kitchenItem))
        {
            PlaceKitchenItem(kitchenItem);
            if (kitchenItem.gameObject.TryGetComponent<ICookableItem>(out var cookableItem))
            {
                cookableItem.StartCook();
            }
            else
            {
                //ui warning pop up
                Debug.Log("Bu eþya piþirilemez!");
            }
        }
        else if (HasKitchenItem())
        {
            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }
    }
}
