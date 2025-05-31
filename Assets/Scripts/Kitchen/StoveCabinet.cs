using System;
using System.Collections;
using UnityEngine;

public class StoveCabinet : BaseKitchenStation
{
    private Action OnKitchenObjectCooked;
    private bool isProcessDone = true;
    protected override KitchenStationType kitchenStationType => KitchenStationType.StoveCabinet;

    public override void Interact(PlayerCarryHandler interactor)
    {
        Debug.Log("interact");
        if (interactor.HasKitchenObject && !HasKitchenObject())
        {
            Debug.Log("first if");
            if (interactor.CurrentCarryKitchenObject.ObjectData.IsProcessableAtStation(kitchenStationType, interactor.CurrentCarryKitchenObject.CurrentState))
            {
                isProcessDone = interactor.CurrentCarryKitchenObject.CurrentState == KitchenObjectState.Cooked ? true : false;
                Debug.Log("second if");
                SetCurrentObject();
                float processTime = currentKitchenObject.ObjectData.GetMatch
                                                                    (kitchenStationType,
                                                                    currentKitchenObject.CurrentState).processTimer;
                StartCoroutine(CookingProgress(processTime));
            }
        }

        else if (!interactor.HasKitchenObject && HasKitchenObject() && isProcessDone)
        {
            StopAllCoroutines();
            RemoveCurrentObject();
        }
    }

    private void StartNextProcess()
    {
        if (currentKitchenObject.ObjectData.IsProcessableAtStation(kitchenStationType, currentKitchenObject.CurrentState))
        {
            Debug.Log("nextProcess");
            isProcessDone = true;
            float processTime = currentKitchenObject.ObjectData.GetMatch
                                                                (kitchenStationType,
                                                                currentKitchenObject.CurrentState).processTimer;
            StartCoroutine(CookingProgress(processTime));
        }
    }

    private IEnumerator CookingProgress(float time)
    {
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            //update UI
            yield return null;
        }
        ProcessIngredient();
        isProcessDone = true;
        StartNextProcess();
    }
}
