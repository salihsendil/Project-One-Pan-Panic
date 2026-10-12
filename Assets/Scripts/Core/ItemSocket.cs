using System;
using UnityEngine;


// This class controls setting, holding, and removing items during interaction.
// If you need to set an item at the start, fill the startItem variable. If not, leave it empty.
//
// The CurrentItem property is only added for PlateDispenserModule.cs.
// It invokes an event when an item is placed or removed.
//
// It can only set and remove items that implement IPickable.


public class ItemSocket : MonoBehaviour, IInteractor
{
    //Item
    private IPickable currentItem;
    [SerializeField] private GameObject startItem;

    //Positioning
    [SerializeField] private Transform holdPoint;


    //Properties
    public bool HasItem => currentItem != null;
    public IPickable GetItem => currentItem;
    public IPickable CurrentItem { set => currentItem = value; } //just for plate dispensermodule


    //Events
    public event Action<IPickable> OnItemPlaced;
    public event Action<IPickable> OnItemRemoved;


    private void Start()
    {
        if (startItem == null) return;
        if (!startItem.TryGetComponent(out currentItem)) return;

        SetItem(currentItem);
    }

    public void SetItem(IPickable pickable)
    {
        currentItem = pickable;

        Transform itemTransform = pickable.Transform;
        itemTransform.SetParent(holdPoint);
        itemTransform.localPosition = Vector3.zero;

        OnItemPlaced?.Invoke(pickable);
    }

    public IPickable RemoveItem()
    {
        var tempItem = currentItem;
        currentItem.Transform.parent = null;
        currentItem = null;
        OnItemRemoved?.Invoke(tempItem);
        return tempItem;
    }
}