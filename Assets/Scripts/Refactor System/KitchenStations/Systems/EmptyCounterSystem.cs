using UnityEngine;

public class EmptyCounterSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (transferItemHandler.TryPlaceKitchenItem(out var kitchenItem)) //karakter dolu, dolap boþ
        {
            if (!HasKitchenItem())
            {
                Debug.Log("tezgah boþ, eþya koyulabilir.");
                PlaceKitchenItem(kitchenItem);
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
