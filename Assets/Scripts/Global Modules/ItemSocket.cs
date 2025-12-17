using UnityEngine;

public class ItemSocket : MonoBehaviour, IItemCarrier
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private BaseKitchenItem currentItem;

    public bool HasItem() => currentItem != null;
    public BaseKitchenItem GetItem() { return currentItem; }

    public void SetItem(BaseKitchenItem obj)
    {
        currentItem = obj;
        currentItem.transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        currentItem.transform.SetParent(holdPoint);
    }

    public BaseKitchenItem RemoveItem()
    {
        var tempItem = currentItem;
        currentItem = null;
        return tempItem;
    }
}
