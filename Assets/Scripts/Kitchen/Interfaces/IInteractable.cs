using UnityEngine;

public interface IInteractable
{
    public void Interact(PlayerCarryHandler interactor);
    public void HandleRayHit(bool isHit);

}
