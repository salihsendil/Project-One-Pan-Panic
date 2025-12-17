using UnityEngine;

public interface IItemCarrier
{
    public bool HasItem();
    public BaseKitchenItem GetItem();
    public void SetItem(BaseKitchenItem obj);
    public BaseKitchenItem RemoveItem();
}
