using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour, ITransferItemHandler
{
    [Header("Hold Point")]
    [SerializeField] private Transform holdPointTransform;

    [Header("Currently Holding")]
    [SerializeField] private KitchenItem currentKitchenItem;

    public bool HasKitchenItem => currentKitchenItem != null;

    public void GiveKitchenItem(out KitchenItem kitchenItem)
    {
        kitchenItem = currentKitchenItem;
        currentKitchenItem = null; //burasý buglu, her iki tarafta doluken objeyi býrakýyor, animasyon da býrakýyor, üstüne obje alabiliyor, unchild yapmýyor.
    }

    public void ReceiveKitchenItem(KitchenItem kitchenItem)
    {
        if (kitchenItem == null) { return; }

        currentKitchenItem = kitchenItem;
        currentKitchenItem.transform.SetParent(holdPointTransform);
        currentKitchenItem.transform.position = holdPointTransform.position;
    }

    public void InteractWithStation(KitchenStation station)
    {
        if (station == null) { return; }

        station.SetTransferItemHandler(this);
        station.Interact();
    }
}
