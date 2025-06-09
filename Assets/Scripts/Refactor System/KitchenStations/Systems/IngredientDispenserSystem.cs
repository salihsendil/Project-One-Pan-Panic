using UnityEngine;

public class IngredientDispenserSystem : KitchenStation
{
    [SerializeField] private KitchenItemSO kitchenItemSO;

    public override void Interact()
    {
        if (transferItemHandler == null) { return; }

        if (!IsOccupied) //dolap �st� bo�
        {
            if (!transferItemHandler.HasKitchenItem) //karakter bo�
            {
                GameObject item = Instantiate(kitchenItemSO.Prefab, kitchenItemPoint.position, Quaternion.identity);
                PlaceKitchenItem(item.GetComponent<KitchenItem>());
                transferItemHandler.ReceiveKitchenItem(RemoveKitchenItem());
            }

            else //karakter dolu
            {
                transferItemHandler.GiveKitchenItem(out var kitchenItem);
                PlaceKitchenItem(kitchenItem);
            }
        }

        else //dolap �st� dolu
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
