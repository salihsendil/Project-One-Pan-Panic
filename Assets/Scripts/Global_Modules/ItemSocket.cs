using DG.Tweening;
using UnityEngine;
using Zenject;

public class ItemSocket : MonoBehaviour, IInteractor
{
    //Zenject
    [Inject] private SFXService sfxService;

    //Type
    [SerializeField] private InteractorType interactorType;

    //Item
    private IPickable currentItem;

    //Positioning
    [SerializeField] private Transform holdPoint;

    //Test
    [SerializeField] private GameObject testGameObject; //test delete

    public bool HasItem => currentItem != null;
    public IPickable GetItem => currentItem;
    public InteractorType InteractorType => interactorType;

    private void Awake()//test delete
    {
        if (testGameObject != null)
        {
            if (testGameObject.TryGetComponent(out IPickable pickable))
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
                sfxService.PlaySFXOneShot(SFXType.ItemTransfer);
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
