using UnityEngine;

public interface IStationInteractable
{
    public bool IsOccupied { get; }
    public void Interact();
    public void InteractAlternate();
    public void HandleRayHit(bool isHit);
}
