using UnityEngine;

public interface IInteractor
{
    public bool HasItem { get; }
    public IPickable GetItem { get; }
    public void SetItem(IPickable pickable);
    public IPickable RemoveItem();
}
