using System.Collections.Generic;
using UnityEngine;


// Base class for Plate and Cookware Items.
// All container items must show an icon, hold item(s), have container data, and be pickable.
//
// The container slot holds references to ingredient items.
// The list size is specified in the container data.


public abstract class ContainerItem : MonoBehaviour, IPickable
{
    [Header("Data")]
    [SerializeField] protected ContainerItemSO containerItemSO;

    [Header("Transfer")]
    [SerializeField] protected bool isPickable = true;
    [SerializeField] protected Transform holdPoint;

    [Header("Items")]
    [SerializeField] protected List<ContainerSlot> containerSlots;

    [Header("UI Display")]
    [SerializeField] protected IItemIconDisplay iconDisplay;


    public bool IsPickable { get => isPickable; set => isPickable = value; }
    public ItemType ItemType => containerItemSO.ItemType;
    public Transform Transform => transform;


    protected virtual void Awake()
    {
        containerSlots = new(containerItemSO.Capacity);
        iconDisplay = GetComponent<IItemIconDisplay>();
    }

    protected void SetItem(IPickable pickable)
    {
        Transform itemTransform = pickable.Transform;
        itemTransform.SetParent(holdPoint);
        itemTransform.localPosition = Vector3.zero;
    }


    public abstract bool CanReceive(IPickable pickable);
    public abstract void Receive(IPickable pickable);



    public bool HasItems => containerSlots != null && containerSlots.Count > 0;
    public List<ContainerSlot> GetItems => containerSlots;


    public abstract bool CanImportItems(List<ContainerSlot> items);
    public abstract void ImportItems(List<ContainerSlot> items);
    public abstract bool CanExportItems();
    public abstract List<ContainerSlot> ExportItems();
}
