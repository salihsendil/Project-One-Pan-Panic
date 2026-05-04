using UnityEngine;

public interface IInteractor
{
    public bool HasItem { get; }
    public IPickable GetItem { get; }
    public void SetItem(IPickable pickable);
    public void SetItemToOffset(IPickable pickable, Vector3 offset);
    public IPickable RemoveItem();
}