using UnityEngine;

public class EmptyCabinet : BaseKitchenStation
{
    public override void Interact(PlayerCarryHandler interactor)
    {
        if (interactor.HasKitchenObject && !HasKitchenObject())
        {
            currentKitchenObject = OnObjectDropRequest?.Invoke();
            currentKitchenObject.transform.position = kitchenObjectPickUpPoint.position;
            currentKitchenObject.transform.SetParent(kitchenObjectPickUpPoint);
        }

        else if (!interactor.HasKitchenObject && HasKitchenObject())
        {
            OnObjectPickUpRequest?.Invoke(currentKitchenObject.gameObject);
            currentKitchenObject = null;
            //Destroy(currentKitchenObject.gameObject);
        }
    }
}
