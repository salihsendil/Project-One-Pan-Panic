using UnityEngine;
using Zenject;

public class DeliveryModule : MonoBehaviour, IInteractableModule
{
    //References
    [Inject] private OrderSystem orderSystem;

    public bool TryInteract(PlayerCarryingController player)
    {
        if (!player.HasItem()) { return false; }

        if (player.GetItem() is not ContainerItem containerItem) { return false; }

        if (containerItem.IsPlateReadyToServe())
        {
            if (orderSystem.RecipeHasOrdered(containerItem.CurrentRecipeID))
            {
                Debug.Log("plate is serving");
                return true;
            }
        }
        Debug.LogError("there is not any order like that!");
        return false;
    }
}
