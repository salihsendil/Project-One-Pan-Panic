using UnityEngine;

public class ItemSocket : MonoBehaviour, IItemCarrier
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject currentItem;

    public bool HasItem() => currentItem != null;
    public GameObject GetItem() { return currentItem; }

    public void SetItem(GameObject obj)
    {
        currentItem = obj;
        currentItem.transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        currentItem.transform.SetParent(holdPoint);
    }

    public GameObject RemoveItem()
    {
        var tempItem = currentItem;
        currentItem = null;
        return tempItem;
    }
}
