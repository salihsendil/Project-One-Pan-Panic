using UnityEngine;

public class ItemSocket : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject currentItemTest;
    [SerializeField] private IPickable currentItem;

    public bool HasItem => currentItem != null;
    public IPickable GetItem => currentItem;

    private void Start()
    {
        if (currentItemTest != null)
        {
            if (currentItemTest.TryGetComponent(out IPickable pickable))
            {
                SetItem(pickable);
            }
        }
    }

    public void SetItem(IPickable kitchenItem)
    {
        SetItemToOffset(kitchenItem, Vector3.zero);
    }

    public void SetItemToOffset(IPickable kitchenItem, Vector3 offset)
    {
        currentItem = kitchenItem;
        currentItem.Transform.SetParent(holdPoint);
        currentItem.Transform.SetPositionAndRotation(holdPoint.position, holdPoint.transform.rotation);
        currentItem.Transform.localPosition += offset;
        currentItem.Transform.localRotation = Quaternion.identity;
    }

    public IPickable RemoveItem()
    {
        var tempItem = currentItem;
        currentItem = null;
        return tempItem;
    }
}
