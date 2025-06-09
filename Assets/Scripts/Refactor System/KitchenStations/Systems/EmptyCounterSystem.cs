using UnityEngine;

public class EmptyCounterSystem : KitchenStation
{
    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied && transferItemHandler.HasKitchenItem) //dolap bo�, karakter dolu
        {
            Debug.Log("tezgah bo�, e�ya koyulabilir.");
            transferItemHandler.GiveKitchenItem(out var kitchenItem);
            PlaceKitchenItem(kitchenItem);
        }

        else if (IsOccupied) //dolap dolu, karakter bo�/dolu
        {
            if (!transferItemHandler.HasKitchenItem)
            {
                Debug.Log("tezgahta e�ya var e�ya al�nabilir.");
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }

            else
            {
                Debug.Log("tabak durumu");
            }
        }
    }
}
