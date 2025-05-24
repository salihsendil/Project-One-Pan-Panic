using UnityEngine;

public class TrashBin : BaseKitchenStation
{
    protected override KitchenStationType kitchenStationType => KitchenStationType.TrashBin;

    public override void Interact(PlayerCarryHandler interactor)
    {
        if (interactor.HasKitchenObject)
        {
            //POLISH: add animation
            Destroy(OnObjectDropRequest?.Invoke().gameObject);
            currentKitchenObject = null;
        }
    }
}
