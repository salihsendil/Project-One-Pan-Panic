using System;
using UnityEngine;

public class CuttingSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (transferItemHandler.TryPlaceKitchenItem(out var kitchenItem)) // karakter dolu, dolap bo�
        {
            if (!HasKitchenItem())
            {
                Debug.Log("tezgah bo�, e�ya koyulabilir.");
                PlaceKitchenItem(kitchenItem);

                if (currentKitchenItem.TryGetComponent<ICuttableItem>(out ICuttableItem cuttableItem)) // koyma ve etkile�im tu�u ay�r�l�nca buray� de�i�tir.
                {
                    if (cuttableItem.CurrentState == CuttingState.Chopped)
                    {
                        Debug.Log("E�ya zaten kesilmi�!");
                        return;
                    }
                    cuttableItem.StartCut();
                }
            }

            else
            {
                Debug.Log("tabak durumu");
            }
        }

        else if (HasKitchenItem()) // karakter bo�, dolap dolu
        {
            Debug.Log("tezgahta e�ya var e�ya al�nabilir.");

            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }
    }
}
