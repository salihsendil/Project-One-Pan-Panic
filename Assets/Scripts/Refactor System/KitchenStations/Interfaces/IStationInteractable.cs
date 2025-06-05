using UnityEngine;

public interface IStationInteractable
{
    public void Interact();
    public void HandleRayHit(bool isHit);
}
