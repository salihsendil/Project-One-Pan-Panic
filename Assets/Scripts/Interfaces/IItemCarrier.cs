using UnityEngine;

public interface IItemCarrier
{
    public bool HasItem();
    public KitchenItem GetItem();
    public void SetItem(KitchenItem obj);
    public KitchenItem RemoveItem();
}
