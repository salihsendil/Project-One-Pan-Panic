using System.Collections;
using UnityEngine;

public class CuttingCabinet : BaseKitchenStation
{
    protected override KitchenStationType kitchenStationType => KitchenStationType.CuttingCabinet;

    public override void Interact(PlayerCarryHandler interactor)
    {
        if (interactor.HasKitchenObject && !HasKitchenObject())
        {
            if (interactor.CurrentCarryKitchenObject.ObjectData.IsProcessableAtStation(kitchenStationType, interactor.CurrentCarryKitchenObject.CurrentState))
            {
                SetCurrentObject();
                int stepCount = currentKitchenObject.ObjectData.GetMatch(
                                                                kitchenStationType,
                                                                currentKitchenObject.CurrentState).processStepCounter;
                CutKitchenObject(stepCount);
                ProcessIngredient();
            }
        }

        else if (!interactor.HasKitchenObject && HasKitchenObject())
        {
            RemoveCurrentObject();
        }
    }

    private void CutKitchenObject(int number)
    {
        StartCoroutine(CuttingProgress(number));
    }

    private IEnumerator CuttingProgress(int againCounter)
    {
        int counter = 0;
        while (counter < againCounter)
        {
            Debug.Log("kesiyorum.");
            counter++;
            yield return new WaitForSeconds(0.5f);

        }
    }

}
