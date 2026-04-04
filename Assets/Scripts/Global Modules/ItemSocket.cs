using UnityEngine;

public class ItemSocket : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private GameObject currentItemTest;
    [SerializeField] private IPickable currentItem;
    [SerializeField] private GameObject currentItemGO; //debugDelete

    public bool HasItem => currentItem != null;
    public IPickable GetItem => currentItem;

    private void Start()
    {
        TestMethod();
    }

    public void SetItem(IPickable pickable)
    {
        SetItemToOffset(pickable, Vector3.zero);
    }

    public void SetItemToOffset(IPickable pickable, Vector3 offset)
    {
        currentItem = pickable;
        currentItemGO = pickable.GetGameObject; //debugDelete
        currentItem.Transform.SetParent(holdPoint);
        currentItem.Transform.position = holdPoint.position;
        currentItem.Transform.localPosition += offset;
    }

    public IPickable RemoveItem()
    {
        currentItemGO = null;
        var tempItem = currentItem;
        currentItem.Transform.parent = null;
        currentItem = null;
        return tempItem;
    }

    public void TestMethod()
    {
        if (currentItemTest != null)
        {
            if (currentItemTest.TryGetComponent(out IPickable pickable))
            {
                SetItem(pickable);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            TestMethod();
        }
    }
}
