using System;
using UnityEngine;

public class CuttingSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (!IsOccupied()) //dolap bo�
        {
            if (transferItemHandler.HasKitchenItem) //karakter dolu
            {
                Debug.Log("tezgah bo�, e�ya koyulabilir.");
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);

                if (currentKitchenItem.TryGetComponent<ICuttableItem>(out ICuttableItem cuttableItem)) // kesilebilir mi? koyma ve etkile�im tu�u ay�r�l�nca buray� de�i�tir.
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

            else //karakter bo�
            {
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }
        }
    }
}
