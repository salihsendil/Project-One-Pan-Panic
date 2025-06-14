using System;
using UnityEngine;

public class CuttingSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied) //dolap boþ
        {
            if (transferItemHandler.HasKitchenItem) //karakter dolu
            {
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);
            }
        }

        else //dolap dolu
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

            else if (!transferItemHandler.HasBusyForProcess) //karakter 
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }
        }
    }

    public override void InteractAlternate()
    {
        if (currentKitchenItem == null) { return; }

        if (currentKitchenItem.TryGetComponent<ICuttableItem>(out ICuttableItem cuttableItem))
        {
            currentKitchenItem.TryGetComponent<IKitchenItemStateProvider>(out IKitchenItemStateProvider stateProvider);

            if (currentKitchenItem.KitchenItemData.GetProcessRuleMatch(stateProvider.CurrentState, out KitchenItemSO.ProcessRule rule))
            {
                cuttableItem.StartCut(rule, transferItemHandler);
            }
        }
    }
}
