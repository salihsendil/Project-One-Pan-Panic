using UnityEngine;
using Zenject;

public class DeliveryModule : MonoBehaviour, IInteractableModule
{
    //References
    [Inject] private OrderSystem orderSystem;
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private SignalBus signalBus;

    public bool TryInteract(PlayerCarryingController player)
    {
        if (!player.HasItem()) { return false; }

        if (player.GetItem() is not ContainerItem containerItem) { return false; }

        if (containerItem.IsPlateReadyToServe())
        {
            if (orderSystem.TryCompleteOrder(containerItem.CurrentRecipe, out Order order))
            {
                signalBus.Fire(new OrderDeliveredSignal(order));
                PoolItemCleaner.RestoreAndReturn(player.RemoveItem(), poolManager);
                return true;
            }
            Debug.LogError("It's not ordered!");
        }
        return false;
    }
}
