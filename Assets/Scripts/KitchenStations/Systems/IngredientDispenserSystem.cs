using UnityEngine;

public class IngredientDispenserSystem : KitchenStation
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied) //dolap üstü boþ
        {
            if (!transferItemHandler.HasKitchenItem) //karakter boþ
            {
                GameObject item = Instantiate(kitchenItemSO.Prefab, kitchenItemPoint.position, Quaternion.identity);
                PlaceKitchenItem(item.GetComponent<KitchenItem>());
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }

            else //karakter dolu
            {
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);
            }
        }

        else //dolap üstü dolu
        {
            if (transferItemHandler.HasKitchenItem) //karakter dolu
            {
                if (currentKitchenItem.TryGetComponent<ContainerBehaviour>(out var container))
                {
                    if (container.CanPuttableOnPlate(transferItemHandler.GetKitchenItem))
                    {
                        transferItemHandler.GiveKitchenItem(out var kitchenItem);
                        kitchenItem.TryGetComponent<IKitchenItemStateProvider>(out IKitchenItemStateProvider stateProvider);
                        container.PutOnPlate(kitchenItem, stateProvider);
                    }
                }
            }

            else //karakter boþ
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }
        }
    }
}
