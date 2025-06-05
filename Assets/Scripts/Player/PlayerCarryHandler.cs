using UnityEngine;

public class PlayerCarryHandler : MonoBehaviour, ITransferItemHandler
{
    [Header("Hold Point")]
    [SerializeField] private Transform holdPointTransform;

    [Header("Currently Holding")]
    [SerializeField] private KitchenItem currentKitchenItem;

    public bool HasKitchenItem => currentKitchenItem != null;

    public bool TryPlaceKitchenItem(out KitchenItem kitchenItem)
    {
        if (!HasKitchenItem)
        {
            kitchenItem = null;
            return false;
        }
        kitchenItem = currentKitchenItem;
        currentKitchenItem = null; //buras� buglu, her iki tarafta doluken objeyi b�rak�yor, animasyon da b�rak�yor, �st�ne obje alabiliyor, unchild yapm�yor.
        return true;
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
