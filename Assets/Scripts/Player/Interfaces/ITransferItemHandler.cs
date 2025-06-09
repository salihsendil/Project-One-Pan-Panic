using UnityEngine;

public interface ITransferItemHandler
{
    public bool HasKitchenItem { get; }
    public void GiveKitchenItem(out KitchenItem kitchenItem);
    public void ReceiveKitchenItem(KitchenItem kitchenItem);

}
