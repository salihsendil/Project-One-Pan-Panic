using UnityEngine;

public interface ITransferItemHandler
{
    public bool HasKitchenItem { get; }
    public bool TryPlaceKitchenItem(out KitchenItem kitchenItem);
    public void ReceiveKitchenItem(KitchenItem kitchenItem);

}
