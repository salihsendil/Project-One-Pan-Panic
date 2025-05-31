using System.Collections;
using UnityEngine;

public class CuttingCabinet : BaseKitchenStation
{
    private bool isProcessDone = true;
    protected override KitchenStationType kitchenStationType => KitchenStationType.CuttingCabinet;

    public override void Interact(PlayerCarryHandler interactor)
    {
        if (interactor.HasKitchenObject && !HasKitchenObject())
        {
            if (interactor.CurrentCarryKitchenObject.ObjectData.IsProcessableAtStation(kitchenStationType, interactor.CurrentCarryKitchenObject.CurrentState))
            {
                isProcessDone = false;
                SetCurrentObject();
                int stepCount = currentKitchenObject.ObjectData.GetMatch(
                                                                kitchenStationType,
                                                                currentKitchenObject.CurrentState).processStepCounter;
                StartCoroutine(CuttingProgress(stepCount));
            }
        }

        else if (!interactor.HasKitchenObject && HasKitchenObject() && isProcessDone)
        {
            RemoveCurrentObject();
        }
    }

    private IEnumerator CuttingProgress(int stepCount)
    {
        float counter = 0f;
        while (counter < stepCount)
        {
            counter += Time.deltaTime;
            //update UI
            yield return null;
        }
        ProcessIngredient();
        isProcessDone = true;
    }
}
