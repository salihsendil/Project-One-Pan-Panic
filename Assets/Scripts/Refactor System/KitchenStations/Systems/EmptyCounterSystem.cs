using UnityEngine;

public class EmptyCounterSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied && transferItemHandler.HasKitchenItem) //dolap boþ, karakter dolu
        {
            Debug.Log("tezgah boþ, eþya koyulabilir.");
            transferItemHandler.GiveKitchenItem(out var kitchenItem);
            PlaceKitchenItem(kitchenItem);
        }

        else if (IsOccupied) //dolap dolu, karakter boþ/dolu
        {
            if (!transferItemHandler.HasKitchenItem)
            {
                Debug.Log("tezgahta eþya var eþya alýnabilir.");
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }

            else
            {
                Debug.Log("tabak durumu");
            }
        }
    }
}
