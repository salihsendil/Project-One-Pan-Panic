using UnityEngine;

public class ItemSocket : MonoBehaviour, IItemCarrier
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private KitchenItem currentItem;

    public bool HasItem() => currentItem != null;
    public KitchenItem GetItem() { return currentItem; }

    public void SetItem(KitchenItem obj)
    {
        currentItem = obj;
        currentItem.transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        currentItem.transform.SetParent(holdPoint);
    }

    public KitchenItem RemoveItem()
    {
        var tempItem = currentItem;
        currentItem = null;
        return tempItem;
    }
}
