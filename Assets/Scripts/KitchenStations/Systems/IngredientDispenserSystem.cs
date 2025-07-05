using UnityEngine;
using Zenject;

public class IngredientDispenserSystem : KitchenStation
{
    [Inject] private KitchenItemPoolManager poolManager;
    [SerializeField] private KitchenItemType kitchenItemType;

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied) //dolap üstü boþ
        {
            if (!transferItemHandler.HasKitchenItem) //karakter boþ
            {
                KitchenItem item = poolManager.GetKitchenItemFromPool(kitchenItemType);

                PlaceKitchenItem(item);

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
                if (currentKitchenItem.TryGetComponent(out ContainerBehaviour container))
                {
                    if (container.CanPuttableOnPlate(transferItemHandler.GetKitchenItem))
                    {
                        transferItemHandler.GiveKitchenItem(out var kitchenItem);

                        kitchenItem.TryGetComponent(out IKitchenItemStateProvider stateProvider);

                        container.PutOnPlate(kitchenItem, stateProvider);
                    }
                }

                else if (transferItemHandler.GetKitchenItem.TryGetComponent(out ContainerBehaviour containerBehaviour))
                {
                    if (containerBehaviour.CanPuttableOnPlate(currentKitchenItem))
                    {
                        currentKitchenItem.TryGetComponent(out IKitchenItemStateProvider stateProvider);

                        containerBehaviour.PutOnPlate(RemoveKitchenItem(), stateProvider);
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
