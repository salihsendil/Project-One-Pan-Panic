using System;
using UnityEngine;

public class CuttingSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied) //dolap bo�
        {
            if (transferItemHandler.HasKitchenItem) //karakter dolu
            {
                Debug.Log("tezgah bo�, e�ya koyulabilir.");
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
                        container.PutOnPlate(kitchenItem);
                    }
                }
            }

            else //karakter bo�
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }
        }
    }

    public override void InteractAlternate()
    {

        if (currentKitchenItem == null) { return; }

        Debug.Log("kitchen Station override girdim");
        if (currentKitchenItem.TryGetComponent<ICuttableItem>(out ICuttableItem cuttableItem)) // kesilebilir mi? koyma ve etkile�im tu�u ay�r�l�nca buray� de�i�tir.
        {
            Debug.Log("cuttable item buldum");
            if (currentKitchenItem.KitchenItemData.GetProcessRuleMatch(cuttableItem.CurrentState, out KitchenItemSO.ProcessRule rule))
            {
                cuttableItem.StartCut(rule);
            }
        }
    }
}
