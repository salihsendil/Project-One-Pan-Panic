using System;
using UnityEngine;

public abstract class KitchenStation : MonoBehaviour, IStationInteractable
{
    public static Func<KitchenItem> OnKitchenItemPlaceOnRequest;
    public static Action<KitchenItem> OnKitchenItemTakeOffRequest;

    [Header("Kitchen Item")]
    [SerializeField] protected Transform kitchenItemPoint;
    [SerializeField] protected KitchenItem currentKitchenItem;

    [Header("Material Visuals")]
    [SerializeField] protected Material baseMaterial;
    [SerializeField] protected Material selectedMaterial;

    [Header("References")]
    [SerializeField] protected ITransferItemHandler transferItemHandler;

    private MeshRenderer meshRenderer => GetComponent<MeshRenderer>();
    public bool IsOccupied => currentKitchenItem != null;

    public void SetTransferItemHandler(ITransferItemHandler handler)
    {
        transferItemHandler = handler;
    }
    public virtual void Interact()
    {
        
    }

    public virtual void InteractAlternate()
    {

    }

    protected void PlaceKitchenItem(KitchenItem kitchenItem)
    {
        currentKitchenItem = kitchenItem;
        currentKitchenItem.gameObject.transform.position = kitchenItemPoint.position;
        currentKitchenItem.transform.SetParent(kitchenItemPoint);
    }

    protected KitchenItem RemoveKitchenItem()
    {
        var temp = currentKitchenItem;
        currentKitchenItem = null;
        return temp;
    }

    public void HandleRayHit(bool isHit)
    {
        meshRenderer.material = isHit ? selectedMaterial : baseMaterial;
    }

}
