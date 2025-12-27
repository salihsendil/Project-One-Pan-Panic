using UnityEngine;

public class ItemSocket : MonoBehaviour, IItemCarrier
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private BaseKitchenItem currentItem;

    public bool HasItem() => currentItem != null;
    public BaseKitchenItem GetItem() { return currentItem; }

    public void SetItem(BaseKitchenItem kitchenItem)
    {
        SetItemToOffset(kitchenItem, Vector3.zero);
    }

    public void SetItemToOffset(BaseKitchenItem kitchenItem, Vector3 offset)
    {
        currentItem = kitchenItem;
        currentItem.transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        currentItem.transform.SetParent(holdPoint);
        currentItem.transform.localPosition += offset;
        currentItem.transform.localRotation = Quaternion.identity;
    }

    public BaseKitchenItem RemoveItem()
    {
        var tempItem = currentItem;
        currentItem = null;
        return tempItem;
    }
}
