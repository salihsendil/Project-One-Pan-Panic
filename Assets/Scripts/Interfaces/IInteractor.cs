using UnityEngine;

public interface IInteractor
{
    public bool HasItem { get; }
    public IPickable GetItem { get; }
    public InteractorType InteractorType { get; }
    public void SetItem(IPickable pickable);
    public void SetItemToOffset(IPickable pickable, Vector3 offset);
    public IPickable RemoveItem();
}

public enum InteractorType
{
    None,
    Player = 5,
    HoldingCounter,
    IngredientCounter,
    CuttingCounter,
    CookingCounter,
    ContainerCounter,
    TrashCounter,
    DeliveryCounter
}