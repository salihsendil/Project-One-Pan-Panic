using UnityEngine;
using Zenject;

public class DeliveryModule : MonoBehaviour, IInteractableModule
{
    //References
    [Inject] private OrderSystem orderSystem;
    [Inject] private KitchenItemPoolManager poolManager;
    [Inject] private SignalBus signalBus;

    public bool TryInteract(PlayerCarryingController player)
    {
        if (!player.HasItem()) { return false; }

        if (player.GetItem() is not ContainerItem containerItem) { return false; }

        if (containerItem.IsPlateReadyToServe())
        {
            if (orderSystem.RecipeHasOrdered(containerItem.CurrentRecipe.RecipeID))
            {
                signalBus.Fire(new OrderDeliveredSignal(containerItem.CurrentRecipe.SuccessScore));
                KitchenItemRestorer.FullRestoreContainer(containerItem, poolManager);
                return true;
            }
        }
        Debug.LogError("It's not ordered!");
        return false;
    }
}
