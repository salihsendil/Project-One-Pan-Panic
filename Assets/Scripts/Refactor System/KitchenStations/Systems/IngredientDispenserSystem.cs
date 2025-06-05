using UnityEngine;

public class IngredientDispenserSystem : KitchenStation
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact()
    {
        base.Interact();

        if (transferItemHandler.TryPlaceKitchenItem(out var kitchenItem))
        {
            if (!HasKitchenItem())
            {
                PlaceKitchenItem(kitchenItem);
            }
            else // HasKitchenItem()
            {
                Debug.Log("tabak durumu");
            }
        }

        else if (HasKitchenItem())
        {
            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }

        else 
        {
            GameObject item = Instantiate(kitchenItemSO.Prefab, kitchenItemPoint.position, Quaternion.identity);
            PlaceKitchenItem(item.GetComponent<KitchenItem>());
            transferItemHandler.ReceiveKitchenItem(currentKitchenItem);
        }
    }
}
