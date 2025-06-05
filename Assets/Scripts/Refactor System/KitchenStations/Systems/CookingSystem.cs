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
                Debug.Log("Bu e�ya pi�irilemez!");
            }
        }
        else if (HasKitchenItem())
        {
            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }
    }
}
