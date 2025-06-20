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
            if (!transferItemHandler.HasKitchenItem)
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }

            else
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
        }
    }
}
