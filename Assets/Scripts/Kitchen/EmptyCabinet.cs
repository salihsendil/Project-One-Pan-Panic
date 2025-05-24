using UnityEngine;

public class EmptyCabinet : BaseKitchenStation
{
    protected override KitchenStationType kitchenStationType => KitchenStationType.EmptyCabinet;

    public override void Interact(PlayerCarryHandler interactor)
    {
        if (interactor.HasKitchenObject && !HasKitchenObject())
        {
            SetCurrentObject();
        }

        else if (!interactor.HasKitchenObject && HasKitchenObject())
        {
            RemoveCurrentObject();
        }
    }
}
