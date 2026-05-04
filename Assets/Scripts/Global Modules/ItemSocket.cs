using DG.Tweening;
using UnityEngine;

public class ItemSocket : MonoBehaviour, IInteractor
{
    private IPickable currentItem;
    [SerializeField] private Transform holdPoint;

    [SerializeField] private GameObject gameObject; //test delete

    public bool HasItem => currentItem != null;
    public IPickable GetItem => currentItem;

    private void Awake()//test delete
    {
        if (gameObject != null)
        {
            if (gameObject.TryGetComponent(out IPickable pickable))
            {
                SetItem(pickable);
            }
        }
    }

    public void SetItem(IPickable pickable)
    {
        SetItemToOffset(pickable, Vector3.zero);
    }

    public void SetItemToOffset(IPickable pickable, Vector3 offset)
    {
        pickable.Transform.DOKill();

        currentItem = pickable;

        Transform itemTransform = pickable.Transform;
        itemTransform.DOJump(holdPoint.position, 0.5f, 1, 0.2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                itemTransform.SetParent(holdPoint);
                itemTransform.localPosition = offset;
            });
    }

    public IPickable RemoveItem()
    {
        currentItem.Transform.DOKill();

        var tempItem = currentItem;
        currentItem.Transform.parent = null;
        currentItem = null;
        return tempItem;
    }
}
