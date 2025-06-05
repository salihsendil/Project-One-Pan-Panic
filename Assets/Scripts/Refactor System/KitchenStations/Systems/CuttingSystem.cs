using System;
using UnityEngine;

public class CuttingSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (transferItemHandler.TryPlaceKitchenItem(out var kitchenItem)) // karakter dolu, dolap boþ
        {
            if (!HasKitchenItem())
            {
                Debug.Log("tezgah boþ, eþya koyulabilir.");
                PlaceKitchenItem(kitchenItem);

                if (currentKitchenItem.TryGetComponent<ICuttableItem>(out ICuttableItem cuttableItem)) // koyma ve etkileþim tuþu ayýrýlýnca burayý deðiþtir.
                {
                    if (cuttableItem.CurrentState == CuttingState.Chopped)
                    {
                        Debug.Log("Eþya zaten kesilmiþ!");
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

        else if (HasKitchenItem()) // karakter boþ, dolap dolu
        {
            Debug.Log("tezgahta eþya var eþya alýnabilir.");

            transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
        }
    }
}
