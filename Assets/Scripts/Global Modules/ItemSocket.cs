using DG.Tweening;
using UnityEngine;

public class ItemSocket : MonoBehaviour, IInteractor
{
    [SerializeField] private Transform holdPoint;
    [SerializeField] private IPickable currentItem;

    public bool HasItem => currentItem != null;
    public IPickable GetItem => currentItem;

    public void SetItem(IPickable pickable)
    {
        SetItemToOffset(pickable, Vector3.zero);
    }

    public void SetItemToOffset(IPickable pickable, Vector3 offset)
    {
        pickable.Transform.DOKill();

        currentItem = pickable;

        currentItem.Transform.DOJump(holdPoint.position, 0.5f, 1, 0.2f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                currentItem.Transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f);
                currentItem.Transform.SetParent(holdPoint);
                currentItem.Transform.position = holdPoint.position;
                currentItem.Transform.localPosition += offset;
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
