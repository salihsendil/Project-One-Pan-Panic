using UnityEngine;
using Zenject;

public class DeliveryModule : MonoBehaviour, IInstantModule
{
    //References
    [Inject] private OrderSystem orderSystem;
    [Inject] private UniversalPoolManager poolManager;
    [Inject] private SignalBus signalBus;

    public bool TryInteractionInstant(IInteractor interactor)
    {
        if (!interactor.HasItem) { return false; }

        if (interactor.GetItem.GetGameObject.TryGetComponent(out ContainerItem containerItem)) { return false; }

        if (containerItem.IsPlateReadyToServe())
        {
            if (orderSystem.TryCompleteOrder(containerItem.CurrentRecipe, out Order order))
            {
                signalBus.Fire(new OrderDeliveredSignal(order));
                PoolItemCleaner.RestoreAndReturn(containerItem, poolManager);
                return true;
            }
            Debug.LogError("It's not ordered!");
        }
        return false;
    }
}
