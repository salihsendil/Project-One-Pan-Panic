using System;
using UnityEngine;

public class IngredientCabinet : BaseKitchenStation
{
    [SerializeField] private KitchenObjectSO ingredient;
    public override void Interact(PlayerCarryHandler interactor)
    {
        if (!interactor.HasKitchenObject)
        {
            GameObject kitchenObject = Instantiate(ingredient.objectPrefab, transform.position, Quaternion.identity);
            OnObjectPickUpRequest?.Invoke(kitchenObject);
        }
    }
}
