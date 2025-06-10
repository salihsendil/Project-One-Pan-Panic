using UnityEngine;

public interface IContainerItem
{
    public bool CanPuttableOnPlate(KitchenItem kitchenItem);
    public void PutOnPlate(KitchenItem kitchenItem);
}
