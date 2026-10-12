using System;


// Main contract for interactive classes (ItemSocket, etc.).
// All interactive classes must implement this interface.

public interface IInteractor
{
    public event Action<IPickable> OnItemPlaced;
    public event Action<IPickable> OnItemRemoved;

    public bool HasItem { get; }
    public IPickable GetItem { get; }
    public IPickable CurrentItem { set; }
    public void SetItem(IPickable pickable);
    public IPickable RemoveItem();
}