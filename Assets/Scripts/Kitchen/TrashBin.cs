using UnityEngine;

public class TrashBin : BaseKitchenStation
{
    public override void Interact(PlayerCarryHandler interactor)
    {
        if (interactor.HasKitchenObject)
        {
            //POLISH: add animation
            Destroy(OnObjectDropRequest?.Invoke());
        }
    }
}
