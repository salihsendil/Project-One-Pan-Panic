using UnityEngine;

public class EmptyCounterSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied && transferItemHandler.HasKitchenItem) //dolap boþ, karakter dolu
        {
            transferItemHandler.GiveKitchenItem(out var kitchenItem);

            PlaceKitchenItem(kitchenItem);
        }

        else if (IsOccupied) //dolap dolu, karakter boþ/dolu
        {
            if (!transferItemHandler.HasKitchenItem) //dolap dolu, karakter boþ
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }

            else //dolap dolu, karakter dolu
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

                else if (transferItemHandler.GetKitchenItem.TryGetComponent(out ContainerBehaviour containerBehaviour))
                {
                    if (containerBehaviour.CanPuttableOnPlate(currentKitchenItem))
                    {
                        currentKitchenItem.TryGetComponent<IKitchenItemStateProvider>(out IKitchenItemStateProvider stateProvider);

                        containerBehaviour.PutOnPlate(RemoveKitchenItem(), stateProvider);
                    }
                }

            }
        }
    }
}
