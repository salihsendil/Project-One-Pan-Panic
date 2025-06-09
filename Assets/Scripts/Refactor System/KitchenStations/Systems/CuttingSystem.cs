using System;
using UnityEngine;

public class CuttingSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (!IsOccupied()) //dolap boþ
        {
            if (transferItemHandler.HasKitchenItem) //karakter dolu
            {
                Debug.Log("tezgah boþ, eþya koyulabilir.");
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);

                if (currentKitchenItem.TryGetComponent<ICuttableItem>(out ICuttableItem cuttableItem)) // kesilebilir mi? koyma ve etkileþim tuþu ayýrýlýnca burayý deðiþtir.
                {
                    if (kitchenItem.KitchenItemData.GetProcessRuleMatch(cuttableItem.CurrentState, out KitchenItemSO.ProcessRule rule))
                    {
                        cuttableItem.StartCut(rule);
                    }
                }
            }
        }

        else //dolap dolu
        {
            if (transferItemHandler.HasKitchenItem) //karakter dolu
            {
                Debug.Log("tabak durumu");
            }

            else //karakter boþ
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }
        }
    }
}
