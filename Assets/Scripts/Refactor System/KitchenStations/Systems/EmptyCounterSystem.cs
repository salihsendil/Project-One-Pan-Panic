using UnityEngine;

public class EmptyCounterSystem : KitchenStation
{
    public override void Interact()
    {
        base.Interact();

        if (transferItemHandler.TryPlaceKitchenItem(out var kitchenItem)) //karakter dolu, dolap bo�
        {
            if (!HasKitchenItem())
            {
                Debug.Log("tezgah bo�, e�ya koyulabilir.");
                PlaceKitchenItem(kitchenItem);
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
