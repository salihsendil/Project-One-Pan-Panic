using UnityEngine;
using Zenject;

public class IngredientDispenserSystem : KitchenStation
{
    [Inject] private KitchenItemPoolManager poolManager;
    [SerializeField] private KitchenItemType kitchenItemType;

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied) //dolap �st� bo�
        {
            if (!transferItemHandler.HasKitchenItem) //karakter bo�
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

        else //dolap �st� dolu
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

            else //karakter bo�
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }
        }
    }
}
