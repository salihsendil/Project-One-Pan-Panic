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

        if (!interactor.GetItem.GetGameObject.TryGetComponent(out ContainerItem containerItem)) { return false; }

        if (containerItem.TryGetRecipe(out RecipeSO recipe))
        {
            if (orderSystem.TryCompleteOrder(recipe, out Order order))
            {
                signalBus.Fire(new OrderDeliveredSignal(order));
                interactor.RemoveItem();
                PoolItemCleaner.RestoreAndReturn(containerItem, poolManager);
                return true;
            }
            Debug.LogError("It's not ordered!");
        }
        return false;
    }
}
