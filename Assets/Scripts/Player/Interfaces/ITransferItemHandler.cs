using UnityEngine;

public interface ITransferItemHandler
{
    public bool HasKitchenItem { get; }
    public bool HasBusyForProcess { get;  set; }
    public KitchenItem GetKitchenItem { get; }
    public void GiveKitchenItem(out KitchenItem kitchenItem);
    public void ReceiveKitchenItem(KitchenItem kitchenItem);

}
